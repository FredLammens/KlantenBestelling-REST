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
        public void AddClientNoExceptionsTest()
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
        public void GetClientNormal() 
        {
            UnitOfWork uow = new UnitOfWork(new KlantenBestellingenTestContext(false));
            Client client = new Client("TestName", "Test");
            uow.Clients.AddClient(client);
            Client returned = uow.Clients.GetClient(1);
            returned.Id.ShouldBe(1);
            returned.Name.ShouldBe(client.Name);
            returned.Address.ShouldBe(client.Address);
        }
    }
}
