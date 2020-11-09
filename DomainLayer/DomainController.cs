using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class DomainController
    {
        private readonly IUnitOfWork uow;

        public DomainController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        /// <summary>
        /// Adds client to database if not already in database. 
        /// And returns client from database
        /// </summary>
        /// <param name="client">client to insert</param>
        /// <returns></returns>
        public Client AddClient(Client client)
        {
            Client addedClient = uow.Clients.AddClient(client);
            uow.Complete();
            return addedClient;
        }
        /// <summary>
        /// Deletes client from database with id given.
        /// if in database and if client doesnt have any orders.
        /// </summary>
        /// <param name="id">id from client to delete</param>
        public void DeleteClient(int id)
        {
            uow.Clients.DeleteClient(id);
            uow.Complete();
        }
        /// <summary>
        /// Gets client with all orders from database with id given.
        /// if client is in database.
        /// </summary>
        /// <param name="id">id from client to get</param>
        /// <returns></returns>
        public Client GetClient(int id)
        {
            Client gettedClient = uow.Clients.GetClient(id);
            return gettedClient;
        }
        /// <summary>
        /// Updates client from database with id from client object and values from client object.
        /// </summary>
        /// <param name="client">client to update gotten from database</param>
        /// <returns></returns>
        public Client UpdateClient(Client client)
        {
            Client updatedClient = uow.Clients.UpdateClient(client);
            uow.Complete();
            return updatedClient;
        }
        /// <summary>
        /// Adds order from client to database via the foreign key.
        /// if order already is in database with the same client => adds amounts together and updates the database object.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public Order AddOrder(Order order, int clientID)
        {
            return uow.Orders.AddOrder(order, clientID);
        }
        /// <summary>
        /// Deletes order from Client derived with ClientId from database
        /// </summary>
        /// <param name="id">id from order to delete</param>
        /// <param name="clientId">clientId to remove link</param>
        public void DeleteOrder(int id, int clientId)
        {
            uow.Orders.DeleteOrder(id, clientId);
        }
        /// <summary>
        /// Gets order from Client derived with ClientId from database
        /// </summary>
        /// <param name="id">id from order to get</param>
        /// <param name="clientId">clientId to get client link</param>
        /// <returns></returns>
        public Order GetOrder(int id, int clientId)
        {
            return uow.Orders.GetOrder(id, clientId);
        }
        /// <summary>
        /// Updates order from client derived with clientId from database
        /// </summary>
        /// <param name="order">order to update</param>
        /// <param name="clientId">clientId for link</param>
        /// <returns></returns>
        public Order UpdateOrder(Order order, int clientId)
        {
            return uow.Orders.UpdateOrder(order, clientId);
        }
    }
}
