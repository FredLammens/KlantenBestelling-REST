
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DataLayer
{
    public class KlantenBestellingenContext : DbContext
    {
        /// <summary>
        /// DbSet that contains orders from database
        /// </summary>
        public DbSet<DOrder> Orders { get; set; }
        /// <summary>
        /// DbSet that contains clients from database
        /// </summary>
        public DbSet<DClient> Clients { get; set; }
        /// <summary>
        /// connectionstring for DB
        /// </summary>
        private string connectionString;
        /// <summary>
        /// Empty constructor
        /// </summary>
        public KlantenBestellingenContext() { }
        /// <summary>
        /// constructor that sets connectionstring
        /// </summary>
        /// <param name="db">what Database to use connectionstring from </param>
        public KlantenBestellingenContext(string db = "MainDB") : base()
        {
            SetConnectionString(db);
        }
        /// <summary>
        /// Sets the connectionstring derived from the appsettings.jsonfile
        /// </summary>
        /// <param name="db">what Database to use connectionstring from </param>
        private void SetConnectionString(string db = "MainDB") 
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            switch (db) 
            {
                case "MainDB":
                    connectionString = configuration.GetConnectionString("KlantenBestellingenConnection").ToString();
                    break;
                case "TestDB":
                    connectionString = configuration.GetConnectionString("KlantenBestellingenTestConnection").ToString();
                    break;
            }

        }
        /// <summary>
        /// Sets connectionString on the configure of the app
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(connectionString))
                SetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
