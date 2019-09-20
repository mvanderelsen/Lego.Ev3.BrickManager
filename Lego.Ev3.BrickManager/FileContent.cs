using Lego.Ev3.Framework.Core;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lego.Ev3.BrickManager
{
    public class FileContent
    {
        private const int WIDTH = FileConverter.RGF_WIDTH;
        private const int HEIGHT = FileConverter.RGF_HEIGHT;

        public static Bitmap ResizeInDisplayPort(Bitmap bitMap)
        {
            if (bitMap.Height == HEIGHT || bitMap.Width == WIDTH) return bitMap;

            int height = bitMap.Height;
            int width = bitMap.Width;

            while (width < WIDTH && height < HEIGHT)
            {
                width += 1;
                height += 1;
            }
            
            Bitmap resized = Resize(bitMap, width, height);
            bitMap.Dispose();
            return resized;
        }

        public static Bitmap Resize(Bitmap bitMap, int width, int height)
        {
            try
            {
                Bitmap b = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bitMap, 0, 0, width, height);
                }
                return b;
            }
            catch
            {
                return bitMap;
            }

            //Bitmap resizedBitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //Graphics g = Graphics.FromImage(resizedBitmap);
            //g.CompositingQuality = CompositingQuality.HighQuality;
            //g.CompositingMode = CompositingMode.SourceCopy;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //Rectangle rectangle = new Rectangle(0, 0, width, height);
            //g.DrawImage(bitMap, rectangle, 0, 0, bitMap.Width, bitMap.Height, GraphicsUnit.Pixel);
            //g.Dispose();

            //return resizedBitmap;
        }
    }
}
