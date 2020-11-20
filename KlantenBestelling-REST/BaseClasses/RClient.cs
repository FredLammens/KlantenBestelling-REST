
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlantenBestelling_REST.BaseClasses
{
    public class RClient
    {
        private string _klantId;
        [JsonProperty("klantId")]
        public string ClientId { get => _klantId; set => _klantId = "http://localhost:50051/api/Klant/" + value; }
        [JsonProperty("naam")]
        public string Name { get; set; }
        [JsonProperty("adres")]
        public string Address { get; set; }
        [JsonProperty("bestellingen")]
        public List<ROrder> Orders { get; set; } = new List<ROrder>();
        [JsonConstructor]
        public RClient(string clientId, string name, string address, List<ROrder> orders)
        {
            ClientId = clientId;
            Name = name;
            Address = address;
            Orders = orders;
        }

        public RClient()
        {

        }

    }
}
