
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenBestelling_REST.BaseClasses
{
    public class ROrder
    {
        private string _bestellingId;
        [JsonProperty("bestellingId")]
        public string OrderId { get => _bestellingId; set => _bestellingId = ClientId + "/Bestelling/" + value; }
        [JsonProperty("product")]
        public string Product { get; set; }
        [JsonProperty("aantal")]
        public int Amount { get; set; }
        [JsonProperty("klantId")]
        public string ClientId { get; set; }
        [JsonConstructor]
        public ROrder(string orderId, string product, int amount, string clientId)
        {
            OrderId = orderId;
            Product = product;
            Amount = amount;
            ClientId = clientId;
        }
        public ROrder()
        {

        }
    }
}
