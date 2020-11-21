using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KlantenBestelling_REST.BaseClasses
{
    public class ROrderIn
    {
        [JsonPropertyName("bestellingId")]
        public int OrderId { get; set; }
        [JsonPropertyName("klantId")]
        public int ClientId { get; set; }
        [JsonPropertyName("product")]
        public string Product { get; set; }
        [JsonPropertyName("aantal")]
        public int Amount { get; set; }

        public ROrderIn(int clientId, string product, int amount)
        {
            ClientId = clientId;
            Product = product;
            Amount = amount;
        }
        public ROrderIn()
        {

        }
    }
}
