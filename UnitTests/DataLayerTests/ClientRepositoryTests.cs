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
            Client returnedClient = uow.Clients.AddClient(client);
            returnedClient.Id.ShouldBe(1);
        }
        [TestMethod]
        public void AddSameClientTwiceTest()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            Action act = () => uow.Clients.AddClient(client);
            act.ShouldThrow<Exception>().Message.ShouldBe("Client already in database.");
        }
        [TestMethod]
        public void GetClientNormalNoExceptions()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            Client returned = uow.Clients.GetClient(1);
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
            Client returned = uow.Clients.GetClient(1);
            uow.Orders.AddOrder(new Order(Product.Duvel, 10, returned),returned.Id);
            Client returnedWithOrders = uow.Clients.GetClient(1);
            returnedWithOrders.GetOrders().Count.ShouldBe(1);

        }

        [TestMethod]
        public void UpdateClientNormalNoExceptionsTest()
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            Client returned = uow.Clients.GetClient(1);
            returned.Name = "Shabalaba";
            Action act = () => uow.Clients.UpdateClient(returned);
            act.ShouldNotThrow();
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
            //uow.Clients.DeleteClient(1);
            Action act = () => uow.Clients.DeleteClient(1);
            act.ShouldNotThrow();
        }
    }
}
