using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class DClient
    {
        /// <summary>
        /// Data Client Id
        /// </summary>
        [Key]
        public int ClientId { get; set; }
        /// <summary>
        /// Data Client Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Data Client Address
        /// </summary>
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// All Data Client orders
        /// </summary>
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
        /// <summary>
        /// Empty constructor 
        /// </summary>
        public DClient()
        {

        }
    }
}
