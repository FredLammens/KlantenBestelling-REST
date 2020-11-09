using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositories
{
    public interface IOrdersRepository
    {
        Order AddOrder(Order order, int clientId);
        void AddOrders(IReadOnlyList<Order> orders);
        Order UpdateOrder(Order order);
        Order GetOrder(int id);
        Order GetOrder(Order order);
        void DeleteOrder(int id);
        bool IsInOrders(Order order);
    }
}
