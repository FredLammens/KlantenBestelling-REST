using DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.BaseClasses
{
    public class Mapper
    {
        /// <summary>
        /// Maps Order object to a Data Order object
        /// </summary>
        /// <param name="order">Order to map</param>
        /// <returns></returns>
        public static DOrder FromOrderToDOrder(Order order)
        {
            return new DOrder(order.Product, order.Amount, FromClientToDClient(order.Client));
        }
        /// <summary>
        /// Maps Client object to a Data Client object
        /// </summary>
        /// <param name="client">Client to map</param>
        /// <returns></returns>
        public static DClient FromClientToDClient(Client client) //voor adden hoeft orders niet mee 
        {
            return new DClient(client.Name, client.Address);
        }
        /// <summary>
        /// Maps Data Order object to Order object
        /// </summary>
        /// <param name="dorder">Data order to map</param>
        /// <returns></returns>
        public static Order FromDOrderToOrder(DOrder dorder)
        {
            Order order = new Order(dorder.Product, dorder.Amount, FromDClientToClient(dorder.Client));
            order.Id = dorder.OrderId;
            return order;
        }
        /// <summary>
        /// Maps Data Client object to Client object
        /// </summary>
        /// <param name="dclient">=Data Client to map</param>
        /// <returns></returns>
        public static Client FromDClientToClient(DClient dclient)
        {
            HashSet<Order> orders = new HashSet<Order>();
            foreach (DOrder dOrder in dclient.Orders)
            {
                orders.Add(new Order(dOrder.Product, dOrder.Amount));
            }
            Client client = new Client(dclient.Name, dclient.Address,orders);
            client.Id = dclient.ClientId;
            return client;
        }

    }
}
