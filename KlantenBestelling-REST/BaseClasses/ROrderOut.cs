
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class ROrderOut
    {
        private string _bestellingId;
        [JsonPropertyName("bestellingId")]
        public string OrderId { get => _bestellingId; set => _bestellingId = ClientId + "/Bestelling/" + value; }
        [JsonPropertyName("product")]
        public string Product { get; set; }
        [JsonPropertyName("aantal")]
        public int Amount { get; set; }
        private string _clientId;
        [JsonPropertyName("klantId")]
        public string ClientId { get => _clientId; set => _clientId = "http://localhost:50051/api/Klant/" + value; }

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
