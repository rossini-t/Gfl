using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gfl
{
    public enum GFL_SAVE_FLAGS : uint 
    {
        GFL_SAVE_REPLACE_EXTENSION = 0x0001,
        GFL_SAVE_ANYWAY = 0x0002,
        GFL_SAVE_WANT_FILENAME = 0x0003,
        GFL_SAVE_ICC_PROFILE = 0x0004
    }

    public enum GFL_LOAD_FLAGS : uint
    {
         GFL_LOAD_SKIP_ALPHA			   =0x00000001, /* Alpha not loaded (32bits only)                     */
         GFL_LOAD_IGNORE_READ_ERROR	       =0x00000002,
         GFL_LOAD_BY_EXTENSION_ONLY        =0x00000004 ,/* Use only extension to recognize format. Faster     */
         GFL_LOAD_READ_ALL_COMMENT         =0x00000008 ,/* Read Comment in GFL_FILE_DESCRIPTION               */
         GFL_LOAD_FORCE_COLOR_MODEL        =0x00000010 ,/* Force to load picture in the ColorModel            */
         GFL_LOAD_PREVIEW_NO_CANVAS_RESIZE =0x00000020 ,/* With gflLoadPreview, width & height are the maximum box */
         GFL_LOAD_BINARY_AS_GREY           =0x00000040 ,/* Load Black&White file in greyscale                 */
         GFL_LOAD_ORIGINAL_COLORMODEL      =0x00000080 ,/* If the colormodel is CMYK, keep it                 */
         GFL_LOAD_ONLY_FIRST_FRAME         =0x00000100 ,/* No search to check if file is multi-frame          */
         GFL_LOAD_ORIGINAL_DEPTH           =0x00000200 ,/* In the case of 10/16 bits per component            */
         GFL_LOAD_METADATA                 =0x00000400 ,/* Read all metadata                                  */
         GFL_LOAD_COMMENT                  =0x00000800 ,/* Read comment                                       */
         GFL_LOAD_HIGH_QUALITY_THUMBNAIL   =0x00001000 ,/* gflLoadThumbnail                                   */
         GFL_LOAD_EMBEDDED_THUMBNAIL       =0x00002000 ,/* gflLoadThumbnail                                   */
         GFL_LOAD_ORIENTED_THUMBNAIL       =0x00004000 ,/* gflLoadThumbnail                                   */
      GFL_LOAD_ORIGINAL_EMBEDDED_THUMBNAIL =0x00008000 ,/* gflLoadThumbnail                                   */
         GFL_LOAD_ORIENTED				   =0x00008000

    }

    public enum GFL_ERROR : ushort
    {
		GFL_NO_ERROR                = 0, // No error 
		GFL_ERROR_FILE_OPEN         = 1, //File =open error 
		GFL_ERROR_FILE_READ         = 2, // File read error 
		GFL_ERROR_FILE_CREATE       = 3, // File create error 
		GFL_ERROR_FILE_WRITE        = 4,// File write error 
		GFL_ERROR_NO_MEMORY         = 5,//No more memory 
		GFL_ERROR_UNKNOWN_FORMAT    = 6, // Unknown format 
		GFL_ERROR_BAD_BITMAP        = 7, //The format doesn't permit to save this type of picture 
		GFL_ERROR_BAD_FORMAT_INDEX  = 10,// Bad picture format 
		GFL_ERROR_BAD_PARAMETERS    = 50,// Bad parameters 
		GFL_UNKNOWN_ERROR           = 255 //  Other error 
    }

    public enum GFL_MODE
    {
       GFL_MODE_TO_BINARY     =  1, 
       GFL_MODE_TO_4GREY      =  2, 
       GFL_MODE_TO_8GREY      =  3, 
       GFL_MODE_TO_16GREY     =  4, 
       GFL_MODE_TO_32GREY     =  5, 
       GFL_MODE_TO_64GREY     =  6, 
       GFL_MODE_TO_128GREY    =  7, 
       GFL_MODE_TO_216GREY    =  8, 
       GFL_MODE_TO_256GREY    =  9, 
       GFL_MODE_TO_8COLORS    = 12, 
       GFL_MODE_TO_16COLORS   = 13, 
       GFL_MODE_TO_32COLORS   = 14, 
       GFL_MODE_TO_64COLORS   = 15, 
       GFL_MODE_TO_128COLORS  = 16, 
       GFL_MODE_TO_216COLORS  = 17, 
       GFL_MODE_TO_256COLORS  = 18, 
       GFL_MODE_TO_RGB        = 19, 
       GFL_MODE_TO_RGBA       = 20, 
       GFL_MODE_TO_BGR        = 21, 
       GFL_MODE_TO_ABGR       = 22, 
       GFL_MODE_TO_BGRA       = 23, 
       GFL_MODE_TO_ARGB       = 24 
    }

    internal enum GFL_MODE_PARAM
    {
       GFL_MODE_NO_DITHER        = 0, 
       GFL_MODE_PATTERN_DITHER   = 1, 
       GFL_MODE_HALTONE45_DITHER = 2,  // Only with GFL_MODE_TO_BINARY 
       GFL_MODE_HALTONE90_DITHER = 3,  // Only with GFL_MODE_TO_BINARY 
       GFL_MODE_ADAPTIVE         = 4, 
       GFL_MODE_FLOYD_STEINBERG  = 5  // Only with GFL_MODE_TO_BINARY 
    }

    public enum GFL_BITMAP_TYPE : ushort
    {
        GFL_BINARY = 0x0001,  //Binary 
        GFL_GREY = 0x0002 , //Grey scale 
        GFL_COLORS = 0x0004, // Colors with colormap 
        GFL_RGB = 0x00010 , //TrueColors - Red/Green/Blue 
        GFL_RGBA=  0x0020 , //TrueColors - Red/Green/Blue/Alpha 
        GFL_BGR = 0x0040 , //TrueColors - Blue/Green/Red 
        GFL_ABGR = 0x0080, // TrueColors - Alpha/Blue/Green/Red 
        GFL_BGRA = 0x0100 , //TrueColors - Blue/Green/Red/Alpha 
        GFL_ARGB = 0x0200, // TrueColors - Alpha/Red/Green/Blue 
        GFL_CMYK =  0x0400 //TrueColors - Cyan/Magenta/Yellow/Black 
    }

    public enum GFL_CORDER  : ushort 
    {
        GFL_CORDER_INTERLEAVED =0,// Interleaved 
        GFL_CORDER_SEQUENTIAL =1, // Sequential 
        GFL_CORDER_SEPARATE =2 // Separate 
    }

    internal enum GFL_BYTE_ORDER
    {
        GFL_BYTE_ORDER_DEFAULT = 0,
        GFL_BYTE_ORDER_LSBF = 1,
        GFL_BYTE_ORDER_MSBF = 2
    }     

    public enum GFL_COLORMODEL 
    {
        GFL_CM_RGB =0 ,//Red-Green-Blue 
        GFL_CM_GREY =1 ,//Greyscale 
        GFL_CM_CMY =2 ,//Cyan-Magenta-Yellow 
        GFL_CM_CMYK =3 ,//Cyan-Magenta-Yellow-Black 
        GFL_CM_YCBCR =4 ,//YCbCr 
        GFL_CM_YUV16 =5 ,//YUV 16bits 
        GFL_CM_LAB =6 ,//Lab 
        GFL_CM_LOGLUV =7 ,//Log Luv 
        GFL_CM_LOGL =8 //Log L 
    }

    public enum GFL_CTYPE : ushort
    {
        GFL_CTYPE_GREYSCALE =0, //Greyscale 
        GFL_CTYPE_RGB =1, //Red-Green-Blue 
        GFL_CTYPE_BGR =2, //Blue-Green-Red 
        GFL_CTYPE_RGBA =3, //Red-Green-Blue-Alpha 
        GFL_CTYPE_ABGR =4, //Alpha-Blue-Green-Red 
        GFL_CTYPE_CMY =5, //Cyan-Magenta-Yellow 
        GFL_CTYPE_CMYK =6 //Cyan-Magenta-Yellow-Black 
    }

    public enum GFL_COMPRESSION : ushort
    {
        GFL_NO_COMPRESSION =0 ,//No compression 
        GFL_RLE =1 ,//Packbits 
        GFL_LZW =2 ,//LZW 
        GFL_JPEG =3 ,//JPEG 
        GFL_ZIP =4 ,//ZIP 
        GFL_SGI_RLE =5 ,//GSI Packbits 
        GFL_CCITT_RLE =6 ,//CCITT RLE 
        GFL_CCITT_FAX3 =7 ,//Fax Group 3 
        GFL_CCITT_FAX3_2D =8 ,//Fax Group 3-2D 
        GFL_CCITT_FAX4 =9 ,//Fax Group 4 
        GFL_WAVELET =10 ,//Wavelette 
        GFL_UNKNOWN_COMPRESSION =255 //Other compression 
    }


    public enum GFL_ORIGIN : ushort
    {
        GFL_LEFT            =0x00,
        GFL_RIGHT           =0x01,
        GFL_TOP             =0x00,
        GFL_BOTTOM          =0x10,
        GFL_TOP_LEFT        =(GFL_TOP | GFL_LEFT),
        GFL_BOTTOM_LEFT     =(GFL_BOTTOM | GFL_LEFT),
        GFL_TOP_RIGHT       =(GFL_TOP | GFL_RIGHT),
        GFL_BOTTOM_RIGHT    =(GFL_BOTTOM | GFL_RIGHT)
    }

}
