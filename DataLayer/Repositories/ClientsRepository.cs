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
        public void AddClient(Client client)
        {
            //mag nog niet in databank zitten 
            DClient dClient = Mapper.FromClientToDClient(client);
            if (context.Clients.Any(c => c.Name == client.Name && c.Address == client.Address))
                throw new Exception("Client already in database.");
            //klant toevoegen
            context.Clients.Add(dClient);
        }
        /// <summary>
        /// Deletes client from database with id given.
        /// if in database and has no orders
        /// </summary>
        /// <param name="id">id from client to delete</param>
        public void DeleteClient(int id)
        {
            //met bestellingen => foutmelding 
            if (HasOrders(id))
                throw new Exception("Client has orders.");
            //kijk of het erinzit
            if (!context.Clients.Any(c => c.ClientId == id))
                    throw new Exception("Client not in database.");
                context.Clients.Remove(context.Clients.Single(c => c.ClientId  == id));
        }
        /// <summary>
        /// Checks if client has orders
        /// </summary>
        /// <param name="id">clientId</param>
        /// <returns></returns>
        public bool HasOrders(int id) 
        {
            return context.Orders.Any(o => o.Client.ClientId == id);
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
            if (!context.Clients.Any(c => c.ClientId== id))
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
        /// <summary>
        /// Updates client from database with id from client object and values from client object.
        /// </summary>
        /// <param name="client">client to update gotten from database</param>
        /// <returns></returns>
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
