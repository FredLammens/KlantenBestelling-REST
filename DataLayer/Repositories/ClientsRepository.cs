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
        /// <summary>
        /// Adds client to database if not already in database. 
        /// And returns client from database
        /// </summary>
        /// <param name="client">client to insert</param>
        /// <returns></returns>
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
        /// <summary>
        /// Deletes client from database with id given.
        /// if in database and if client doesnt have any orders.
        /// </summary>
        /// <param name="id">id from client to delete</param>
        public void DeleteClient(int id)
        {
                //kijk of het erinzit
                if (!context.Clients.Any(c => c.ClientId == id))
                    throw new Exception("Client not in database.");
                //met bestellingen => foutmelding 
                if (context.Orders.Any(o => o.Client.ClientId == id))
                    throw new Exception("Client has orders.");
                context.Clients.Remove(context.Clients.Single(c => c.ClientId  == id));
                context.SaveChanges();
        }
        /// <summary>
        /// Gets client with all orders from database with id given.
        /// if client is in database.
        /// </summary>
        /// <param name="id">id from client to get</param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates client from database with id from client object and values from client object.
        /// </summary>
        /// <param name="client">client to update gotten from database</param>
        /// <returns></returns>
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
