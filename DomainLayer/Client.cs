using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer
{
    public class Client
    {
        public int Id { get; set; }
        public string  Name { get; private set; }
        public string Adres { get; private set; }
        public HashSet<Order> Orders { get; private set; } = new HashSet<Order>();

        public Client(string name, string adres)
        {
            if (string.IsNullOrEmpty(name) && adres.Length >= 10)
                throw new ArgumentException();
            else {
                Name = name;
                Adres = adres;
            }
        }
        public void addOrder(Order order) 
        {
            if (order.Client == this)
            {
                Orders.Add(order);
            }
            else 
            {
                throw new ArgumentException("client is not the same.");
            }
        }
    }
}
