using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace wpfTest.Source
{
    public static class BitmapHelper
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Metohd
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Public

        #region 비트맵 구하기 - GetBitmap(sourceBitmap, targetWidth)

        /// <summary>
        /// 비트맵 구하기
        /// </summary>
        /// <param name="sourceBitmap">소스 비트맵</param>
        /// <param name="targetWidth">타겟 너비</param>
        /// <returns>비트맵</returns>
        public static Bitmap GetBitmap(Bitmap sourceBitmap, int targetWidth)
        {
            int maximumSide = sourceBitmap.Width > sourceBitmap.Height ? sourceBitmap.Width : sourceBitmap.Height;

            float ratio = (float)maximumSide / (float)targetWidth;

            Bitmap targetBitmap = (sourceBitmap.Width > sourceBitmap.Height ? new Bitmap(targetWidth, (int)(sourceBitmap.Height / ratio)) :
                                                                              new Bitmap((int)(sourceBitmap.Width / ratio), targetWidth));

            using (Graphics targetGraphics = Graphics.FromImage(targetBitmap))
            {
                targetGraphics.CompositingQuality = CompositingQuality.HighQuality;
                targetGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                targetGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                targetGraphics.DrawImage
                (
                    sourceBitmap,
                    new Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height),
                    new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                    GraphicsUnit.Pixel
                );

                targetGraphics.Flush();
            }

            return targetBitmap;
        }

        #endregion
        #region 색상 균형 필터 적용하기 - ApplyColorBalanceFilter(sourceBitmap, blueLevel, greenLevel, redLevel)

        /// <summary>
        /// 색상 균형 필터 적용하기
        /// </summary>
        /// <param name="sourceBitmap">소스 비트맵</param>
        /// <param name="blueLevel">청색 레벨</param>
        /// <param name="greenLevel">녹색 레벨</param>
        /// <param name="redLevel">적색 레벨</param>
        /// <returns>비트맵</returns>
        public static Bitmap ApplyColorBalanceFilter(Bitmap sourceBitmap, byte blueLevel, byte greenLevel, byte redLevel)
        {
            BitmapData sourceBitmapData = sourceBitmap.LockBits
            (
                new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb
            );

            byte[] targetByteArray = new byte[sourceBitmapData.Stride * sourceBitmapData.Height];

            Marshal.Copy(sourceBitmapData.Scan0, targetByteArray, 0, targetByteArray.Length);

            sourceBitmap.UnlockBits(sourceBitmapData);

            float blue = 0;
            float green = 0;
            float red = 0;

            for (int i = 0; i + 4 < targetByteArray.Length; i += 4)
            {
                blue = 255.0f / (float)blueLevel * (float)targetByteArray[i];
                green = 255.0f / (float)greenLevel * (float)targetByteArray[i + 1];
                red = 255.0f / (float)redLevel * (float)targetByteArray[i + 2];

                if (blue > 255)
                {
                    blue = 255;
                }
                else if (blue < 0)
                {
                    blue = 0;
                }

                if (green > 255)
                {
                    green = 255;
                }
                else if (green < 0)
                {
                    green = 0;
                }

                if (red > 255)
                {
                    red = 255;
                }
                else if (red < 0)
                {
                    red = 0;
                }

                targetByteArray[i] = (byte)blue;
                targetByteArray[i + 1] = (byte)green;
                targetByteArray[i + 2] = (byte)red;
            }

            Bitmap targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData targetBitmapData = targetBitmap.LockBits
            (
                new Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb
            );

            Marshal.Copy(targetByteArray, 0, targetBitmapData.Scan0, targetByteArray.Length);

            targetBitmap.UnlockBits(targetBitmapData);

            return targetBitmap;
        }

        #endregion
    }
}
