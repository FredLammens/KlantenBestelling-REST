using DataLayer;
using DomainLayer;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenContext());
            //toevoegen klant
            //Client client = new Client("traal", "tralala");
            //uow.Clients.AddClient(client);
            //toevoegen order
            Client gettedClient = uow.Clients.GetClient(1); //    
            Order order = new Order(Product.Duvel, 5, gettedClient);
            gettedClient.AddOrder(order);
            uow.Orders.AddOrder(order);
            Console.WriteLine(gettedClient);
        }
    }
}
