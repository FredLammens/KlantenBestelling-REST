using DataLayer;
using DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DomainLayerTests
{
    [TestClass]
    public class DomainControllerTests
    {
        [TestMethod]
        public void AddClientNormalNoExceptionsTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            Client gettedClient = null;
            //
            Action act = () => gettedClient = dc.AddClient(client);
            act.ShouldNotThrow();
            gettedClient.Name.ShouldBe(client.Name);
            gettedClient.Address.ShouldBe(client.Address);

        }
        [TestMethod]
        public void DeleteClientNormalNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            Client gettedClient = dc.AddClient(client); ;
            //
            Action act = () => dc.DeleteClient(gettedClient.Id);
            act.ShouldNotThrow();
            act = () => dc.GetClient(gettedClient.Id);
            act.ShouldThrow<Exception>().Message.ShouldBe("Client not in database");
        }
        [TestMethod]
        public void GetClientNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            dc.AddClient(client);
            //
            Client gettedClient = null;
            Action act = () => gettedClient = dc.GetClient(1);
            act.ShouldNotThrow();
            gettedClient.Name.ShouldBe(client.Name);
            gettedClient.Address.ShouldBe(client.Address);
        }
        [TestMethod]
        public void UpdateClientNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            dc.AddClient(client);
            //
            Client gettedClient = dc.GetClient(1);
            gettedClient.Name = "Shabalaba";
            Action act = () => dc.UpdateClient(gettedClient);
            act.ShouldNotThrow();
            Client updatedClient = dc.GetClient(1);
            updatedClient.Name.ShouldBe("Shabalaba");
        }
        [TestMethod]
        public void AddOrderAlreadyInDatabaseTest()
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            dc.AddClient(client);
            //
            Client gettedClient = dc.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            dc.AddOrder(order, gettedClient.Id);
            Action act = () => dc.AddOrder(order, gettedClient.Id);
            act.ShouldNotThrow();
            Order changedOrder = dc.GetOrder(1);
            changedOrder.Amount.ShouldBe(20);
        }
        [TestMethod]
        public void AddOrderNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            dc.AddClient(client);
            //
            Client gettedClient = dc.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            Action act = () => dc.AddOrder(order, gettedClient.Id);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void DeleteOrderNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            dc.AddClient(client);
            //
            Client gettedClient = dc.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            dc.AddOrder(order, gettedClient.Id);
            Action act = () => dc.DeleteOrder(1);
            act.ShouldNotThrow();
            act = () => dc.GetOrder(1);
            act.ShouldThrow<Exception>().Message.ShouldBe("Order not in database.");
        }
        [TestMethod]
        public void GetOrderNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            dc.AddClient(client);
            //
            Client gettedClient = dc.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            dc.AddOrder(order, gettedClient.Id);
            Order gettedOrder = null;
            Action act = () => gettedOrder = dc.GetOrder(1);
            act.ShouldNotThrow();
            gettedOrder.Product.ShouldBe(order.Product);
            gettedOrder.Amount.ShouldBe(order.Amount);
            gettedOrder.Client.ShouldBe(order.Client);
        }
        [TestMethod]
        public void UpdateOrderNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Client client = new Client("TestName", "Test5678910");
            dc.AddClient(client);
            //
            Client gettedClient = dc.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            dc.AddOrder(order, gettedClient.Id);
            Order toUpdateOrder = dc.GetOrder(1);
            toUpdateOrder.Product = Product.Westmalle;
            Order updatedOrder = null;
            Action act = () => updatedOrder = dc.UpdateOrder(toUpdateOrder);
            act.ShouldNotThrow();
            updatedOrder.Product.ShouldBe(Product.Westmalle);
        }
        [TestMethod]
        public void IntegratieTest() 
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            DomainController dc = new DomainController(uow);
            Action act = () =>
            {
                Client client = new Client("TestName", "Test5678910");
                Client client2 = new Client("Test2", "Tralalalalallalalal");
                dc.AddClient(client);
                dc.AddClient(client2);
                Client gettedClient = dc.GetClient(2);
                dc.DeleteClient(gettedClient.Id);
                Client gettedClient2 = dc.GetClient(1);
                gettedClient2.Name = "Testeroo";
                dc.UpdateClient(gettedClient2);
                dc.AddClient(client2);
                Client gettedClient3 = dc.GetClient(3);
                dc.AddOrder(new Order(Product.Duvel, 10, gettedClient3), gettedClient3.Id);
            };
            act.ShouldNotThrow();
        }
    }
}
