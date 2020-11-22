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
        /// <summary>
        /// Data Order Id
        /// </summary>
        [Key]
        public int OrderId { get; set; }
        /// <summary>
        /// Data Order Product
        /// </summary>
        [Required]
        public Product Product { get; set; }
        /// <summary>
        /// Data Order Amount
        /// </summary>
        [Required]
        public int Amount { get; set; }
        /// <summary>
        /// Data Order Client
        /// </summary>
        [ForeignKey("Client_Id")]
        public DClient Client { get; set; }
        /// <summary>
        /// Used for Data Order Client foreign key 
        /// </summary>
        [Required]
        public int Client_Id { get; set; }
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
        /// <summary>
        /// Constructor for EFCore 
        /// </summary>
        /// <param name="amount"></param>
        public DOrder(int amount) 
        {
            Amount = amount;
        }
        /// <summary>
        /// Data Order Empty Constructor 
        /// </summary>
        public DOrder()
        {

        }
    }
}
