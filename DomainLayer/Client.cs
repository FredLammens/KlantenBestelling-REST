using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public class Client
    {
        public int Id { get; set; }
        private string _name;
        public string  Name { get => _name; set { if (string.IsNullOrEmpty(value)) { throw new ArgumentException(); } _name = value; } }
        private string _adres;
        public string Adres { get => _adres; set { if (value.Length >= 10) { throw new ArgumentException(); } _adres = value; } }
        private readonly List<Order> _orders = new List<Order>();

        public Client(string name, string adres)
        {
                Name = name;
                Adres = adres;
        }
        public void AddOrder(Order order) 
        {
            if (order.Client == this)
            {
                _orders.Add(order);
            }
            else 
            {
                throw new ArgumentException("client is not the same.");
            }
        }
        public IReadOnlyList<Order> GetOrders() 
        {
            return _orders.AsReadOnly();
        }
    }
}
