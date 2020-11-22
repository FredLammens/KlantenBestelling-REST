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
        /// <summary>
        /// context used for the database.
        /// </summary>
        private readonly KlantenBestellingenContext context;
        /// <summary>
        /// Repository for Clients from DB.
        /// </summary>
        public IClientsRepository Clients { get; private set; }
        /// <summary>
        /// Repository for Orders from DB.
        /// </summary>
        public IOrdersRepository Orders { get; private set; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>

        public UnitOfWork(KlantenBestellingenContext context)
        {
            this.context = context;
            Clients = new ClientsRepository(context);
            Orders = new OrdersRepository(context);
        }
        /// <summary>
        /// Completes the unit of work.
        /// </summary>
        /// <returns></returns>

        public int Complete()
        {
            return context.SaveChanges();
        }
        /// <summary>
        /// Discards the unit of work.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
