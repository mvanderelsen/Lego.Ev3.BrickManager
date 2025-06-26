using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Lego.Ev3.BrickManager
{
    internal static class RgfConverter
    {
        /// <summary>
        /// Robot Graphic File width
        /// </summary>
        public const int RGF_WIDTH = 178;

        /// <summary>
        /// Robot Graphic File height
        /// </summary>
        public const int RGF_HEIGHT = 128;

        #region RGF

        /// <summary>
        /// Converts a rgf byte[] to a bitmap
        /// </summary>
        /// <param name="rgfFileData"></param>
        /// <returns></returns>
        public static Bitmap RGFtoBitmap(byte[] rgfFileData)
        {
            return RGFtoBitmap(rgfFileData, Color.Black);
        }


        /// <summary>
        /// Converts a rgf byte[] to a bitmap
        /// </summary>
        /// <param name="rgfFileData"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Bitmap RGFtoBitmap(byte[] rgfFileData, Color color)
        {
            Color white = Color.White;
            int width = rgfFileData[0];
            int height = rgfFileData[1];
            Bitmap bitmap = new Bitmap(width, height);
            int byteIndex = 2;
            int bitIndex = 0;

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    bool bit = (rgfFileData[byteIndex] & (1 << bitIndex)) > 0;
                    if (bit)
                    {
                        bitmap.SetPixel(w, h, color);
                    }
                    else bitmap.SetPixel(w, h, white);
                    bitIndex++;
                    if (bitIndex == 8 || w + 1 == width)
                    {
                        bitIndex = 0;
                        byteIndex++;
                    }
                }
            }
            bitmap.MakeTransparent(Color.White);
            return bitmap;
        }


        public static byte[] BitmapToRGF(byte[] data) 
        {
            byte[] rfg;
            using (MemoryStream stream = new(data)) 
            {
                rfg = BitmapToRGF(stream);
            }
            return rfg;
        }

        /// <summary>
        /// Converts a bitmap to a rgf byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] BitmapToRGF(Stream stream)
        {
            using (Bitmap bmp = new Bitmap(stream))
            {
                return BitmapToRGF(bmp);
            }
        }


        /// <summary>
        /// Converts a bitmap to a rgf byte[]
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] BitmapToRGF(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            bitmap = RemoveTransparancy(bitmap);

            if (width > RGF_WIDTH || height > RGF_HEIGHT)
            {
                float scale = Math.Min(RGF_WIDTH / (float)width, RGF_HEIGHT / (float)height);

                width = (int)Math.Round(scale * width);
                height = (int)Math.Round(scale * height);

                if (width > RGF_WIDTH) width = RGF_WIDTH;
                if (height > RGF_HEIGHT) height = RGF_HEIGHT;

                bitmap = ResizeImage(bitmap, new Size(width, height));
            }


            byte[] data;

            using (Bitmap image = ConvertTo1Bit(bitmap))
            {


                data = new byte[width * height + 2];
                data[0] = (byte)width;
                data[1] = (byte)height;


                int bitIndex = 0;
                int dataIndex = 2;
                int tmpByte = 0;


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color color = image.GetPixel(x, y);
                        if (color.R == 0 && color.G == 0 && color.B == 0)
                        {
                            tmpByte |= (1 << bitIndex);
                        }

                        bitIndex++;

                        if (bitIndex == 8 || x + 1 == width)
                        {
                            data[dataIndex] = (byte)tmpByte;
                            tmpByte = 0;
                            bitIndex = 0;
                            dataIndex++;
                        }
                    }

                }
            }

            return data;
        }

        private static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch
            {
                return imgToResize;
            }
        }

        private static Bitmap RemoveTransparancy(Bitmap src)
        {
            Bitmap target = new Bitmap(src.Size.Width, src.Size.Height);
            Graphics g = Graphics.FromImage(target);
            g.Clear(Color.White);
            g.DrawImage(src, 0, 0);
            return target;
        }

        private static Bitmap ConvertTo1Bit(Bitmap input)
        {
            var masks = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
            var output = new Bitmap(input.Width, input.Height, PixelFormat.Format1bppIndexed);
            var data = new sbyte[input.Width, input.Height];

            var inputData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                var scanLine = inputData.Scan0;
                var line = new byte[inputData.Stride];
                for (var y = 0; y < inputData.Height; y++, scanLine += inputData.Stride)
                {
                    Marshal.Copy(scanLine, line, 0, line.Length);
                    for (var x = 0; x < input.Width; x++)
                    {
                        data[x, y] = (sbyte)(64 * (GetGreyLevel(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]) - 0.5));
                    }
                }
            }
            finally
            {
                input.UnlockBits(inputData);
            }
            var outputData = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
            try
            {
                var scanLine = outputData.Scan0;
                for (var y = 0; y < outputData.Height; y++, scanLine += outputData.Stride)
                {
                    var line = new byte[outputData.Stride];
                    for (var x = 0; x < input.Width; x++)
                    {
                        var j = data[x, y] > 0;
                        if (j) line[x / 8] |= masks[x % 8];
                        var error = (sbyte)(data[x, y] - (j ? 32 : -32));
                        if (x < input.Width - 1) data[x + 1, y] += (sbyte)(7 * error / 16);
                        if (y < input.Height - 1)
                        {
                            if (x > 0) data[x - 1, y + 1] += (sbyte)(3 * error / 16);
                            data[x, y + 1] += (sbyte)(5 * error / 16);
                            if (x < input.Width - 1) data[x + 1, y + 1] += (sbyte)(1 * error / 16);
                        }
                    }
                    Marshal.Copy(line, 0, scanLine, outputData.Stride);
                }
            }
            finally
            {
                output.UnlockBits(outputData);
            }
            return output;
        }

        private static double GetGreyLevel(byte r, byte g, byte b)
        {
            return (r * 0.299 + g * 0.587 + b * 0.114) / 255;
        }
        #endregion
    }
}
