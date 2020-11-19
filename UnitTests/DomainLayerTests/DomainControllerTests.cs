using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DomainLayerTests
{
    [TestClass]
    class DomainControllerTests
    {
        //[TestMethod]
        //public void AddOrderAlreadyInDatabase()
        //{
        //    //init db
        //    UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
        //    Client client = new Client("TestName", "Test");
        //    uow.Clients.AddClient(client);
        //    uow.Complete();
        //    //
        //    Client gettedClient = uow.Clients.GetClient(1);
        //    Order order = new Order(Product.Duvel, 10, gettedClient);
        //    uow.Orders.AddOrder(order, gettedClient.Id);
        //    uow.Complete();
        //    Action act = () => uow.Orders.AddOrder(order, gettedClient.Id);
        //    act.ShouldNotThrow();
        //    uow.Complete();
        //    Order changedOrder = uow.Orders.GetOrder(1);
        //    changedOrder.Amount.ShouldBe(20);
        //}
    }
}
