using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class RClientOut
    {
        private string _clientIdString;
        [JsonPropertyName("klantId")]
        public string ClientIdString { get => _clientIdString; set => _clientIdString = Constants.URI + value; }
        [JsonPropertyName("naam")]
        public string Name { get; set; }
        [JsonPropertyName("adres")]
        public string Address { get; set; }
        [JsonPropertyName("bestellingen")]
        public List<string> OrdersIds { get; set; } = new List<string>();

        public RClientOut(string clientIdString, string name, string address, List<string> ordersIds)
        {
            ClientIdString = clientIdString;
            Name = name;
            Address = address;
            OrdersIds = ordersIds;
        }

        public RClientOut()
        {

        }

    }
}
