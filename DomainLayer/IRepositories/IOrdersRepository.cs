using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.IRepositories
{
    public interface IOrdersRepository
    {
        int AddOrder(Order order);
        int UpdateOrder(Order order);
        Order GetOrder(int id);
        int DeleteOrder(int id);
    }
}
