
using System;
using System.Collections.Generic;
using System.Text;

namespace KlantenBestelling_REST.BaseClasses
{
    public class ROrder
    {
        private string _bestellingId;
        public string bestellingId { get => _bestellingId; set => _bestellingId = klantId + "/Bestelling/" + value; }
        public string product { get; set; }
        public int aantal { get; set; }
        public string klantId { get; set; }

        public ROrder(string bestellingId, string product, int aantal, string klantId)
        {
            this.product = product;
            this.aantal = aantal;
            this.klantId = klantId;
            this.bestellingId = bestellingId;
        }

        public ROrder()
        {

        }
    }
}
