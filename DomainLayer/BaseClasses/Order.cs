using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class Order
    {
        public int Id { get; set; }
        private Product _product;
        public Product Product { get; set; } //kan per definitie niet null zijn.
        private int _amount;
        public int Amount { get => _amount; set { if (value < 0) throw new Exception("Amount can't be empty.");  } }
        private Client _client;
        public Client Client { get => _client; set { if (value == null) throw new Exception("Client can't be empty"); Client.AddOrder(this); _client = value; } }

        public Order(Product product, int amount, Client client)
        {
            Product = product;
            Amount = amount;
            Client = client;
        }

        public Order(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Product == order.Product;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product);
        }
    }
}
