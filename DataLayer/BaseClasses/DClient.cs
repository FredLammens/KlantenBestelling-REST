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

        public DClient(string name, string address)
        {
            Name = name;
            Address = address;
        }
        public DClient()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is DClient client &&
                   Name.Equals(client.Name) &&
                   Address.Equals(client.Address);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Address);
        }
    }
}
