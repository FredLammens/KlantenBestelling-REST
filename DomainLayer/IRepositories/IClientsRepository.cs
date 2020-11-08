using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositories
{
    public interface IClientsRepository
    {
        Client AddClient(Client client);
        int UpdateClient(Client client);
        Client GetClient(int id);
        int DeleteClient(int id);
    }
}
