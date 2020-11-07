using DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class DOrder
    {
        public int Id { get; set; }
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

    }
}
