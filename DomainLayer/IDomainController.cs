using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public interface IDomainController
    {
        Client AddClient(Client client);
        void DeleteClient(int id);
        Client GetClient(int id);
        Client UpdateClient(Client client);
        Order AddOrder(Order order, int clientId);
        void DeleteOrder(int id);
        Order GetOrder(int id);
        Order UpdateOrder(Order order);
        bool IsInClients(int id);
        bool IsInOrders(int id);
    }
}
