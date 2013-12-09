using Gos.SimpleObjectStore;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Infrastructure.Data
{
    public class UnitOfWork
    {
        private UnitOfWork()
        {
            Categories = ObjectStore.GetInstance<Category, int>();
            Products = ObjectStore.GetInstance<Product, int>();
        }

        private static UnitOfWork _unitOfWork;
        public static UnitOfWork Current
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = new UnitOfWork();
                }
                return _unitOfWork;
            }
        }

        public IObjectStore<Category, int> Categories { get; set; }

        public IObjectStore<Product, int> Products { get; set; }
    }
}