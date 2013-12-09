using System;
using System.Web;
using Newtonsoft.Json;
using Webshop.Infrastructure.Model;

namespace Webshop.Infrastructure
{
    public class SessionFacade
    {
        private readonly HttpSessionStateBase _session;

        public SessionFacade(HttpSessionStateBase session)
        {
            _session = session;
        }

        private const string ShoppingCartName = "ShoppingCart";

        public ShoppingCart ShoppingCart
        {
            get
            {
                var value = DeserializeFromSession<ShoppingCart>(ShoppingCartName);
                if (value == null)
                {
                    return new ShoppingCart();
                }
                return value;
            }
            set
            {
                SerializeIntoSession(value, ShoppingCartName);
            }
        }

        private void SerializeIntoSession<T>(T value, string name)
        {
            _session[ShoppingCartName] = JsonConvert.SerializeObject(value, Formatting.None);
        }

        private T DeserializeFromSession<T>(string name)
        {
            var value = (string)(_session[name] ?? String.Empty);
            if (String.IsNullOrWhiteSpace(value))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}