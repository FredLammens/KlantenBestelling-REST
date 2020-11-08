using DataLayer.BaseClasses;
using DomainLayer;
using DomainLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly KlantenBestellingenContext context;
        public OrdersRepository(KlantenBestellingenContext context)
        {
            this.context = context;
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
            //al in databank => amounts op tellen en updaten
            DOrder dOrder = Mapper.FromOrderToDOrder(order);
            if (context.Orders.Any(o => o.Amount == dOrder.Amount && o.Client.Name == dOrder.Client.Name && o.Client.Address == dOrder.Client.Address && o.Product == dOrder.Product))
            {
                DOrder orderToUpdate = context.Orders.Single(o => o.Amount == dOrder.Amount && o.Client.Name == dOrder.Client.Name && o.Client.Address == dOrder.Client.Address && o.Product == dOrder.Product);
                orderToUpdate.Amount = orderToUpdate.Amount + order.Amount;
                context.SaveChanges();
                return Mapper.FromDOrderToOrder(orderToUpdate);
            }
            else
            {
                //klant foreign key toevoegen
                if (clientID <= 0)
                {
                    throw new Exception("No clientId provided.");
                }
                dOrder.Client = null;
                dOrder.Client_Id = clientID;
                context.Orders.Add(dOrder);
                context.SaveChanges();
                return Mapper.FromDOrderToOrder(dOrder);
            }
        }
        /// <summary>
        /// Deletes order from Client derived with ClientId from database
        /// </summary>
        /// <param name="id">id from order to delete</param>
        /// <param name="clientId">clientId to remove link</param>
        public void DeleteOrder(int id, int clientId)
        {
            //check of order erin zit
            if (!context.Orders.Any(o => o.OrderId == id && o.Client_Id == clientId))
                throw new Exception("Order not in database.");
            context.Orders.Remove(context.Orders.Single(o => o.OrderId == id));
            context.SaveChanges();
        }
        /// <summary>
        /// Gets order from Client derived with ClientId from database
        /// </summary>
        /// <param name="id">id from order to get</param>
        /// <param name="clientId">clientId to get client link</param>
        /// <returns></returns>
        public Order GetOrder(int id, int clientId)
        {
            //kijk of het erinzit
            if (!context.Orders.Any(o => o.OrderId == id && o.Client_Id == clientId))
                throw new Exception("Order not in database.");
            DOrder dorder = context.Orders.Single(o => o.OrderId == id && o.Client_Id == clientId);
            return Mapper.FromDOrderToOrder(dorder);
        }
        /// <summary>
        /// Updates order from client derived with clientId from database
        /// </summary>
        /// <param name="order">order to update</param>
        /// <param name="clientId">clientId for link</param>
        /// <returns></returns>
        public Order UpdateOrder(Order order, int clientId)
        {
            //kijk of het erinzit
            if (!context.Orders.Any(o => o.OrderId == order.Id && o.Client_Id == clientId))
                throw new Exception("Order not in database.");
            DOrder orderToUpdate = context.Orders.Single(o => o.OrderId == order.Id && o.Client_Id == clientId);
            orderToUpdate.Amount = order.Amount;
            orderToUpdate.Product = order.Product;
            context.SaveChanges();
            return Mapper.FromDOrderToOrder(orderToUpdate);

        }
    }
}
