using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class KlantenBestellingenTestContext : KlantenBestellingenContext
    {
        public KlantenBestellingenTestContext() : base("TestDB") { }
        public KlantenBestellingenTestContext(bool keepExistingDB = false) : base("TestDB")
        {
            if (keepExistingDB)
                Database.EnsureCreated();
            else 
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

    }
}
