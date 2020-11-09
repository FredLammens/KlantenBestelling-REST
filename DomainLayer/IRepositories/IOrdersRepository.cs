using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositories
{
    public interface IOrdersRepository
    {
        Order AddOrder(Order order);
        Order UpdateOrder(Order order);
        Order GetOrder(int id);
        void DeleteOrder(int id);
        bool IsInOrders(Order order);
    }
}
