using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositories
{
    public interface IOrdersRepository
    {
        void AddOrder(Order order, int clientId);
        void AddOrders(IReadOnlyList<Order> orders);
        void UpdateOrder(Order order);
        Order GetOrder(int id);
        Order GetOrderWithoutId(Order order);
        void DeleteOrder(int id);
        bool IsInOrders(Order order);
        bool IsInOrders(int id);
    }
}
