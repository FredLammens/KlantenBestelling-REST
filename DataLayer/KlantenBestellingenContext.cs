
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DataLayer
{
    public class KlantenBestellingenContext : DbContext
    {
        public DbSet<DOrder> Orders { get; set; }
        public DbSet<DClient> Clients { get; set; }
        private string connectionString;

        public KlantenBestellingenContext() { }
        public KlantenBestellingenContext(string db = "MainDB") : base()
        {
            SetConnectionString(db);
        }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(connectionString))
                SetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
