using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class RClientOut
    {
        private string _clientIdString;
        /// <summary>
        /// ClientId in String format with URI.
        /// </summary>
        [JsonPropertyName("klantId")]
        public string ClientIdString { get => _clientIdString; set => _clientIdString = Constants.URI + value; }
        /// <summary>
        /// Client Name for output.
        /// </summary>
        [JsonPropertyName("naam")]
        public string Name { get; set; }
        /// <summary>
        /// Client Address for output.
        /// </summary>
        [JsonPropertyName("adres")]
        public string Address { get; set; }
        /// <summary>
        /// list of order ids in string format with URI.
        /// </summary>
        [JsonPropertyName("bestellingen")]
        public List<string> OrdersIds { get; set; } = new List<string>();
        /// <summary>
        /// Rest Client Out constructor for output.
        /// </summary>
        /// <param name="clientIdString">clientId in URI string format</param>
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        /// <param name="ordersIds">list of orderIds in URI string format</param>

        public RClientOut(string clientIdString, string name, string address, List<string> ordersIds)
        {
            ClientIdString = clientIdString;
            Name = name;
            Address = address;
            OrdersIds = ordersIds;
        }
        /// <summary>
        /// empty constructor
        /// </summary>
        public RClientOut()
        {

        }

    }
}
