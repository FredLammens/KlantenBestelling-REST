using DataLayer;
using DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.DataLayerTests
{
    [TestClass]
    public class OrderRepositoryTests
    {
        [TestMethod]
        public void AddOrderNormalNoExceptionsTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            //
            Client gettedClient = uow.Clients.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            Action act = () => uow.Orders.AddOrder(order,gettedClient.Id);
            act.ShouldNotThrow();
            uow.Complete();
            Client returned = uow.Clients.GetClient(1);
            returned.GetOrders().Count.ShouldBe(1);
            returned.GetOrders()[0].Product.ShouldBe(order.Product);
            returned.GetOrders()[0].Amount.ShouldBe(order.Amount);
            returned.GetOrders()[0].Client.Name.ShouldBe(client.Name);
            returned.GetOrders()[0].Client.Address.ShouldBe(client.Address);
        }

        [TestMethod]
        public void AddOrderNoClientIdProvided()
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            //
            Client gettedClient = uow.Clients.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            Action act = () => uow.Orders.AddOrder(order, client.Id);
            act.ShouldThrow<Exception>().Message.ShouldBe("No clientId provided.");
        }
        [TestMethod]
        public void GetOrderNormalNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            //
            Client gettedClient = uow.Clients.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            uow.Orders.AddOrder(order, gettedClient.Id);
            uow.Complete();
            Action act = () => uow.Orders.GetOrder(1);
            act.ShouldNotThrow();
            Order gettedOrder = uow.Orders.GetOrder(1);
            gettedOrder.Product.ShouldBe(order.Product);
            gettedOrder.Amount.ShouldBe(order.Amount);
            gettedOrder.Client.ShouldBe(order.Client);
        }
        [TestMethod]
        public void GetOrderNotInDataBaseTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            //
            Action act = () => uow.Orders.GetOrder(1);
            act.ShouldThrow<Exception>().Message.ShouldBe("Order not in database.");
        }

        [TestMethod]
        public void UpdateOrderNormalNoExceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            //
            Client gettedClient = uow.Clients.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            uow.Orders.AddOrder(order, gettedClient.Id);
            uow.Complete();
            Order getted = uow.Orders.GetOrder(1);
            getted.Product = Product.Westmalle;
            Action act = () => uow.Orders.UpdateOrder(getted);
            act.ShouldNotThrow();
            uow.Complete();
            Order gettedOrder = uow.Orders.GetOrder(1);
            gettedOrder.Product.ShouldBe(Product.Westmalle);
        }
        [TestMethod]
        public void UpdateOrderNotInDataseTest() 
        {

            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            //
            Order order = new Order(Product.Duvel, 10, client);
            Action act = () => uow.Orders.UpdateOrder(order);
            act.ShouldThrow<Exception>().Message.ShouldBe("Order not in database.");
        }
        [TestMethod]
        public void DeleteOrderNormalNoEXceptionTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            //
            Client gettedClient = uow.Clients.GetClient(1);
            Order order = new Order(Product.Duvel, 10, gettedClient);
            uow.Orders.AddOrder(order, gettedClient.Id);
            uow.Complete();
            Action act = () => uow.Orders.DeleteOrder(1);
            act.ShouldNotThrow();
            uow.Complete();
            act = () => uow.Orders.GetOrder(1);
            act.ShouldThrow<Exception>().Message.ShouldBe("Order not in database.");
        }
        [TestMethod]
        public void DeleteOrderNotInDatabaseTest() 
        {
            //init db
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            //
            Action act = () => uow.Orders.DeleteOrder(1);
            act.ShouldThrow<Exception>().Message.ShouldBe("Order not in database.");
        }
    }
}
