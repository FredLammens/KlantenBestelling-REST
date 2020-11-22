using DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace UnitTests.DomainLayerTests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void ClientWithNullNameTest()
        {
            Client client = new Client("test", "tralalalala");
            Action act = () => client.Name = "";
            act.ShouldThrow<ArgumentException>().Message.ShouldBe("Name can't be null or empty.");
        }
        [TestMethod]
        public void ClientWithAdressSmallerThanTenTest()
        {
            Client client = new Client("test", "tralalalala");
            Action act = () => client.Address = "abcd";
            act.ShouldThrow<ArgumentException>().Message.ShouldBe("Adress must be bigger or equal to 10 characters");
            act = () => client.Address = "abcdefghij";
            act.ShouldThrow<ArgumentException>().Message.ShouldBe("Adress must be bigger or equal to 10 characters");
        }
        [TestMethod]
        public void ClientsOrdersCheckClientTest()
        {
            Client client = new Client("test", "tralalalala");
            Client clientTest = new Client("TestJ", "TestAdresje");
            Action act = () => client.AddOrder(new Order(Product.Westmalle, 10, clientTest));
            act.ShouldThrow<ArgumentException>().Message.ShouldBe("client is not the same.");
        }
        [TestMethod]
        public void ClientsOrdersDuplicateAddAmountTest()
        {
            Client client = new Client("test", "tralalalala");
            client.AddOrder(new Order(Product.Westmalle, 10, client));
            client.AddOrder(new Order(Product.Westmalle, 5, client));
            client.GetOrders()[0].Amount.ShouldBe(15);
        }
        [TestMethod]
        public void GetOrdersTest()
        {
            Client client = new Client("test", "tralalalala");
            client.AddOrder(new Order(Product.Westmalle, 10, client));
            client.GetOrders().Count.ShouldBe(1);
        }
        [TestMethod]
        public void RemoveOrderTest()
        {
            Client client = new Client("test", "tralalalala");
            Order order = new Order(Product.Westmalle, 10, client);
            client.AddOrder(order);
            client.RemoveOrder(order);
            client.GetOrders().Count.ShouldBe(0);
        }
    }
}
