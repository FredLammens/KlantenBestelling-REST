namespace DataLayer
{
    public class KlantenBestellingenTestContext : KlantenBestellingenContext
    {
        /// <summary>
        /// uses the base constructor of klantenbestellingenContext and sets connectionstring to the TestDB.
        /// </summary>
        public KlantenBestellingenTestContext() : base("TestDB") { }
        /// <summary>
        /// after every context clears the DB. if bool = true
        /// </summary>
        /// <param name="keepExistingDB"> clears db if false</param>
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
