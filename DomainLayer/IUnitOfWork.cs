using DomainLayer.IRepositories;
using System;

namespace DomainLayer
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Repository for Clients from DB.
        /// </summary>
        IClientsRepository Clients { get; }
        /// <summary>
        /// Repository for Orders from DB.
        /// </summary>
        IOrdersRepository Orders { get; }
        /// <summary>
        /// Completes the unit of work.
        /// </summary>
        /// <returns></returns>
        int Complete();

    }
}
