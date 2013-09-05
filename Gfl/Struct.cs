using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices ;

namespace Gfl
{
    [StructLayout( LayoutKind.Sequential) ]
    internal class GFL_BITMAP
    {
          internal GFL_BITMAP_TYPE  Type;
          GFL_ORIGIN    Origin;
          internal Int32  Width;
          internal Int32  Height;  
          internal UInt32  BytesPerLine;  
          internal Int16 LinePadding ;
          UInt16  BitsPerComponent; 
          UInt16  ComponentsPerPixel;  
          UInt16 BytesPerPixel;  
          internal UInt16 Xdpi;  
          internal UInt16 Ydpi;  
          Int16  TransparentIndex;  
          Int32 ColorUsed;
         // [MarshalAs(UnmanagedType.LPStruct)]
          internal IntPtr ColorMap;//internal GFL_COLORMAP ColorMap; // * COlorMap  
          internal IntPtr Data;  
          IntPtr Comment;
          IntPtr MetaData; //void* MetaData;          
          Int32 XOffset;
          Int32 YOffset;
          IntPtr Name; 
    }

    [StructLayout( LayoutKind.Sequential)]
    internal struct READ_DATA
    {
        /// <summary>
        /// Pointer to a byte array
        /// </summary>
        internal IntPtr Data;
        internal UInt32 Index;
        internal UInt32 Length; 
    }


    internal struct GFL_COLOR
    {
          public UInt16 Red;
          public UInt16 Blue;
          public UInt16 Green;
          public UInt16 Alpha;

          internal GFL_COLOR(UInt16 red, UInt16 blue, UInt16 green, UInt16 alpha)
          {
              Red = red; Blue = blue; Green = green; Alpha = alpha;
          }

          internal GFL_COLOR(System.Drawing.Color color)
              : this((UInt16)color.R, (UInt16)color.B, (UInt16)color.G, (UInt16)color.A)
          {              
          }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct GFL_READ_CALLBACK
    {
        IntPtr handle;
        byte[] buffer;

    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct GFL_FILE_INFORMATION
    {
          GFL_BITMAP_TYPE Type;  
          GFL_ORIGIN Origin;  
          Int32 Width;  
          Int32 Height;  
          Int32 FormatIndex;
          [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
          string FormatName; //[8];  
          [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
          string Description; //[64];  
          UInt16 Xdpi;  
          UInt16 Ydpi;  
          UInt16 BitsPerComponent;  
          UInt16 ComponentsPerPixel;  
          Int32 NumberOfImages;  
          UInt32 FileSize;  
          GFL_COLORMODEL ColorModel;  
          GFL_COMPRESSION Compression;
          [MarshalAs( UnmanagedType.ByValTStr ,SizeConst=64)]
          string CompressionDescription;//[64]
    }
  
    [StructLayout(LayoutKind.Sequential)]
    internal struct GFL_SAVE_PARAMS
    {
        UInt32 Flags;
        internal Int32 FormatIndex;
        GFL_COMPRESSION Compression;
        Int16 Quality;           /* JPEG/Wic/Fpx  */
        Int16 CompressionLevel;  /* PNG           */
        byte Interlaced;        /* GIF           */
        byte Progressive;       /* JPEG          */
        byte OptimizeHuffmanTable; /* JPEG       */
        byte InAscii;           /* PPM           */
        UInt16 LutType; /* GFL_LUT_TO8BITS, GFL_LUT_TO10BITS, GFL_LUT_TO12BITS, GFL_LUT_TO16BITS */
        Byte DpxByteOrder;
        Byte CompressRatio; /* JPEG2000 */
        UInt32 MaxFileSize;   /* JPEG2000 */
        IntPtr LutData; /* RRRR.../GGGG..../BBBB.....*/
        IntPtr LutFilename;
        UInt32 Offset;
        GFL_CORDER ChannelOrder;
        GFL_CTYPE ChannelType;
        GFL_SAVE_CALLBACKS CallBacks;
        IntPtr UserParams;
    }

    internal struct GFL_LOAD_CALLBACKS
    {        
		IntPtr Read; 
		IntPtr Tell; 
		IntPtr Seek; 
		IntPtr AllocateBitmap; /* Global or not???? */
		IntPtr AllocateBitmapParams; 
		IntPtr Progress; 
		IntPtr ProgressParams; 
		IntPtr WantCancel; 
		IntPtr WantCancelParams; 
		IntPtr SetLine; 
		IntPtr SetLineParams; 	
    }


    internal struct GFL_SAVE_CALLBACKS
    {        	
	        IntPtr  Write; 
	        IntPtr  Tell;  
	        IntPtr  Seek;  
	        IntPtr GetLine; 
	        IntPtr  GetLineParams; 			
    }      


    [StructLayout(LayoutKind.Sequential)]
    internal struct GFL_LOAD_PARAMS
    {
        
        UInt32 Flags;        
        public Int32 FormatIndex;        
        Int32 ImageWanted;        
        GFL_ORIGIN Origin;        
        public GFL_BITMAP_TYPE ColorModel;        
        public UInt32 LinePadding;        
        byte DefaultAlpha;        
        byte PsdNoAlphaForNonLayer;        
        byte PngComposeWithAlpha;        
        byte WMFHighResolution;        
        Int32 Width;        
        Int32 Height;        
        UInt32 Offset;        
        GFL_CORDER ChannelOrder;        
        GFL_CTYPE ChannelType;        
        UInt16 PcdBase;        
        UInt16 EpsDpi;        
        Int32 EpsWidth;        
        Int32 EpsHeight;        
        UInt16 LutType;        
        UInt16 Reserved3;        
        IntPtr LutData;        
        IntPtr LutFileName;        
        byte CameraRawUseAutomaticBalance;        
        byte CameraRawUseCameraBalance;        
        byte CameraRawHighlight;        
        byte CameraRawAutoBright;        
        Single CameraRawGamma;        
        Single CameraRawBrightness;        
        Single CameraRawRedScaling;        
        Single CameraRawBlueScaling;        
        GFL_LOAD_CALLBACKS  Callbacks;        
        IntPtr UserParams;
    }

    internal struct GFL_COLORMAP
    {
        [MarshalAs(UnmanagedType.ByValArray,MarshalType="byte",SizeConst=256)]
        internal byte[] Red; //[256];
        [MarshalAs(UnmanagedType.ByValArray, MarshalType = "byte", SizeConst = 256)]
        internal byte[] Green;//[256];  
        [MarshalAs(UnmanagedType.ByValArray, MarshalType = "byte", SizeConst = 256)]
        internal byte[] Blue; //[256];
    }
}