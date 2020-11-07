using DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.BaseClasses
{
    public class Mapper
    {
        public static DOrder FromOrderToDOrder(Order order) 
        {
            return new DOrder(order.Product, order.Amount, FromClientToDClient(order.Client));
        }
        public static DClient FromClientToDClient(Client client) //voor adden hoeft orders niet mee 
        {
            return new DClient(client.Name, client.Address);
        }
        public static Order FromDOrderToOrder(DOrder dorder) 
        {
            return new Order(dorder.Product, dorder.Amount, FromDClientToClient(dorder.Client));
        }
        public static Client FromDClientToClient(DClient dclient)  //nagekeken
        {
            Client client = new Client(dclient.Name, dclient.Address);
            HashSet<Order> orders = new HashSet<Order>();
            foreach (DOrder dOrder in dclient.Orders)
            {
                orders.Add(new Order(dOrder.Product, dOrder.Amount, client));
            }
            client.Orders = orders;
            return client;
        }

    }
}
