using System.Text.Json.Serialization;

namespace KlantenBestelling_REST.BaseClasses
{
    public class RClientIn
    {
        [JsonPropertyName("klantId")]
        public int ClientID { get; set; }
        [JsonPropertyName("naam")]
        public string Name { get; set; }
        [JsonPropertyName("adres")]
        public string Address { get; set; }

        public RClientIn(int clientID, string name, string address)
        {
            ClientID = clientID;
            Name = name;
            Address = address;
        }

        public RClientIn(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public RClientIn()
        {

        }
    }
}
