using System.Collections.Generic;
using Webshop.Infrastructure.Data.Entities;

namespace Webshop.Models
{
    public class OrderReviewModel
    {
        public OrderReviewModel()
        {
            OrderItems = new List<OrderReviewItemModel>();
        }
        public IEnumerable<Category> Categories { get; set; }
        
        public IEnumerable<OrderReviewItemModel> OrderItems { get; set; }

        public Customer Customer { get; set; }
        public double SumAllItems { get; set; }
    }

    public class OrderReviewItemModel
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}