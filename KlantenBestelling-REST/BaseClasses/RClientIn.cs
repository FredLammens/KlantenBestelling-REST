using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class RClientIn
    {
        /// <summary>
        /// ClientId
        /// </summary>
        [JsonPropertyName("klantId")]
        public int ClientID { get; set; }
        /// <summary>
        /// Client Name to map into db.
        /// </summary>
        [JsonPropertyName("naam")]
        public string Name { get; set; }
        /// <summary>
        /// Client Address to map into db.
        /// </summary>
        [JsonPropertyName("adres")]
        public string Address { get; set; }
        /// <summary>
        /// RestClientIn constructor to map into db.
        /// </summary>
        /// <param name="clientID">clientId</param>
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        public RClientIn(int clientID, string name, string address)
        {
            ClientID = clientID;
            Name = name;
            Address = address;
        }
        /// <summary>
        /// RestClientIn constructor used for mapping
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        public RClientIn(string name, string address)
        {
            Name = name;
            Address = address;
        }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public RClientIn()
        {

        }
    }
}
