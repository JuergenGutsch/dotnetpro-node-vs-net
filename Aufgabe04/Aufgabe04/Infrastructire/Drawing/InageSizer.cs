using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageGallery.Infrastructire.Drawing
{
    public class InageSizer
    {
        public string ResizeImage(string imagePath, int width, int height)
        {
            if (imagePath == null) throw new ArgumentNullException("imagePath");
            if (!File.Exists(imagePath)) throw new ArgumentException(string.Format("image does not exist: {0}", imagePath));

            var newFilePath = GreateNewFileName(imagePath, width, height);

            if (File.Exists(newFilePath))
            {
                return newFilePath;
            }

            var sourceIamge = Image.FromFile(imagePath);
            var targetImage = ResizeImage(sourceIamge, width, height);
            targetImage.Save(newFilePath, ImageFormat.Jpeg);

            return newFilePath;
        }

        public string GreateNewFileName(string imagePath, int width, int height)
        {
            var newFileName = string.Format("{0}_{1}_{2}.jpg", Path.GetFileNameWithoutExtension(imagePath), width, height);
            if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(imagePath), "thumbs")))
            {
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(imagePath), "thumbs"));
            }
            return Path.Combine(Path.GetDirectoryName(imagePath), "thumbs", newFileName);
       }

        public Image ResizeImage(Image sourceIamge, int width, int height)
        {
            return ImageResize.FixedSize(sourceIamge, width, height, Color.White);
        }
    }
}