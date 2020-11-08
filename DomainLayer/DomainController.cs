using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class DomainController
    {
        private readonly IUnitOfWork uow;

        public DomainController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public Client AddClient(Client client)
        {
            return uow.Clients.AddClient(client);
        }
        public void DeleteClient(int id)
        {
            uow.Clients.DeleteClient(id);
        }
        public Client GetClient(int id)
        {
            return uow.Clients.GetClient(id);
        }
        public Client UpdateClient(Client client)
        {
            return uow.Clients.UpdateClient(client);
        }
        public Order AddOrder(Order order, int clientID)
        {

            return uow.Orders.AddOrder(order, clientID);
        }
        public void DeleteOrder(int id, int clientId)
        {
            uow.Orders.DeleteOrder(id, clientId);
        }
        public Order GetOrder(int id, int clientId)
        {
            return uow.Orders.GetOrder(id, clientId);
        }
        public Order UpdateOrder(Order order, int clientId)
        {
            return uow.Orders.UpdateOrder(order, clientId);
        }
    }
}
