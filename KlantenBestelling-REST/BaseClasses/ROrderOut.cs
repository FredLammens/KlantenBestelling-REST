using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class ROrderOut
    {
        private string _bestellingId;
        /// <summary>
        /// Rest Order Out OrderId
        /// </summary>
        [JsonPropertyName("bestellingId")]
        public string OrderId { get => _bestellingId; set => _bestellingId = ClientId + "/Bestelling/" + value; }
        /// <summary>
        /// Rest Order Out Product.
        /// </summary>
        [JsonPropertyName("product")]
        public string Product { get; set; }
        /// <summary>
        /// Rest Order Out Amount.
        /// </summary>
        [JsonPropertyName("aantal")]
        public int Amount { get; set; }
        private string _clientId;
        /// <summary>
        /// Rest Order Out ClientId
        /// </summary>
        [JsonPropertyName("klantId")]
        public string ClientId { get => _clientId; set => _clientId = Constants.URI + value; }
        /// <summary>
        /// Rest Order Out constructor used for output.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        /// <param name="clientId"></param>
        public ROrderOut(string orderId, string product, int amount, string clientId)
        {
            ClientId = clientId;
            OrderId = orderId;
            Product = product;
            Amount = amount;
        }
        /// <summary>
        /// Rest Order Out Empty constructor.
        /// </summary>
        public ROrderOut()
        {

        }
    }
}
