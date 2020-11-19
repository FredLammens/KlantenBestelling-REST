using DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DClient
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public List<DOrder> Orders { get; set; }

        /// <summary>
        /// Data Client object
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        public DClient(string name, string address)
        {
            Name = name;
            Address = address;
        }
        public DClient()
        {

        }
    }
}
