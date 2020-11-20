
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenBestelling_REST.BaseClasses
{
    public class ROrderOut
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
        public ROrderOut(string orderId, string product, int amount, string clientId)
        {
            ClientId = clientId;
            OrderId = orderId;
            Product = product;
            Amount = amount;
        }
        public ROrderOut()
        {

        }
    }
}
