using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; private set; }
        public int Amount { get; private set; }
        public Client Client { get; private set; }

        public Order(List<Product> products, int amount, Client client)
        {
            Products = products;
            Amount = amount;
            Client = client;
        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   EqualityComparer<List<Product>>.Default.Equals(Products, order.Products) &&
                   Amount == order.Amount &&
                   EqualityComparer<Client>.Default.Equals(Client, order.Client);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Products, Amount, Client);
        }
    }
}
