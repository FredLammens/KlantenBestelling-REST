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
    public class OrdersRepository : IOrdersRepository
    {
        private readonly KlantenBestellingenContext context;
        public OrdersRepository(KlantenBestellingenContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Adds order to database
        /// </summary>
        /// <param name="order">order to add</param>
        /// <returns></returns>
        public Order AddOrder(Order order)
        { 
            DOrder dOrder = Mapper.FromOrderToDOrder(order);
            context.Orders.Add(dOrder);
            return Mapper.FromDOrderToOrder(dOrder);
        }
        /// <summary>
        /// Deletes order from  database
        /// </summary>
        /// <param name="id">id from order to delete</param>
        public void DeleteOrder(int id)
        {
            //check of order erin zit
            if (!context.Orders.Any(o => o.OrderId == id))
                throw new Exception("Order not in database.");
            context.Orders.Remove(context.Orders.Single(o => o.OrderId == id));
        }
        /// <summary>
        /// Gets order from database
        /// </summary>
        /// <param name="id">id from order to get</param>
        /// <returns></returns>
        public Order GetOrder(int id)
        {
            //kijk of het erinzit
            if (!context.Orders.Any(o => o.OrderId == id))
                throw new Exception("Order not in database.");
            DOrder dorder = context.Orders
                .AsNoTracking()
                .Include(o => o.Client)
                .AsNoTracking()
                .Single(o => o.OrderId == id);
            return Mapper.FromDOrderToOrder(dorder);
        }

        public bool IsInOrders(Order order)
        {
            if (context.Orders.Any(o => o.Amount == order.Amount && o.Product == order.Product && o.Client.ClientId == order.Client.Id))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Updates order from client derived with clientId from database
        /// </summary>
        /// <param name="order">order to update</param>
        /// <param name="clientId">clientId for link</param>
        /// <returns></returns>
        public Order UpdateOrder(Order order)
        {
            //kijk of het erinzit
            if (!context.Orders.Any(o => o.OrderId == order.Id))
                throw new Exception("Order not in database.");
            DOrder orderToUpdate = context.Orders.Single(o => o.OrderId == order.Id);
            orderToUpdate.Amount = order.Amount;
            orderToUpdate.Product = order.Product;
            return Mapper.FromDOrderToOrder(orderToUpdate);

        }
    }
}
