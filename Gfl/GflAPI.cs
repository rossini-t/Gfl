using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Imaging ;

namespace Gfl
{
    public static class GflAPI
    {
        [DllImport("libgfl340.dll")]
        public static extern GFL_ERROR gflLibraryInit();
        

        internal static GFL_LOAD_PARAMS Default_Load_params
        {
            get { return GflAPI.m_load_params; }
            set { GflAPI.m_load_params = value; }
        }

        private static GFL_LOAD_PARAMS m_load_params;

        [DllImport("libgfl340.dll")]
        public static extern string gflGetVersion();        

        internal static GFL_SAVE_PARAMS Default_Save_params
        {
            get { return GflAPI.m_save_params; }
            set { GflAPI.m_save_params = value; }
        }

        private static GFL_SAVE_PARAMS m_save_params;

        internal static PixelFormat GetPixelFormat(GFL_BITMAP_TYPE bitmap_type)
        {
            switch (bitmap_type)
            {
                case GFL_BITMAP_TYPE.GFL_BINARY:
                    return PixelFormat.Format1bppIndexed;
                    
                case GFL_BITMAP_TYPE.GFL_GREY:
                    return PixelFormat.Format8bppIndexed;

                case GFL_BITMAP_TYPE.GFL_COLORS:
                    return PixelFormat.Format8bppIndexed;                

                case GFL_BITMAP_TYPE.GFL_RGB:
                    return PixelFormat.Format24bppRgb;
                    
                case GFL_BITMAP_TYPE.GFL_RGBA:
                    return PixelFormat.Format32bppArgb;
                    
                case GFL_BITMAP_TYPE.GFL_BGR:
                    return PixelFormat.Format24bppRgb;
                    
                case GFL_BITMAP_TYPE.GFL_ABGR:
                    return PixelFormat.Format32bppArgb;
                    
                case GFL_BITMAP_TYPE.GFL_BGRA:

                    return PixelFormat.Format32bppArgb;
                    
                case GFL_BITMAP_TYPE.GFL_ARGB:

                    return PixelFormat.Format32bppArgb;
                    
                case GFL_BITMAP_TYPE.GFL_CMYK:
                    return PixelFormat.Undefined;
                    
                default:
                    throw new APIException("BITMAP_TYPE unknown");
            }
        }

        public static void Init()
        {
            gflLibraryInit();        
            gflGetDefaultLoadParams(out m_load_params);
            gflGetDefaultSaveParams(out m_save_params);                                
        }

        public static void Exit()
        {
            gflLibraryExit();
        }

        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflChangeColorDepth(IntPtr src, IntPtr dst, GFL_MODE mode, GFL_MODE_PARAM param);

        internal static GFL_ERROR gflChangeColorDepth(IntPtr src, GFL_MODE mode, GFL_MODE_PARAM param)
        {
            var nullPtr = IntPtr.Zero ;
            return gflChangeColorDepth(src, nullPtr, mode, param);
        }

        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflLoadBitmapFromHandle(IntPtr handle, out IntPtr bitmap, ref GFL_LOAD_PARAMS parameters, IntPtr informations);

        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflLoadBitmapFromMemory( IntPtr  data, UInt32 data_length, out IntPtr bitmap, ref GFL_LOAD_PARAMS parameters, IntPtr informations ); 

        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflLibraryExit();

        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflLoadBitmap(string filename, out GFL_BITMAP bitmap, 
                                                      ref GFL_LOAD_PARAMS param,out GFL_FILE_INFORMATION informations);

        /// <summary>
        /// Load image from file
        /// </summary>
        /// <param name="filename">complete path to image</param>
        /// <param name="bitmap">GFL_BITMAP structure to be copied from unmanaged memory</param>
        /// <param name="param">Load Paramters</param>
        /// <param name="informations"></param>
        /// <returns></returns>
        [DllImport("libgfl340.dll", CallingConvention = CallingConvention.StdCall,CharSet= CharSet.Ansi,BestFitMapping=true)]
        internal static extern GFL_ERROR gflLoadBitmap(string filename, [Out] out GFL_BITMAP bitmap,
                                                      ref GFL_LOAD_PARAMS param, IntPtr informations);

