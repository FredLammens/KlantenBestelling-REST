using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlantenBestelling_REST.BaseClasses
{
    public class Mapper
    {
        public static RClient ClientToRClient(Client client) 
        {
            return new RClient(client.Id.ToString(), client.Name, client.Address, OrdersToROrders(client.GetOrders()));
        }
        public static List<ROrder> OrdersToROrders(IReadOnlyList<Order> orders) 
        {
            List<ROrder> rorders = new List<ROrder>();
            foreach (var order in orders)
            {
                rorders.Add(OrderToROrder(order));
            }
            return rorders;
        }
        public static ROrder OrderToROrder(Order order) 
        {
            return new ROrder(order.Id.ToString(), order.Product.ToString("f"), order.Amount, order.Client.Id.ToString());
        }
        public static Client RClientToClient(RClient rclient)
        {
            Client client = new Client(rclient.Name, rclient.Address);
            return client;
        }
    }
}
