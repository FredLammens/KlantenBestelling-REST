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

        public int AddOrder(Order order)
        {
            //mag nog niet in databank zitten 
            DOrder dOrder = Mapper.FromOrderToDOrder(order);
            if (context.Orders.Any(o => o.Amount == dOrder.Amount && o.Client.Name == dOrder.Client.Name && o.Client.Address == dOrder.Client.Address && o.Product == dOrder.Product))
                throw new Exception("Order already in database.");
            //klant toevoegen
            context.Orders.Add(dOrder);
            context.SaveChanges();
            return dOrder.OrderId;
        }

        public int DeleteOrder(int id)
        {
            //check of order erin zit
            throw new NotImplementedException();
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
