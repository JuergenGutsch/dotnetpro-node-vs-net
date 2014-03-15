using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageGallery.Infrastructire.Drawing
{
    internal static class ImageResize
    {
        #region AnchorPosition enum

        public enum AnchorPosition
        {
            Top,
            Center,
            Bottom,
            Left,
            Right
        }

        #endregion

        #region Dimensions enum

        public enum Dimensions
        {
            Width,
            Height
        }

        #endregion

        public static Image FixedSize(Image imgPhoto, int width, int height, Color color)
        {
            var sourceWidth = imgPhoto.Width;
            var sourceHeight = imgPhoto.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            var destX = 0;
            var destY = 0;
            decimal nPercent;
            var nPercentW = (width/(decimal) sourceWidth);
            var nPercentH = (height/(decimal) sourceHeight);

            //if we have to pad the height pad both the top and the bottom
            //with the difference between the scaled height and the desired height
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = (int) Math.Round((width - (sourceWidth*nPercent))/2, 0);
            }
            else
            {
                nPercent = nPercentW;
                destY = (int) Math.Round((height - (sourceHeight*nPercent))/2, 0);
            }

            var destWidth = (int) Math.Round(sourceWidth*nPercent);
            var destHeight = (int) Math.Round(sourceHeight*nPercent);

            var bitmap = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(color);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(imgPhoto,
                              new Rectangle(0, 0, destWidth, destHeight),
                              new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);

            graphics.Dispose();

            return bitmap;
        }

        public static Image Crop(Image imgPhoto, int width, int height, AnchorPosition anchor)
        {
            var sourceWidth = imgPhoto.Width;
            var sourceHeight = imgPhoto.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            var destX = -1;
            var destY = -1;
            double nPercent;
            var nPercentW = (width/(double) sourceWidth);
            var nPercentH = (height/(double) sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (anchor)
                {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;
                    case AnchorPosition.Bottom:
                        destY = (int) (height - (sourceHeight*nPercent));
                        break;
                    default:
                        destY = (int) ((height - (sourceHeight*nPercent))/2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (anchor)
                {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;
                    case AnchorPosition.Right:
                        destX = (int) (width - (sourceWidth*nPercent));
                        break;
                    default:
                        destX = (int) ((width - (sourceWidth*nPercent))/2);
                        break;
                }
            }

            var destWidth = (int) (sourceWidth*nPercent);
            var destHeight = (int) (sourceHeight*nPercent);

            var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            var graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(imgPhoto,
                              new Rectangle(destX, destY, destWidth + 2, destHeight + 2),
                              new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);

            graphics.Dispose();

            return bitmap;
        }
    }
}