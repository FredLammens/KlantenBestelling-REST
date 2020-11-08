using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public Client Client { get; set; }

        public Order(Product product, int amount, Client client)
        {
            Product = product;
            Amount = amount;
            Client = client;
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
