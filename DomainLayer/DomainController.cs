namespace DomainLayer
{
    public class DomainController : IDomainController
    {
        private readonly IUnitOfWork uow;

        public DomainController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
       
        public Client AddClient(Client client)
        {
            uow.Clients.AddClient(client);
            //if client has order add orders
            if (client.GetOrders().Count > 0)
            {
                uow.Orders.AddOrders(client.GetOrders());
            }
            //add client
            uow.Complete();
            Client addedClient = uow.Clients.GetClient(client.Name, client.Address);
            return addedClient;
        }
        
        public void DeleteClient(int id)
        {
            uow.Clients.DeleteClient(id);
            uow.Complete();
        }
       
        public Client GetClient(int id)
        {
            Client gettedClient = uow.Clients.GetClient(id);
            return gettedClient;
        }
       
        public Client UpdateClient(Client client)
        {
            uow.Clients.UpdateClient(client);
            uow.Complete();
            Client updatedClient = uow.Clients.GetClient(client.Name, client.Address);
            return updatedClient;
        }
       
        public Order AddOrder(Order order, int clientId)
        {
            Order updatedOrder;
            //al in databank => amounts op tellen en updaten
            if (uow.Orders.IsInOrders(order))
            {
                updatedOrder = uow.Orders.GetOrderWithoutId(order);
                updatedOrder.Amount += order.Amount;
                uow.Orders.UpdateOrder(updatedOrder);
                uow.Complete();
            }
            else
            {
                uow.Orders.AddOrder(order, clientId);
                uow.Complete();
                updatedOrder = uow.Orders.GetOrderWithoutId(order);
            }
            return updatedOrder;
        }
        
        public void DeleteOrder(int id)
        {
            uow.Orders.DeleteOrder(id);
            uow.Complete();
        }
      
        public Order GetOrder(int id)
        {
            return uow.Orders.GetOrder(id);
        }
       
        public Order UpdateOrder(Order order)
        {
            uow.Orders.UpdateOrder(order);
            uow.Complete();
            Order updatedOrder = uow.Orders.GetOrderWithoutId(order);
            return updatedOrder;
        }
        public bool IsInClients(int id)
        {
            return uow.Clients.IsInClients(id);
        }
        public bool IsInOrders(int id)
        {
            return uow.Orders.IsInOrders(id);
        }
    }
}
