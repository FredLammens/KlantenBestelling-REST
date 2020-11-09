using DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        /// <summary>
        /// Data Order object
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="amount">amount</param>
        /// <param name="client">client</param>
        public DOrder(Product product, int amount,DClient client)
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
    }
}
