
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class RClientOut
    {
        private int _clientId;
        [JsonIgnore]
        public int ClientID { get => _clientId; set 
            {
                _clientId = value;
                ClientIdString = value.ToString();
            }
        }
        private string _clientIdString;
        [JsonPropertyName("klantId")]
        public string ClientIdString { get => _clientIdString; set => _clientIdString = "http://localhost:50051/api/Klant/" + value;}
        [JsonPropertyName("naam")]
        public string Name { get; set; }
        [JsonPropertyName("adres")]
        public string Address { get; set; }
        [JsonPropertyName("bestellingen")]
        public List<ROrder> Orders { get; set; } = new List<ROrder>();

        public RClientOut(int clientID, string name, string address, List<ROrder> orders)
        {
            ClientID = clientID;
            Name = name;
            Address = address;
            Orders = orders;
        }

        public RClientOut()
        {

        }

    }
}
