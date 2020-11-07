using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DClient
    {
        public int Id { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        public string Adres { get; set; }
        public List<DOrder> Orders { get; set; } = new List<DOrder>();

        public DClient(string name, string adres)
        {
                Name = name;
                Adres = adres;
        }
    }
}
