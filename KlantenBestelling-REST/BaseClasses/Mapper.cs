using DomainLayer;
using System;
using System.Collections.Generic;

namespace KlantenBestelling_REST.BaseClasses
{
    public class Mapper
    {
        /// <summary>
        /// Maps Client to RestClient out object.
        /// </summary>
        /// <param name="client">client object to map</param>
        /// <returns></returns>
        public static RClientOut ClientToRClientOut(Client client)
        {
            return new RClientOut(client.Id.ToString(), client.Name, client.Address, OrdersToROrdersOutIds(client.GetOrders()));
        }
        /// <summary>
        /// Maps Orders to OrderOutIdString (with URI).
        /// </summary>
        /// <param name="orders">orders to map</param>
        /// <returns></returns>
        private static List<string> OrdersToROrdersOutIds(IReadOnlyList<Order> orders) //iet smis met bestelling id => altijd 0
        {
            List<string> rorders = new List<string>();
            foreach (var order in orders)
            {
                rorders.Add(OrderToROrderOut(order).OrderId);
            }
            return rorders;
        }
        /// <summary>
        /// Maps Order To RestOrder out object
        /// </summary>
        /// <param name="order">order to map</param>
        /// <returns></returns>
        public static ROrderOut OrderToROrderOut(Order order)
        {
            return new ROrderOut(order.Id.ToString(), order.Product.ToString("f"), order.Amount, order.Client.Id.ToString());
        }
        /// <summary>
        /// Maps RestClientIn to Client
        /// </summary>
        /// <param name="rClientIn">RestClientIn to map</param>
        /// <returns></returns>
        public static Client RClientInToClient(RClientIn rClientIn) //
        {
            Client client = new Client(rClientIn.Name, rClientIn.Address);
            client.Id = rClientIn.ClientID;
            return client;
        }
        /// <summary>
        /// Maps RestOrderIn to Order object
        /// </summary>
        /// <param name="rOrderIn">RestOrderIn object</param>
        /// <param name="dc">Repository controller</param>
        /// <returns></returns>
        public static Order ROrderInToOrder(ROrderIn rOrderIn, IDomainController dc)
        {
            Product p = (Product)Enum.Parse(typeof(Product), rOrderIn.Product);
            Order order = new Order(p, rOrderIn.Amount, dc.GetClient(rOrderIn.ClientId));
            order.Id = rOrderIn.OrderId;
            return order;
        }
    }
}
