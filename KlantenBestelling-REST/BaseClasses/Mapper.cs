using DomainLayer;
using System;
using System.Collections.Generic;

namespace KlantenBestelling_REST.BaseClasses
{
    public class Mapper
    {
        public static RClientOut ClientToRClientOut(Client client) 
        {
            return new RClientOut(client.Id.ToString(), client.Name, client.Address, OrdersToROrdersOutIds(client.GetOrders()));
        }
        private static List<string> OrdersToROrdersOutIds(IReadOnlyList<Order> orders) //iet smis met bestelling id => altijd 0
        {
            List<string> rorders = new List<string>();
            foreach (var order in orders)
            {
                rorders.Add(OrderToROrderOut(order).OrderId);
            }
            return rorders;
        }
        public static ROrderOut OrderToROrderOut(Order order) 
        {
            return new ROrderOut(order.Id.ToString(), order.Product.ToString("f"), order.Amount, order.Client.Id.ToString());
        }
        public static Client RClientInToClient(RClientIn rClientIn) //
        {
            Client client = new Client(rClientIn.Name, rClientIn.Address);
            client.Id = rClientIn.ClientID;
            return client;
        }
        public static Order ROrderInToOrder(ROrderIn rOrderIn, IDomainController dc) 
        {
            Product p = (Product)Enum.Parse(typeof(Product), rOrderIn.Product);
            Order order = new Order(p, rOrderIn.Amount, dc.GetClient(rOrderIn.ClientId));
            order.Id = rOrderIn.OrderId;
            return order;
        }
    }
}
