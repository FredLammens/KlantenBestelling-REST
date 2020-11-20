using DomainLayer;
using System.Collections.Generic;

namespace KlantenBestelling_REST.BaseClasses
{
    public class Mapper
    {
        public static RClientOut ClientToRClientOut(Client client) 
        {
            return new RClientOut(client.Id.ToString(), client.Name, client.Address, OrdersToROrdersOut(client.GetOrders()));
        }
        public static List<ROrderOut> OrdersToROrdersOut(IReadOnlyList<Order> orders) 
        {
            List<ROrderOut> rorders = new List<ROrderOut>();
            foreach (var order in orders)
            {
                rorders.Add(OrderToROrder(order));
            }
            return rorders;
        }
        public static ROrderOut OrderToROrder(Order order) 
        {
            return new ROrderOut(order.Id.ToString(), order.Product.ToString("f"), order.Amount, order.Client.Id.ToString());
        }
        public static Client RClientInToClient(RClientIn rClientIn) //
        {
            Client client = new Client(rClientIn.Name, rClientIn.Address);
            client.Id = rClientIn.ClientID;
            return client;
        }
    }
}
