using DataLayer.BaseClasses;
using DomainLayer;
using DomainLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataLayer.Repositories
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly KlantenBestellingenContext context;
        public ClientsRepository(KlantenBestellingenContext context)
        {
            this.context = context;
        }
        
        public void AddClient(Client client)
        {
            //mag nog niet in databank zitten 
            DClient dClient = Mapper.FromClientToDClient(client);
            if (context.Clients.Any(c => c.Name == client.Name && c.Address == client.Address))
                throw new Exception("Client already in database.");
            //klant toevoegen
            context.Clients.Add(dClient);
        }

        public void DeleteClient(int id)
        {
            //met bestellingen => foutmelding 
            if (HasOrders(id))
                throw new Exception("Client has orders.");
            //kijk of het erinzit
            if (!context.Clients.Any(c => c.ClientId == id))
                throw new Exception("Client not in database.");
            context.Clients.Remove(context.Clients.Single(c => c.ClientId == id));
        }

        public bool HasOrders(int id)
        {
            return context.Orders.Any(o => o.Client.ClientId == id);
        }
        public Client GetClient(int id)
        {
            //kijk of het erinzit
            if (!context.Clients.Any(c => c.ClientId == id))
                throw new Exception("Client not in database");
            DClient dclient = context.Clients
                        .AsNoTracking()
                        .Include(c => c.Orders)
                        .AsNoTracking()
                        .Single(c => c.ClientId == id);
            return Mapper.FromDClientToClient(dclient);
        }
        public Client GetClient(string Name, string Address)
        {
            if (!context.Clients.Any(c => c.Name == Name && c.Address == Address))
                throw new Exception("Client not in database.");

            DClient dclient = context.Clients
                                     .AsNoTracking()
                                     .Include(c => c.Orders)
                                     .AsNoTracking()
                                     .Single(c => c.Name == Name && c.Address == Address);
            return Mapper.FromDClientToClient(dclient);
        }
       
        public void UpdateClient(Client client)
        {
            if (!context.Clients.Any(c => c.ClientId == client.Id))
                throw new Exception("Client not in database");
            DClient clientToUpdate = context.Clients
                                       .Include(c => c.Orders)
                                       .Single(c => c.ClientId == client.Id);
            clientToUpdate.Address = client.Address;
            clientToUpdate.Name = client.Name;
        }
        public bool IsInClients(int id)
        {
            return context.Clients.Any(c => c.ClientId == id);
        }
        //Niet nodig
        //public void DeleteAllOrdersFromClient(int clientId)
        //{
        //    if (!context.Clients.Any(c => c.ClientId == clientId))
        //        throw new Exception("Client not in database");
        //    DClient clientToUpdate = context.Clients
        //                               .Include(c => c.Orders)
        //                               .Single(c => c.ClientId == clientId);
        //    if (clientToUpdate.Orders.Count > 0)
        //    {
        //        context.Orders.RemoveRange(clientToUpdate.Orders);
        //    }
        //}
    }
}
