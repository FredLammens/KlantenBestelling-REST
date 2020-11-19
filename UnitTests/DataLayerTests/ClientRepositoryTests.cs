using DataLayer;
using DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace UnitTests
{
    [TestClass]
    public class ClientRepositoryTests
    {
        [TestMethod]
        public void AddClientNormalNoExceptionsTest()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            Action act = () => uow.Clients.AddClient(client);
            act.ShouldNotThrow();
        }
        [TestMethod]
        public void AddClientCorrectIDReturnedTest()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            Client returnedClient = uow.Clients.GetClient(client.Name, client.Address);
            returnedClient.Id.ShouldBe(1);
        }
        [TestMethod]
        public void AddSameClientTwiceTest()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            Action act = () => uow.Clients.AddClient(client);
            act.ShouldThrow<Exception>().Message.ShouldBe("Client already in database.");
        }
        [TestMethod]
        public void GetClientWithIdNormalNoExceptions()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            Client returned = uow.Clients.GetClient(1);
            returned.Id.ShouldBe(1);
            returned.Name.ShouldBe(client.Name);
            returned.Address.ShouldBe(client.Address);
        }
        [TestMethod]
        public void GetClientWithNameAndAddressNormalNoExceptions()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            Client returned = uow.Clients.GetClient(client.Name,client.Address);
            returned.Id.ShouldBe(1);
            returned.Name.ShouldBe(client.Name);
            returned.Address.ShouldBe(client.Address);
        }
        [TestMethod]
        public void GetClientNotInDatabase()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Action act = () => uow.Clients.GetClient(1);
            act.ShouldThrow<Exception>().Message.ShouldBe("Client not in database");
        }
        [TestMethod]
        public void GetClientWithOrders() 
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            Client returned = uow.Clients.GetClient(1);
            uow.Orders.AddOrder(new Order(Product.Duvel, 10, returned),returned.Id);
            uow.Complete();
            Client returnedWithOrders = uow.Clients.GetClient(1);
            returnedWithOrders.GetOrders().Count.ShouldBe(1);

        }

        [TestMethod]
        public void UpdateClientNormalNoExceptionsTest()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            uow.Complete();
            Client returned = uow.Clients.GetClient(1);
            returned.Name = "Shabalaba";
            Action act = () => uow.Clients.UpdateClient(returned);
            act.ShouldNotThrow();
            uow.Complete();
            Client updatedReturned = uow.Clients.GetClient(1);
            updatedReturned.Name.ShouldBe("Shabalaba");
        }
        [TestMethod]
        public void UpdateClientNotInDatabaseTest() 
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            Action act = () => uow.Clients.UpdateClient(client);
            act.ShouldThrow<Exception>().Message.ShouldBe("Client not in database");
        }
        [TestMethod]
        public void DeleteClientNormalNoExceptions() 
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            uow.Clients.AddClient(new Client("TestName", "Test"));
            uow.Complete();
            Action act = () => uow.Clients.DeleteClient(1);
            act.ShouldNotThrow();
        }
        [TestMethod]
        public void DeleteClientNotInDataBase() 
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            //uow.Clients.DeleteClient(1);
            Action act = () => uow.Clients.DeleteClient(1);
            act.ShouldThrow<Exception>().Message.ShouldBe("Client not in database.");
        }
        [TestMethod]
        public void DeleteClientWithOrdersReturnException() 
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            uow.Clients.AddClient(new Client("TestName", "Test"));
            uow.Complete();
            Client returned = uow.Clients.GetClient(1);
            uow.Orders.AddOrder(new Order(Product.Duvel, 10, returned), returned.Id);
            uow.Complete();
            Action act = () => uow.Clients.DeleteClient(1);
            act.ShouldThrow<Exception>().Message.ShouldBe("Client has orders.");
        }
    }
}
