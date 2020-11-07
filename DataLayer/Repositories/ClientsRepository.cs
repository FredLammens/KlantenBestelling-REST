using DataLayer.BaseClasses;
using DomainLayer;
using DomainLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly KlantenBestellingenContext context;
        public ClientsRepository(KlantenBestellingenContext context)
        {
            this.context = context;
        }

        public int AddClient(Client client)
        {
            using (context) 
            {
                if (context.Clients.Any(c => c.Name == client.Name && c.Adres == client.Adres))
                    throw new Exception("client already in database");

                DClient dclient = Mapper.FromClientToDClient(client);
                context.Add(dclient);
                context.SaveChanges();
                return dclient.Id;
            }
        }

        public int DeleteClient(int id)
        {
            using (context) 
            {
                if (!context.Clients.Any(c => c.Id == id))
                    throw new Exception("Client not in database");
                context.Clients.Remove(new DClient() { Id = id });
                context.SaveChanges();
                return id;
            }
        }

        public Client GetClient(int id)
        {
            using (context) 
            {
                if (!context.Clients.Any(c => c.Id == id))
                    throw new Exception("Client not in database");
                return Mapper.FromDClientToClient(context.Clients
                    .Include(c => c.Orders)
                    .AsNoTracking()
                    .Single(c => c.Id == id));
            }
        }

        public int UpdateClient(Client client)
        {
            using (context) 
            {
                if (!context.Clients.Any(c => c.Id == client.Id))
                    throw new Exception("Client not in database");
                DClient clientToUpdate = context.Clients.Single(c => c.Id == client.Id);
                clientToUpdate = Mapper.FromClientToDClient(client); //kan zijn variabelen handmatig moet aangepast worden
                context.SaveChanges();
                return clientToUpdate.Id;
            }
        }
    }
}
