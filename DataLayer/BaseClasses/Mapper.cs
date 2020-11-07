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
        public static DClient FromClientToDClient(Client client) 
        {
            return new DClient(client.Name, client.Adres);
        }
        public static Order FromDOrderToOrder(DOrder dorder) 
        {
            return new Order(dorder.Product, dorder.Amount, FromDClientToClient(dorder.Client));
        }
        public static Client FromDClientToClient(DClient dclient) 
        {
            return new Client(dclient.Name, dclient.Adres);
        }

    }
}
