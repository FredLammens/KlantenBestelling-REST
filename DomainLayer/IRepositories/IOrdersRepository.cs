using System.Collections.Generic;

namespace DomainLayer.IRepositories
{
    public interface IOrdersRepository
    {
        /// <summary>
        /// Adds order to database
        /// </summary>
        /// <param name="order">order to add</param>
        /// <returns></returns>
        void AddOrder(Order order, int clientId);
        /// <summary>
        /// Adds list of orders to database.
        /// </summary>
        /// <param name="orders">orders to add</param>
        void AddOrders(IReadOnlyList<Order> orders);
        /// <summary>
        /// Updates order from client derived with clientId from database
        /// </summary>
        /// <param name="order">order to update</param>
        /// <param name="clientId">clientId for link</param>
        /// <returns></returns>
        void UpdateOrder(Order order);
        /// <summary>
        /// Gets order from database
        /// </summary>
        /// <param name="id">id from order to get</param>
        /// <returns></returns>
        Order GetOrder(int id);
        /// <summary>
        /// Gets Order with order without id. Based on amount and product.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order GetOrderWithoutId(Order order);
        /// <summary>
        /// Deletes order from  database
        /// </summary>
        /// <param name="id">id from order to delete</param>
        void DeleteOrder(int id);
        /// <summary>
        /// Checks if order is in database
        /// </summary>
        /// <param name="order">order to check</param>
        /// <returns></returns>
        bool IsInOrders(Order order);
        /// <summary>
        /// Checks if order is in database based on id
        /// </summary>
        /// <param name="id">orderId</param>
        /// <returns></returns>
        bool IsInOrders(int id);
    }
}
