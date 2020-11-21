
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class RClientOut
    {
        private string _clientIdString;
        [JsonPropertyName("klantId")]
        public string ClientIdString { get => _clientIdString; set => _clientIdString = Constants.URI + value;}
        [JsonPropertyName("naam")]
        public string Name { get; set; }
        [JsonPropertyName("adres")]
        public string Address { get; set; }
        [JsonPropertyName("bestellingen")]
        public List<ROrderOut> Orders { get; set; } = new List<ROrderOut>();

        public RClientOut(string clientIdString, string name, string address, List<ROrderOut> orders)
        {
            ClientIdString = clientIdString;
            Name = name;
            Address = address;
            Orders = orders;
        }

        public RClientOut()
        {

        }

    }
}
