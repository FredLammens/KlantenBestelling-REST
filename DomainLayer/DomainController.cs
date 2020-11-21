using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class DomainController : IDomainController
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
            uow.Clients.AddClient(client);
            //if client has order add orders
            if (client.GetOrders().Count > 0) 
            {
                uow.Orders.AddOrders(client.GetOrders());
            }
            //add client
            uow.Complete();
            Client addedClient = uow.Clients.GetClient(client.Name, client.Address);
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
            uow.Clients.UpdateClient(client);
            uow.Complete();
            Client updatedClient = uow.Clients.GetClient(client.Name, client.Address);
            return updatedClient;
        }
        /// <summary>
        /// Adds order from client to database via the foreign key.
        /// if order already is in database with the same client => adds amounts together and updates the database object.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public Order AddOrder(Order order,int clientId)//? hoe door domein checks van client
        {
            Order updatedOrder;
            //al in databank => amounts op tellen en updaten
            if (uow.Orders.IsInOrders(order))
            {
                updatedOrder = uow.Orders.GetOrderWithoutId(order);
                updatedOrder.Amount += order.Amount;
                uow.Orders.UpdateOrder(updatedOrder);
                uow.Complete();
            }
            else 
            {
                uow.Orders.AddOrder(order, clientId);
                uow.Complete();
                updatedOrder = uow.Orders.GetOrderWithoutId(order);
            }
            return updatedOrder;
        }
        /// <summary>
        /// Deletes order from Client derived with ClientId from database
        /// </summary>
        /// <param name="id">id from order to delete</param>
        /// <param name="clientId">clientId to remove link</param>
        public void DeleteOrder(int id)
        {
            uow.Orders.DeleteOrder(id);
            uow.Complete();
        }
        /// <summary>
        /// Gets order from Client derived with ClientId from database
        /// </summary>
        /// <param name="id">id from order to get</param>
        /// <param name="clientId">clientId to get client link</param>
        /// <returns></returns>
        public Order GetOrder(int id)
        {
            return uow.Orders.GetOrder(id);
        }
        /// <summary>
        /// Updates order from client derived with clientId from database
        /// </summary>
        /// <param name="order">order to update</param>
        /// <param name="clientId">clientId for link</param>
        /// <returns></returns>
        public Order UpdateOrder(Order order)
        {
            uow.Orders.UpdateOrder(order);
            uow.Complete();
            Order updatedOrder = uow.Orders.GetOrderWithoutId(order);
            return updatedOrder;
        }
        public bool IsInClients(int id) 
        {
            return uow.Clients.IsInClients(id);
        }
        public bool IsInOrders(int id) 
        {
            return uow.Orders.IsInOrders(id);
        }
    }
}
