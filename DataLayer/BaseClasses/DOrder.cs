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
        [ForeignKey("Client_Id")]
        public DClient Client { get; set; }
        [Required]
        public int Client_Id { get; set; }

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
