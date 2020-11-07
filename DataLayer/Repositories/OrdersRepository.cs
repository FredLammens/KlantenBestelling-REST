using DataLayer.BaseClasses;
using DomainLayer;
using DomainLayer.IRepositories;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public int DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
