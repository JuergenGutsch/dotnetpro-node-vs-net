using System.Drawing;
using System.IO;
using System.Reflection;
using ImageGallery.Infrastructire.Drawing;
using NUnit.Framework;

namespace ImageGallery.Tests
{
    [TestFixture]
    public class ImageSizerTests
    {
        [Test]
        public void ResizeImage()
        {
            const int expWidth = 100;
            const int expHeight = 75;
            var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var sourceIamge = Image.FromFile(Path.Combine(root, "WP_001131.jpg"));
            var sizer = new InageSizer();

            var targetImage = sizer.ResizeImage(sourceIamge, 100, 100);

            var resultWidth = targetImage.Width;
            var resultHeight = targetImage.Height;

            Assert.That(resultWidth, Is.EqualTo(expWidth));
            Assert.That(resultHeight, Is.EqualTo(expHeight));
        }

        [Test]
        public void CompleteTests()
        {
            const int expWidth = 100;
            const int expHeight = 75;
            var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var sourceIamge = Path.Combine(root, "WP_001131.jpg");
            var filename = string.Format("{0}_{1}_{2}.jpg", Path.GetFileNameWithoutExtension(sourceIamge), expWidth, expHeight);
            var expectedPath = Path.Combine(root, "thumbs", filename);

            var sizer = new InageSizer();
            var resultPath = sizer.ResizeImage(sourceIamge, expWidth, expHeight);

            Assert.That(resultPath, Is.EqualTo(expectedPath));
            
            var expIamge = Image.FromFile(resultPath);

            var resultWidth = expIamge.Width;
            var resultHeight = expIamge.Height;

            Assert.That(resultWidth, Is.EqualTo(expWidth));
            Assert.That(resultHeight, Is.EqualTo(expHeight));
        }

        [Test]
        public void TestResizedName()
        {
            const int expWidth = 100;
            const int expHeight = 75;
            var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var sourceIamge = Path.Combine(root, "WP_001131.jpg");
            var filename = string.Format("{0}_{1}_{2}.jpg", Path.GetFileNameWithoutExtension(sourceIamge), expWidth, expHeight);
            var expected = Path.Combine(root, "thumbs", filename);

            var sizer = new InageSizer();

            var result = sizer.GreateNewFileName(sourceIamge, expWidth, expHeight);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
