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

        public Client AddClient(Client client)
        {
            //mag nog niet in databank zitten 
            DClient dClient = Mapper.FromClientToDClient(client);
            if (context.Clients.Any(c => c.Name == client.Name && c.Address == client.Address))
                throw new Exception("Client already in database.");
            //klant toevoegen
            context.Clients.Add(dClient);
            context.SaveChanges();
            Client clientReturn = new Client(dClient.Name, dClient.Address);
            clientReturn.Id = dClient.ClientId;
            return clientReturn;
        }

        public void DeleteClient(int id)
        {

            //kijk of het erinzit
            if (!context.Clients.Any(c => c.ClientId == id))
                throw new Exception("Client not in database.");
            //met bestellingen => foutmelding 
            if (context.Orders.Any(o => o.Client.ClientId == id))
                throw new Exception("Client has orders.");
            context.Clients.Remove(new DClient() { ClientId = id });
            context.SaveChanges();
        }

        public Client GetClient(int id)
        {
            //kijk of het erinzit
            if (!context.Clients.Any(c => c.ClientId == id))
                throw new Exception("Client not in database");
            DClient dclient = context.Clients
                        .Include(c => c.Orders)
                        .Single(c => c.ClientId == id);
            return Mapper.FromDClientToClient(dclient);
        }

        public Client UpdateClient(Client client)
        {
            if (!context.Clients.Any(c => c.ClientId == client.Id))
                throw new Exception("Client not in database");
            DClient clientToUpdate = context.Clients.Single(c => c.ClientId == client.Id);
            clientToUpdate.Address = client.Address;
            clientToUpdate.Name = client.Name;
            context.SaveChanges();
            return Mapper.FromDClientToClient(clientToUpdate);
        }
    }
}
