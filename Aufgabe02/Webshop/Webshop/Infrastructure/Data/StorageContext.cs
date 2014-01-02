using Gos.SimpleObjectStore;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Infrastructure.Data
{
    public class StorageContext
    {
        private StorageContext()
        {
            Categories = ObjectStore.GetInstance<Category, int>();
            Products = ObjectStore.GetInstance<Product, int>();
            Customers = new ObjectStore<Customer, int>();
            Orders = new ObjectStore<Order, int>();
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

        public IObjectStore<Category, int> Categories { get; set; }

        public IObjectStore<Product, int> Products { get; set; }

        public IObjectStore<Customer, int> Customers { get; set; }

        public IObjectStore<Order, int> Orders { get; set; }
    }
}