        [DllImport("libgfl340.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, BestFitMapping = true,ExactSpelling=true)]
        internal static extern GFL_ERROR gflLoadBitmap(string filename, [Out] out IntPtr bitmap,
                                                      ref GFL_LOAD_PARAMS param, IntPtr informations);

        [DllImport("libgfl340.dll")]
        internal static extern void gflGetDefaultSaveParams(out GFL_SAVE_PARAMS save_params);
        
        [DllImport("libgfl340.dll")]
        internal static extern IntPtr gflAllockBitmap(GFL_BITMAP_TYPE bitmap_type, Int32 width, Int32 height, UInt32 line_padding, ref GFL_COLOR color);

        [DllImport("libgfl340.dll")]
        internal static extern void gflFreeBitmap(GFL_BITMAP bitmap);


        [DllImport("libgfl340.dll")]
        internal static extern void gflFreeBitmap(IntPtr bitmap);


        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflLoadBitmap(string filename, out IntPtr bitmap,
                                                      ref GFL_LOAD_PARAMS param, out GFL_FILE_INFORMATION informations);

        [DllImport("libgfl340.dll")]
        internal static extern int gflGetFormatIndexByName(string indexName);

  
        [DllImport("libgfl340.dll",EntryPoint="gflGetDefaultLoadParams")]
        internal static extern void gflGetDefaultLoadParams([Out] out GFL_LOAD_PARAMS load_params);

        [DllImport("libgfl340.dll")]
        internal static extern void gflFreeFileInformation(ref GFL_FILE_INFORMATION informations);

        //Work only on Windows Xp.
        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflRotateFine(GFL_BITMAP src, ref GFL_BITMAP dst, double angle,ref GFL_COLOR bgColor);

        /// <summary>
        /// The gflRotateFine function applies a rotation on a picture.
        /// </summary>
        /// <param name="src">Pointer to a GFL_BITMAP structure</param>
        /// <param name="dst">Address of a pointer to a GFL_BITMAP structure. NULL (IntPtr.Zero) if on the same instance.</param>
        /// <param name="angle">Angle of rotation in degrees.</param>
        /// <param name="bgColor">Pointer to a GFL_COLOR structure used to set the background color. Can be NULL, the background color is (0,0,0).</param>
        /// <returns>The function returns GFL_NO_ERROR if it is successful or a value of GFL_ERROR.</returns>
        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflRotateFine( IntPtr  src,ref IntPtr dst, double angle,ref GFL_COLOR bgColor);
        
        [DllImport("libgfl340.dll")]        
        private static extern GFL_ERROR gflRotateFine( IntPtr  src,IntPtr dst, double angle,ref GFL_COLOR bgColor);

        [DllImport("libgfl340.dll")]
        private static extern GFL_ERROR gflRotate(IntPtr src, IntPtr dst, int angle, ref GFL_COLOR bgColor);

        internal static GFL_ERROR gflRotate(IntPtr src, int angle, ref GFL_COLOR bgColor)
        {
            IntPtr nullPointer = IntPtr.Zero;
            return gflRotate(src, nullPointer, angle, ref bgColor);
        }
                
        internal static GFL_ERROR gflRotateFine(IntPtr src,double angle, ref GFL_COLOR bgColor)
        {
            IntPtr nullPointer = IntPtr.Zero ;
            return gflRotateFine(src, nullPointer, angle, ref bgColor);
        }

        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflSaveBitmap(string filename, GFL_BITMAP bitmap, ref GFL_SAVE_PARAMS sparams);

        [DllImport("libgfl340.dll")]
        internal static extern void gflMemoryFree(IntPtr ptr);


        /// <summary>
        /// Free Only gfl_bitmap.Data
        /// </summary>
        /// <param name="gfl_bitmap">pointer to GFL_BITMAP struct</param>
        [DllImport("libgfl340.dll")]
        internal static extern void gflFreeBitmapData(IntPtr gfl_bitmap);


        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflSaveBitmap(string filename, IntPtr bitmap, ref GFL_SAVE_PARAMS sparams);

        [DllImport("libgfl340.dll")]
        internal static extern void gflGetDefaultLoadParams(IntPtr ptr);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("libgfl340.dll")]
        internal static extern GFL_ERROR gflLoadBitmap(string filePath, out IntPtr m_gfl_bitmap, IntPtr ptr, IntPtr nil);

        [DllImport("libgfl340.dll")]
        internal static extern void gflGetDefaultLoadParams(ref IntPtr ptr);

    }
}
