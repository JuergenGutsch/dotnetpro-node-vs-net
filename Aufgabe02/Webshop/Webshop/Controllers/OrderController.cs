using System.Linq;
using System.Web.Mvc;
using Webshop.Infrastructure;
using Webshop.Infrastructure.Data;
using Webshop.Infrastructure.Data.Entities;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class OrderController : Controller
    {
        private readonly StorageContext _storageContext;

        public OrderController()
        {
            _storageContext = StorageContext.Current;
        }

        public ActionResult OrderCart()
        {
            var sessionFacade = new SessionFacade(HttpContext.Session);
            var categories = _storageContext.Categories.LoadAll();

            var customer = sessionFacade.Customer;

            var model = new AddCustomerModel
            {
                Categories = categories,
                CustomerId = customer.Id,
                Name = customer.Name,
                FamilyName = customer.FamilyName,
                Address = customer.Address,
                Zip = customer.Zip,
                City = customer.City,
                Email = customer.Email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomerData(AddCustomerModel model)
        {
            var sessionFacade = new SessionFacade(HttpContext.Session);
            var categories = _storageContext.Categories.LoadAll();

            if (!ModelState.IsValid)
            {
                model.Categories = categories;
                return View("OrderCart", model);
            }

            // do handle customer
            var customer = new Customer
            {
                Id = model.CustomerId,
                Salutation = model.Salutation,
                Name = model.Name,
                FamilyName = model.FamilyName,
                Address = model.Address,
                Zip = model.Zip,
                City = model.City,
                Email = model.Email
            };

            sessionFacade.Customer = customer;

            return RedirectToAction("OrderReview");
        }

        [HttpGet]
        public ActionResult OrderReview()
        {
            var sessionFacade = new SessionFacade(HttpContext.Session);
            var categories = _storageContext.Categories.LoadAll();
            var cart = new Cart(sessionFacade, _storageContext);

            var items = cart.GetOrder().Lineitems.Select(x => new OrderReviewItemModel
            {
                Product = _storageContext.Products.LoadSingle(y => y.Id == x.ProductId),
                Quantity = x.Quantity
            });

            return View(new OrderReviewModel
            {
                OrderItems = items,
                Customer = sessionFacade.Customer,
                Categories = categories,
                SumAllItems = items.Select(x => x.Product.UnitPrice * x.Quantity).Sum()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderReview(OrderReviewModel model)
        {
            var sessionFacade = new SessionFacade(HttpContext.Session);
            var categories = _storageContext.Categories.LoadAll();
            var cart = new Cart(sessionFacade, _storageContext);

            var items = cart.GetOrder().Lineitems.Select(x => new OrderReviewItemModel
            {
                Product = _storageContext.Products.LoadSingle(y => y.Id == x.ProductId),
                Quantity = x.Quantity
            });

            if (!ModelState.IsValid)
            {
                model.OrderItems = items;
                model.Customer = sessionFacade.Customer;
                model.Categories = categories;
                model.SumAllItems = items.Select(x => x.Product.UnitPrice * x.Quantity).Sum();
                return View(model);
            }

            var newCustomerId = GetNewCustomerId();
            var newOrderId = GetNewOrderId();

            // Do Checkout
            var customer = sessionFacade.Customer;
            if (customer.Id <= 0)
            {
                customer.Id = newCustomerId;
                sessionFacade.Customer = customer;
            }
            var order = cart.GetOrder();
            order.Id = newOrderId;
            order.CustomerId = customer.Id;

            UpdateUnitsInStock(order);

            SaveCustomer(customer);

            SaveOrder(order);

            sessionFacade.ShoppingCart = null;

            return RedirectToAction("Index", "Home");
        }

        private void SaveOrder(Order order)
        {
            _storageContext.Orders.SaveOnSubmit(order);
            _storageContext.Orders.SubmitChanges();
        }

        private void SaveCustomer(Customer customer)
        {
            if (customer.Id > 0)
            {
                _storageContext.Customers.SaveOnSubmit(customer);
                _storageContext.Customers.SubmitChanges();
            }
        }

        private int GetNewCustomerId()
        {
            var lastCustomer = _storageContext.Customers.LoadAll()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
            var newCustomerId = 1;
            if (lastCustomer != null)
            {
                newCustomerId = lastCustomer.Id + 1;
            }
            return newCustomerId;
        }

        private int GetNewOrderId()
        {
            var lastOrder = _storageContext.Orders.LoadAll()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
            var newOrderId = 1;
            if (lastOrder != null)
            {
                newOrderId = lastOrder.Id + 1;
            }
            return newOrderId;
        }

        private void UpdateUnitsInStock(Order order)
        {
            foreach (var item in order.Lineitems)
            {
                var product = _storageContext.Products.LoadSingle(x => x.Id == item.ProductId);
                if (product.UnitsInStock >= item.Quantity)
                {
                    product.UnitsInStock -= item.Quantity;
                }
            }
            _storageContext.Products.SubmitChanges();
        }
    }
}