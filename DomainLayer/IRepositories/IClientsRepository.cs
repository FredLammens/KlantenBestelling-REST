using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositories
{
    public interface IClientsRepository
    {
        Client AddClient(Client client);
        Client UpdateClient(Client client);
        Client GetClient(int id);
        void DeleteClient(int id);
    }
}
