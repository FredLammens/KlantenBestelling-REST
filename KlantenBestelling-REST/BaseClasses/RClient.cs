using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlantenBestelling_REST.BaseClasses
{
    public class RClient
    {
        private string _klantId;
        public string klantId { get => _klantId; set => _klantId = "http://localhost:50012/api/Klant/" + value; }
        public string naam { get; set; }
        public string adres { get; set; }
        public List<ROrder> bestellingen { get; set; } = new List<ROrder>();

        public RClient(string klantId, string naam, string adres, List<ROrder> bestellingen)
        {
            this.klantId = klantId;
            this.naam = naam;
            this.adres = adres;
            this.bestellingen = bestellingen;
        }

        public RClient()
        {

        }

    }
}
