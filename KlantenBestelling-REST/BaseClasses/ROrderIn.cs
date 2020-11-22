using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class ROrderIn
    {
        /// <summary>
        /// Rest Order in Order id.
        /// </summary>
        [JsonPropertyName("bestellingId")]
        public int OrderId { get; set; }
        /// <summary>
        ///  Rest Order in Order ClientId.
        /// </summary>
        [JsonPropertyName("klantId")]
        public int ClientId { get; set; }
        /// <summary>
        ///  Rest Order in Product.
        /// </summary>
        [JsonPropertyName("product")]
        public string Product { get; set; }
        /// <summary>
        ///  Rest Order in Amount.
        /// </summary>
        [JsonPropertyName("aantal")]
        public int Amount { get; set; }
        /// <summary>
        ///  Rest Order in constructor.Used for in database.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        public ROrderIn(int clientId, string product, int amount)
        {
            ClientId = clientId;
            Product = product;
            Amount = amount;
        }
        /// <summary>
        /// empty constructor.
        /// </summary>
        public ROrderIn()
        {

        }
    }
}
