using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositories
{
    public interface IClientsRepository
    {
        void AddClient(Client client);
        void UpdateClient(Client client);
        Client GetClient(int id);
        Client GetClient(string Name, string Address);
        void DeleteClient(int id);
        bool HasOrders(int id);
        bool IsInClients(int id);
    }
}
