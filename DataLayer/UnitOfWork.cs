using DataLayer.Repositories;
using DomainLayer;
using DomainLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KlantenBestellingenContext context;
        public IClientsRepository Clients { get; private set; }

        public IOrdersRepository Orders { get; private set; }

        public UnitOfWork(KlantenBestellingenContext context)
        {
            this.context = context;
            Clients = new ClientsRepository(context);
            Orders = new OrdersRepository(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
