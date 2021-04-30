﻿using AssetsTools.NET;
using AssetsTools.NET.Extra;
using BCnEncoder.Encoder;
using BCnEncoder.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexturePlugin
{
    public class TextureEncoderDecoder
    {
        public static byte[] Decode(byte[] data, int width, int height, TextureFormat format)
        {
            switch (format)
            {
                case TextureFormat.RGB9e5Float: //pls don't use (what is this?)
                    return null;
                //crunch-unity
                //case TextureFormat.DXT1Crunched:
                //case TextureFormat.DXT5Crunched:
                //case TextureFormat.ETC_RGB4Crunched:
                //case TextureFormat.ETC2_RGBA8Crunched:
                //{
                //    byte[] dest = new byte[data.Length]; //just to be safe, buf is same size as original
                //    uint size = 0;
                //    unsafe
                //    {
                //        fixed (byte* dataPtr = data)
                //        fixed (byte* destPtr = dest)
                //        {
                //            IntPtr dataIntPtr = (IntPtr)dataPtr;
                //            IntPtr destIntPtr = (IntPtr)destPtr;
                //            size = PInvoke.DecodeByCrunch(dataIntPtr, destIntPtr, (int)format, (uint)width, (uint)height);
                //        }
                //    }
                //    if (size > 0)
                //    {
                //        byte[] resizedDest = new byte[size];
                //        Buffer.BlockCopy(dest, 0, resizedDest, 0, (int)size);
                //        dest = null;
                //        return resizedDest;
                //    }
                //    else
                //    {
                //        dest = null;
                //        return null;
                //    }
                //}
                //pvrtexlib
                case TextureFormat.ARGB32:
                case TextureFormat.BGRA32New:
                case TextureFormat.RGBA32:
                case TextureFormat.RGB24:
                case TextureFormat.ARGB4444:
                case TextureFormat.RGBA4444:
                case TextureFormat.RGB565:
                case TextureFormat.Alpha8:
                case TextureFormat.R8:
                case TextureFormat.R16:
                case TextureFormat.RG16:
                case TextureFormat.RHalf:
                case TextureFormat.RGHalf:
                case TextureFormat.RGBAHalf:
                case TextureFormat.RFloat:
                case TextureFormat.RGFloat:
                case TextureFormat.RGBAFloat:
                /////////////////////////////////
                case TextureFormat.YUV2:
                case TextureFormat.EAC_R:
                case TextureFormat.EAC_R_SIGNED:
                case TextureFormat.EAC_RG:
                case TextureFormat.EAC_RG_SIGNED:
                case TextureFormat.ETC_RGB4_3DS:
                case TextureFormat.ETC_RGBA8_3DS:
                case TextureFormat.ETC2_RGB4:
                case TextureFormat.ETC2_RGBA1:
                case TextureFormat.ETC2_RGBA8:
                case TextureFormat.PVRTC_RGB2:
                case TextureFormat.PVRTC_RGBA2:
                case TextureFormat.PVRTC_RGB4:
                case TextureFormat.PVRTC_RGBA4:
                case TextureFormat.ASTC_RGB_4x4:
                case TextureFormat.ASTC_RGB_5x5:
                case TextureFormat.ASTC_RGB_6x6:
                case TextureFormat.ASTC_RGB_8x8:
                case TextureFormat.ASTC_RGB_10x10:
                case TextureFormat.ASTC_RGB_12x12:
                case TextureFormat.ASTC_RGBA_4x4:
                case TextureFormat.ASTC_RGBA_5x5:
                case TextureFormat.ASTC_RGBA_8x8:
                case TextureFormat.ASTC_RGBA_10x10:
                case TextureFormat.ASTC_RGBA_12x12:
                {
                    byte[] dest = new byte[data.Length*16]; //just to be safe, buf is 16 times size of original (obv this is prob too big)
                    uint size = 0;
                    unsafe
                    {
                        fixed (byte* dataPtr = data)
                        fixed (byte* destPtr = dest)
                        {
                            IntPtr dataIntPtr = (IntPtr)dataPtr;
                            IntPtr destIntPtr = (IntPtr)destPtr;
                            size = PInvoke.DecodeByPVRTexLib(dataIntPtr, destIntPtr, (int)format, (uint)width, (uint)height);
                        }
                    }
                    if (size > 0)
                    {
                        byte[] resizedDest = new byte[size];
                        Buffer.BlockCopy(dest, 0, resizedDest, 0, (int)size);
                        dest = null;
                        return resizedDest;
                    }
                    else
                    {
                        dest = null;
                        return null;
                    }
                }
                //bcnencoder does not decode imagine that
                //detex
                case TextureFormat.DXT1:
                    return DXTDecoders.ReadDXT1(data, width, height, false);
                case TextureFormat.DXT5:
                    return DXTDecoders.ReadDXT5(data, width, height);
                case TextureFormat.BC7:
                    return BC7Decoder.ReadBC7(data, width, height);
                case TextureFormat.BC6H: //pls don't use
                case TextureFormat.BC4:
                case TextureFormat.BC5:
                    return null;
                default:
                    return null;
            }
        }

        public static byte[] Encode(byte[] data, int width, int height, TextureFormat format, int quality = 5)
        {
            switch (format)
            {
                case TextureFormat.RGB9e5Float: //pls don't use (what is this?)
                    return null;
                //crunch-unity
                //case TextureFormat.DXT1Crunched:
                //case TextureFormat.DXT5Crunched:
                //case TextureFormat.ETC_RGB4Crunched:
                //case TextureFormat.ETC2_RGBA8Crunched:
                //{
                //    byte[] dest = new byte[data.Length]; //just to be safe, buf is same size as original
                //    uint size = 0;
                //    unsafe
                //    {
                //        fixed (byte* dataPtr = data)
                //        fixed (byte* destPtr = dest)
                //        {
                //            IntPtr dataIntPtr = (IntPtr)dataPtr;
                //            IntPtr destIntPtr = (IntPtr)destPtr;
                //            size = PInvoke.EncodeByCrunch(dataIntPtr, destIntPtr, (int)format, quality, (uint)width, (uint)height);
                //        }
                //    }
                //    if (size > 0)
                //    {
                //        byte[] resizedDest = new byte[size];
                //        Buffer.BlockCopy(dest, 0, resizedDest, 0, (int)size);
                //        dest = null;
                //        return resizedDest;
                //    }
                //    else
                //    {
                //        dest = null;
                //        return null;
                //    }
                //}
                //pvrtexlib
                case TextureFormat.ARGB32:
                case TextureFormat.BGRA32New:
                case TextureFormat.RGBA32:
                case TextureFormat.RGB24:
                case TextureFormat.ARGB4444:
                case TextureFormat.RGBA4444:
                case TextureFormat.RGB565:
                case TextureFormat.Alpha8:
                case TextureFormat.R8:
                case TextureFormat.R16:
                case TextureFormat.RG16:
                case TextureFormat.RHalf:
                case TextureFormat.RGHalf:
                case TextureFormat.RGBAHalf:
                case TextureFormat.RFloat:
                case TextureFormat.RGFloat:
                case TextureFormat.RGBAFloat:
                /////////////////////////////////
                case TextureFormat.YUV2: //looks like this should be YUY2 and the api has a typo
                case TextureFormat.EAC_R:
                case TextureFormat.EAC_R_SIGNED:
                case TextureFormat.EAC_RG:
                case TextureFormat.EAC_RG_SIGNED:
                case TextureFormat.ETC_RGB4_3DS:
                case TextureFormat.ETC_RGBA8_3DS:
                case TextureFormat.ETC2_RGB4:
                case TextureFormat.ETC2_RGBA1:
                case TextureFormat.ETC2_RGBA8:
                case TextureFormat.PVRTC_RGB2:
                case TextureFormat.PVRTC_RGBA2:
                case TextureFormat.PVRTC_RGB4:
                case TextureFormat.PVRTC_RGBA4:
                case TextureFormat.ASTC_RGB_4x4:
                case TextureFormat.ASTC_RGB_5x5:
                case TextureFormat.ASTC_RGB_6x6:
                case TextureFormat.ASTC_RGB_8x8:
                case TextureFormat.ASTC_RGB_10x10:
                case TextureFormat.ASTC_RGB_12x12:
                case TextureFormat.ASTC_RGBA_4x4:
                case TextureFormat.ASTC_RGBA_5x5:
                case TextureFormat.ASTC_RGBA_8x8:
                case TextureFormat.ASTC_RGBA_10x10:
                case TextureFormat.ASTC_RGBA_12x12:
                {
                    byte[] dest = new byte[data.Length]; //just to be safe, buf is same size as original
                    uint size = 0;
                    unsafe
                    {
                        fixed (byte* dataPtr = data)
                        fixed (byte* destPtr = dest)
                        {
                            IntPtr dataIntPtr = (IntPtr)dataPtr;
                            IntPtr destIntPtr = (IntPtr)destPtr;
                            size = PInvoke.EncodeByPVRTexLib(dataIntPtr, destIntPtr, (int)format, quality, (uint)width, (uint)height);
                        }
                    }
                    if (size > 0)
                    {
                        byte[] resizedDest = new byte[size];
                        Buffer.BlockCopy(dest, 0, resizedDest, 0, (int)size);
                        dest = null;
                        return resizedDest;
                    }
                    else
                    {
                        dest = null;
                        return null;
                    }
                }
                case TextureFormat.DXT1:
                case TextureFormat.DXT5:
                case TextureFormat.BC4:
                case TextureFormat.BC5:
                case TextureFormat.BC7:
                    CompressionFormat bcFmt = CompressionFormat.Bc1;
                    switch (format)
                    {
                        case TextureFormat.DXT1: bcFmt = CompressionFormat.Bc1; break;
                        case TextureFormat.DXT5: bcFmt = CompressionFormat.Bc3; break;
                        case TextureFormat.BC4:  bcFmt = CompressionFormat.Bc4; break;
                        case TextureFormat.BC5:  bcFmt = CompressionFormat.Bc5; break;
                        case TextureFormat.BC7:  bcFmt = CompressionFormat.Bc7; break;
                    }
                    BcEncoder enc = new BcEncoder(bcFmt);
                    enc.OutputOptions.GenerateMipMaps = false;
                    return enc.EncodeToRawBytes(data, width, height, PixelFormat.Rgba32, 0, out int _, out int _);
                case TextureFormat.BC6H: //pls don't use
                    return null;
                default:
                    return null;
            }
        }
    }
}