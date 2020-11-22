using DataLayer.Repositories;
using DomainLayer;
using DomainLayer.IRepositories;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// context used for the database.
        /// </summary>
        private readonly KlantenBestellingenContext context;

        public IClientsRepository Clients { get; private set; }

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
