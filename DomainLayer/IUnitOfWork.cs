using DomainLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IClientsRepository Clients { get; }
        IOrdersRepository Orders { get; }
        int Complete();
    }
}
