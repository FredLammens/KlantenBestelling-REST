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

        public Order AddOrder(Order order,int clientID)
        {
            //mag nog niet in databank zitten 
            DOrder dOrder = Mapper.FromOrderToDOrder(order);
            if (context.Orders.Any(o => o.Amount == dOrder.Amount && o.Client.Name == dOrder.Client.Name && o.Client.Address == dOrder.Client.Address && o.Product == dOrder.Product))
                throw new Exception("Order already in database.");
            //klant toevoegen
            if (clientID <= 0) 
            {
                throw new Exception("No clientId provided.");
            }
            dOrder.Client = null;
            dOrder.Client_Id = clientID;
            using (context) 
            {
                context.Orders.Add(dOrder);
                context.SaveChanges();
                return Mapper.FromDOrderToOrder(dOrder);
            }
        }

        public void DeleteOrder(int id)
        {
            //check of order erin zit
            if (!context.Orders.Any(o => o.OrderId == id))
                throw new Exception("Order not in database.");
            context.Orders.Remove(new DOrder() { OrderId = id });
            context.SaveChanges();
        }

        public Order GetOrder(int id)
        {
            //kijk of het erinzit
            throw new NotImplementedException();
        }

        public int UpdateOrder(Order order)
        {
            //kijk of het erinzit
            throw new NotImplementedException();
        }
    }
}
