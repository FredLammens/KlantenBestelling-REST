using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositories
{
    public interface IOrdersRepository
    {
        Order AddOrder(Order order, int clientId);
        Order UpdateOrder(Order order, int clientId);
        Order GetOrder(int id, int clientId);
        void DeleteOrder(int id, int clientId);
    }
}
