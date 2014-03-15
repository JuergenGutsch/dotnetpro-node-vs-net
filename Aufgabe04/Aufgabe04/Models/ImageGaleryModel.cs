using System.Collections.Generic;
using ImageGallery.Infrastructire.Data;

namespace ImageGallery.Models
{
    public class ImageGaleryModel
    {
        public IEnumerable<ImageData> Images { get; set; }
    }
}