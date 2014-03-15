using Gos.SimpleObjectStore;

namespace ImageGallery.Infrastructire.Data
{
    public class StorageContext
    {
        public StorageContext()
        {
            Images = ObjectStore.GetInstance<ImageData>();
        }

        private static StorageContext _current;
        public static StorageContext Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new StorageContext();
                }
                return _current;
            }
        }

        public IObjectStore<ImageData> Images { get; set; }
    }
}