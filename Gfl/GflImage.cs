using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Gfl
{
    
    public class GflImage : IDisposable
    {
        private string m_filePath;
        IntPtr m_gfl_bitmap;
        GFL_BITMAP m_gfl_bitmap_struct;
        GFL_COLORMAP m_colorMap;

        public GflImage(string filePath)
            :this(filePath,System.IO.Path.GetExtension(filePath).Remove(0, 1))
        {            
        }

        /// <summary>
        /// Handle code error returned by gfl calls
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="Gfl.APIException">APIException</exception>
        private static void HandleError(GFL_ERROR e)
        {
            if (e == GFL_ERROR.GFL_NO_ERROR)
                return;
            switch (e)
            {                
                case GFL_ERROR.GFL_ERROR_FILE_OPEN:
                    throw new APIException("Error on open");                    
                case GFL_ERROR.GFL_ERROR_FILE_READ:
                    throw new APIException("Error on read");
                case GFL_ERROR.GFL_ERROR_FILE_CREATE:
                    throw new APIException("Error on create");
                case GFL_ERROR.GFL_ERROR_FILE_WRITE:
                    throw new APIException("Error on write");
                case GFL_ERROR.GFL_ERROR_NO_MEMORY:
                    throw new APIException("Not enough memory");
                case GFL_ERROR.GFL_ERROR_UNKNOWN_FORMAT:
                    throw new APIException("Unknown format");
                case GFL_ERROR.GFL_ERROR_BAD_BITMAP:
                    throw new APIException("Bad bitmap");
                case GFL_ERROR.GFL_ERROR_BAD_FORMAT_INDEX:
                    throw new APIException("Bad format index");
                case GFL_ERROR.GFL_ERROR_BAD_PARAMETERS:
                    throw new APIException("Bad parameters");
                case GFL_ERROR.GFL_UNKNOWN_ERROR:
                    throw new APIException("Unknow error");
                default:
                    throw new APIException("Unknown error type");
            }
        }

        /// <summary>
        /// For static initialization only
        /// </summary>
        private GflImage()
        {
        }

        public static GflImage FromHandle(Bitmap bitmap)
        {
            var handle = bitmap.GetHbitmap();            
            var img = new GflImage();
            var p = GflAPI.Default_Load_params ;
            p.FormatIndex = 2;
            var nullPtr = IntPtr.Zero ;            
            HandleError(GflAPI.gflLoadBitmapFromHandle(handle, out img.m_gfl_bitmap, ref p, nullPtr));
            GflAPI.DeleteObject(handle);
            img.RefreshStruct();
            return img;
        }

        public GflImage(string filePath, string format)
        {
            IntPtr nil = IntPtr.Zero;
            GFL_LOAD_PARAMS p = GflAPI.Default_Load_params;
            HandleError(GflAPI.gflLoadBitmap(filePath, out m_gfl_bitmap,  ref p, nil));
            m_filePath = filePath;
            RefreshStruct();
        }

        private void RefreshStruct()
        {            
            m_gfl_bitmap_struct = (GFL_BITMAP)Marshal.PtrToStructure(m_gfl_bitmap, typeof(GFL_BITMAP));
            if (m_gfl_bitmap_struct.ColorMap != IntPtr.Zero)
                m_colorMap = (GFL_COLORMAP)Marshal.PtrToStructure(m_gfl_bitmap_struct.ColorMap, typeof(GFL_COLORMAP));
        }

        private void FillPalette(ColorPalette cPalette)
        {
            for (int i = 0; i < 255; i++)
            {
                cPalette.Entries[i] = Color.FromArgb((int)m_colorMap.Red[i], (int)m_colorMap.Green[i],
                                                    (int)m_colorMap.Blue[i]);
            }
        }

        public GflImage RotateNew(double angle, Color bgColor)
        {
            var img = new Gfl.GflImage(1, 1, (UInt32)this.m_gfl_bitmap_struct.LinePadding, this.m_gfl_bitmap_struct.Type);
            GFL_COLOR c = new GFL_COLOR(bgColor);
            var err = GflAPI.gflRotateFine(m_gfl_bitmap, ref img.m_gfl_bitmap, angle, ref c);
            if (err != GFL_ERROR.GFL_NO_ERROR)
                throw new APIException(err.ToString());
            img.RefreshStruct();
            return img;
        }

        public GflImage(int width, int height, UInt32 stride, GFL_BITMAP_TYPE type)
        {
           GFL_COLOR  c = new GFL_COLOR(Color.White) ;
           m_gfl_bitmap = GflAPI.gflAllockBitmap(type, width, height, stride, ref c);
           RefreshStruct();
        }

        public GflImage GetCopy(GFL_MODE depthMode, GFL_BITMAP_TYPE type)
        {
            var img = new GflImage(m_gfl_bitmap_struct.Width, m_gfl_bitmap_struct.Height, 1, type);            
            GflAPI.gflChangeColorDepth(m_gfl_bitmap, img.m_gfl_bitmap, depthMode, GFL_MODE_PARAM.GFL_MODE_ADAPTIVE);
            img.RefreshStruct();
            return img;
        }

        private void GreyScale(Bitmap bmp)
        {
            ColorPalette p = bmp.Palette;            
            for (int i = 0; i < 255; i++)
            {
                p.Entries[i] = Color.FromArgb(i, i, i);
            }
            bmp.Palette = p;
        }
        
        public Bitmap ToBitmap()
        {                
            byte[] b = new byte[m_gfl_bitmap_struct.Height * m_gfl_bitmap_struct.BytesPerLine];
            Marshal.Copy(m_gfl_bitmap_struct.Data, b, 0, m_gfl_bitmap_struct.Height * (int)m_gfl_bitmap_struct.BytesPerLine);            
            GCHandle handle = GCHandle.Alloc(b, GCHandleType.Pinned);

            PixelFormat f = GflAPI.GetPixelFormat(m_gfl_bitmap_struct.Type);

            Bitmap bmp = new Bitmap(m_gfl_bitmap_struct.Width, m_gfl_bitmap_struct.Height,f);            
            if (m_gfl_bitmap_struct.Type == GFL_BITMAP_TYPE.GFL_GREY)
                GreyScale(bmp); 

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, m_gfl_bitmap_struct.Width, m_gfl_bitmap_struct.Height), 
                ImageLockMode.ReadWrite,f);
            data.Stride = (int)m_gfl_bitmap_struct.BytesPerLine;
            data.PixelFormat = f;
            data.Scan0 = handle.AddrOfPinnedObject();            
            bmp.UnlockBits(data);
            
            handle.Free();            
                       
            return bmp;
        }

        private IEnumerable<Color> GetColors(GFL_COLORMAP map)
        {
            for (int i = 0; i < map.Blue.Length; i++)
            {
                yield return Color.FromArgb((int)map.Red[i],(int) map.Green[i], (int)map.Blue[i]);
            }
        }

        private IEnumerable<YCC> GetYCC(GFL_COLORMAP map)
        {
            for (int i = 0; i < map.Blue.Length; i++)
            {
                yield return new YCC((int)map.Red[i], (int)map.Green[i], (int)map.Blue[i]);
            }
        }

        public int Width
        {
            get { return m_gfl_bitmap_struct.Width; }
        }

        public int Height
        {
            get { return m_gfl_bitmap_struct.Height; }
        }

        public int XDpi
        {
            get { return m_gfl_bitmap_struct.Xdpi; }
        }

        public int YDpi
        {
            get { return m_gfl_bitmap_struct.Ydpi; }
        }

        #region IDisposable Membres

        public void Dispose()
        {
            GflAPI.gflFreeBitmap(m_gfl_bitmap);
        }

        #endregion

        public void Save(string filePath)
        {
            string ext = System.IO.Path.GetExtension(filePath).Remove(0, 1);
            Save(filePath, ext);
        }  
      
        public void Rotate(double angle, Color bgColor)
        {
            GFL_COLOR c = new GFL_COLOR(bgColor);
            var err = GflAPI.gflRotateFine(m_gfl_bitmap,angle,ref c);            
            if (err != GFL_ERROR.GFL_NO_ERROR)
                throw new APIException(err.ToString());                        
            RefreshStruct();            
        }

        public void Rotate(int angle, Color bgColor)
        {
            GFL_COLOR c = new GFL_COLOR(bgColor);
            HandleError(GflAPI.gflRotate(m_gfl_bitmap, angle, ref c));
            RefreshStruct();
        }

        public void Save()
        {
            string ext = System.IO.Path.GetExtension(m_filePath).Remove(0, 1);
            Save(m_filePath, ext);
        }

        public void Save(string filePath, string format,GFL_MODE colorDepthMode)
        {
            ChangeColorDepth(colorDepthMode);
            Save(filePath, format);
        }

        public void Save(string filePath, string format)
        {
            var sparams = GflAPI.Default_Save_params;
            sparams.FormatIndex = GflAPI.gflGetFormatIndexByName(format);
            var err = GflAPI.gflSaveBitmap(filePath, m_gfl_bitmap, ref sparams);
            if (err != GFL_ERROR.GFL_NO_ERROR)
                throw new APIException(err.ToString());
        }

        public  void ChangeColorDepth(GFL_MODE colorDepthMode)
        {
            var err = GflAPI.gflChangeColorDepth(m_gfl_bitmap, colorDepthMode, GFL_MODE_PARAM.GFL_MODE_FLOYD_STEINBERG);            
            if (err != GFL_ERROR.GFL_NO_ERROR)
                throw new APIException(err.ToString());
            RefreshStruct();
        }

        internal void Rotate(double angle, Color color, GflImage final)
        {
            var c = new GFL_COLOR(color);
            HandleError(GflAPI.gflRotateFine(m_gfl_bitmap, ref final.m_gfl_bitmap, angle, ref c));
            final.RefreshStruct();
        }
    }
}
