﻿using DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace UnitTests.DomainLayerTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void TestSetAmount()
        {
            Client client = new Client("test", "tralalalala");
            Action act = () => new Order(Product.Westmalle, -10, client);
            act.ShouldThrow<Exception>().Message.ShouldBe("Amount can't be empty.");
        }
        [TestMethod]
        public void TestSetClient()
        {
            Action act = () => new Order(Product.Westmalle, 10, null);
            act.ShouldThrow<Exception>().Message.ShouldBe("Client can't be empty");
        }

    }
}
