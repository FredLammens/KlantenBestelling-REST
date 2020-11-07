using DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class DOrder
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DClient Client { get; set; }

        public DOrder(Product product, int amount, DClient client)
        {
            Product = product;
            Amount = amount;
            Client = client;
        }
        public DOrder(int amount) 
        {
            Amount = amount;
        }
        public DOrder()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is DOrder order &&
                   Product == order.Product &&
                   Amount == order.Amount &&
                   EqualityComparer<DClient>.Default.Equals(Client, order.Client);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, Amount, Client);
        }
    }
}
