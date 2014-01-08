using Gos.SimpleObjectStore;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Infrastructure.Data
{
    public class StorageContext
    {
        private StorageContext()
        {
            Categories = ObjectStore.GetInstance<Category>();
            Products = ObjectStore.GetInstance<Product>();
            Customers = ObjectStore.GetInstance<Customer>();
            Orders = ObjectStore.GetInstance<Order>();
        }

        private static StorageContext _storageContext;
        public static StorageContext Current
        {
            get
            {
                if (_storageContext == null)
                {
                    _storageContext = new StorageContext();
                }
                return _storageContext;
            }
        }

        public IObjectStore<Category> Categories { get; set; }

        public IObjectStore<Product> Products { get; set; }

        public IObjectStore<Customer> Customers { get; set; }

        public IObjectStore<Order> Orders { get; set; }
    }
}