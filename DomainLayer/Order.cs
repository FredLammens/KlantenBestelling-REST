using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; private set; }
        public int Amount { get; private set; }
        public Client Client { get; private set; }

        public Order(Product product, int amount, Client client)
        {
            Product = product;
            Amount = amount;
            Client = client;
        }

    }
}
