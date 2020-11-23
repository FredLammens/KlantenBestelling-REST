using DomainLayer;
using KlantenBestelling_REST.BaseClasses;
using KlantenBestelling_REST.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace RESTLayerTests
{
    public class ControllerTest
    {
        private readonly Mock<IDomainController> mockRepo;
        private readonly KBController kbController;

        public ControllerTest()
        {
            mockRepo = new Mock<IDomainController>();
            kbController = new KBController(mockRepo.Object, new LoggerFactory());
        }
        #region ClientTests
        [Fact]
        public void GETClient_UnknownID_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.GetClient(2))
                .Throws(new Exception("Client not in database."));
            var result = kbController.GetClient(2);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public void GETClient_CorrectID_ReturnsOkResult()
        {
            mockRepo.Setup(repo => repo.GetClient(2))
                    .Returns(new Client("bart", "simpsonlaan 12"));
            var result = kbController.GetClient(2);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GETClient_CorrectID_ReturnsRClientOut()
        {
            Client c = new Client("bart", "simpsonlaan 12");
            c.Id = 2;
            mockRepo.Setup(repo => repo.GetClient(2))
                    .Returns(c);
            var result = kbController.GetClient(2).Result as OkObjectResult;

            Assert.IsType<RClientOut>(result.Value);
            Assert.Equal(Constants.URI + 2, (result.Value as RClientOut).ClientIdString);
            Assert.Equal(c.Name, (result.Value as RClientOut).Name);
            Assert.Equal(c.Address, (result.Value as RClientOut).Address);
            Assert.Equal(c.GetOrders().Count, (result.Value as RClientOut).OrdersIds.Count);
        }
        [Fact]
        public void POSTClient_ValidObject_ReturnsCreatedAtAction()
        {
            RClientIn client = new RClientIn("trala", "simpsonlaan 12");
            Client clientRepo = new Client(client.Name, client.Address);
            mockRepo.Setup(repo => repo.AddClient(clientRepo)).Returns(clientRepo); ;
            var response = kbController.PostClient(client);
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }
        [Fact]
        public void POSTClient_ValidObject_ReturnsCorrectItem()
        {
            RClientIn c = new RClientIn("trala", "simpsonlaan 12");
            c.ClientID = 2;
            Client clientRepo = new Client(c.Name, c.Address);
            clientRepo.Id = 2;
            mockRepo.Setup(repo => repo.AddClient(clientRepo)).Returns(clientRepo);
            var tussenResponse = kbController.PostClient(c);
            var response = tussenResponse.Result as CreatedAtActionResult;
            var item = response.Value as RClientOut;
            Assert.IsType<RClientOut>(item);
            Assert.Equal(Constants.URI + 2, item.ClientIdString);
            Assert.Equal(c.Name, item.Name);
            Assert.Equal(c.Address, item.Address);
        }
        [Fact]
        public void POSTClient_InValidObject_ReturnsNotFound()
        {
            RClientIn c = new RClientIn("trala", "simpsonlaan 12");
            kbController.ModelState.AddModelError("format error", "int expected");
            var response = kbController.PostClient(c).Result;
            Assert.IsType<NotFoundObjectResult>(response);
        }
        [Fact]
        public void PUTClient_InValidObject_ReturnsBadRequest()
        {
            RClientIn c = new RClientIn("trala", "simpsonlaan 12");
            c.ClientID = 5;
            var response = kbController.PutClient(2, c);
            Assert.IsType<BadRequestResult>(response.Result);
        }
        [Fact]
        public void PUTClient_InValidObject_ReturnsNotFound()
        {
            RClientIn c = new RClientIn("trala", "simpsonlaan 12");
            c.ClientID = 2;
            kbController.ModelState.AddModelError("simulated exception", "duno client already in db");
            var response = kbController.PutClient(2, c).Result;
            Assert.IsType<NotFoundObjectResult>(response);
        }
        [Fact]
        public void PUTClient_InValidId_ReturnsNotFound()
        {
            RClientIn c = new RClientIn("trala", "simpsonlaan 12");
            c.ClientID = 2;
            Client clientRepo = new Client(c.Name, c.Address);
            clientRepo.Id = 2;
            mockRepo.Setup(repo => repo.UpdateClient(clientRepo)).Throws(new Exception("Client not in DB."));
            var response = kbController.PutClient(2, c).Result;
            Assert.IsType<NotFoundObjectResult>(response);
        }
        [Fact]
        public void PUTClient_ValidObject_ReturnsCorrectItem()
        {
            RClientIn c = new RClientIn("trala", "simpsonlaan 12");
            c.ClientID = 2;
            Client clientRepo = new Client(c.Name, c.Address);
            clientRepo.Id = 2;
            mockRepo.Setup(repo => repo.AddClient(clientRepo)).Returns(clientRepo);
            var tussenResponse = kbController.PutClient(2, c);
            var response = tussenResponse.Result as CreatedAtActionResult;
            var item = response.Value as RClientOut;
            Assert.IsType<RClientOut>(item);
            Assert.Equal(Constants.URI + 2, item.ClientIdString);
            Assert.Equal(c.Name, item.Name);
            Assert.Equal(c.Address, item.Address);
        }
        [Fact]
        public void DeleteClient_ValidObject_ReturnsNoContent()
        {
            var result = kbController.DeleteClient(1);
            Assert.IsType<NoContentResult>(result);

        }
        [Fact]
        public void DeleteClient_InValidObject_ReturnsNotFound()
        {
            kbController.ModelState.AddModelError("simulated exception", "duno client not in db");
            var result = kbController.DeleteClient(1);
            Assert.IsType<NoContentResult>(result);
        }
        #endregion
        #region OrderTests
        [Fact]
        public void GETOrder_UnknownID_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.GetOrder(1))
                .Throws(new Exception("Order not in database."));
            var result = kbController.GetOrder(2, 1);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public void GETOrder_CorrectID_ReturnsOkResult()
        {
            Client c = new Client("bart", "simpsonlaan 12");
            c.Id = 2;
            Order o = new Order(Product.Duvel, 10, c);
            o.Id = 1;
            mockRepo.Setup(repo => repo.GetOrder(1))
                    .Returns(o);
            var result = kbController.GetOrder(2, 1);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GETOrder_CorrectID_ReturnsROrderOut()
        {
            Client c = new Client("bart", "simpsonlaan 12");
            c.Id = 2;
            Order o = new Order(Product.Duvel, 10, c);
            o.Id = 1;
            mockRepo.Setup(repo => repo.GetOrder(1))
                    .Returns(o);
            var result = kbController.GetOrder(2, 1).Result as OkObjectResult;
            Assert.IsType<ROrderOut>(result.Value);
            Assert.Equal(Constants.URI + c.Id, (result.Value as ROrderOut).ClientId);
            Assert.Equal(Constants.URI + c.Id + "/Bestelling/" + o.Id, (result.Value as ROrderOut).OrderId);
            Assert.Equal(o.Amount, (result.Value as ROrderOut).Amount);
            Assert.Equal(o.Product, Enum.Parse(typeof(Product), (result.Value as ROrderOut).Product));
        }
        [Fact]
        public void POSTOrder_ValidObject_ReturnsCreatedAtAction()
        {
            Client c = new Client("bart", "simpsonlaan 12");
            c.Id = 2;
            Order o = new Order(Product.Duvel, 10, c);
            Order gettedOrder = new Order(Product.Duvel, 10, c);
            gettedOrder.Client = c;
            gettedOrder.Id = 1;
            ROrderIn oi = new ROrderIn(c.Id, "Duvel", o.Amount);
            mockRepo.Setup(repo => repo.GetClient(oi.ClientId)).Returns(c);
            mockRepo.Setup(repo => repo.AddOrder(o, c.Id)).Returns(gettedOrder);
            var response = kbController.PostOrder(oi.ClientId, oi);
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }
        [Fact]
        public void POSTOrder_ValidObject_ReturnsCorrectItem()
        {
            Client c = new Client("bart", "simpsonlaan 12");
            c.Id = 2;
            Order o = new Order(Product.Duvel, 10, c);
            Order gettedOrder = new Order(Product.Duvel, 10, c);
            gettedOrder.Client = c;
            gettedOrder.Id = 1;
            ROrderIn oi = new ROrderIn(c.Id, "Duvel", o.Amount) { OrderId = 1 };
            mockRepo.Setup(repo => repo.GetClient(oi.ClientId)).Returns(c);
            mockRepo.Setup(repo => repo.AddOrder(o, c.Id)).Returns(gettedOrder);

            var response = kbController.PostOrder(oi.ClientId, oi).Result as CreatedAtActionResult;
            Assert.Equal(oi.Amount, (response.Value as ROrderOut).Amount);
            Assert.Equal(Constants.URI + oi.ClientId, (response.Value as ROrderOut).ClientId);//
            Assert.Equal(Constants.URI + oi.ClientId +  "/Bestelling/"  +  oi.OrderId, (response.Value as ROrderOut).OrderId);
            Assert.Equal(oi.Product, (response.Value as ROrderOut).Product);
        }
        [Fact]
        public void POSTOrder_InValidProduct_ReturnsNotFound()
        {
            ROrderIn o = new ROrderIn(2,"Duvel", 10);
            kbController.ModelState.AddModelError("format error", "int expected");
            var response = kbController.PostOrder(2,o).Result;
            Assert.IsType<NotFoundObjectResult>(response);
        }
        [Fact]
        public void POSTOrder_InValidClientId_ReturnsBadRequest()
        {
            ROrderIn o = new ROrderIn(2, "Duvel", 10);
            var response = kbController.PostOrder(3, o).Result;
            Assert.IsType<BadRequestObjectResult>(response);
        }
        [Fact]
        public void PUTOrder_InValidObjectNull_ReturnsBadRequest()
        {
            var response = kbController.PutClient(2, null).Result;
            Assert.IsType<BadRequestResult>(response);
        }
        [Fact]
        public void PUTOrder_InValidObjectProduct_ReturnsNotFound()
        {
            ROrderIn o = new ROrderIn(2, "Duff", 10);
            o.OrderId = 1;
            mockRepo.Setup(repo => repo.IsInOrders(1)).Returns(false);
            var response = kbController.PutOrder(2,1,o);
            Assert.IsType<NotFoundObjectResult>(response);
        }
        [Fact]
        public void PUTOrder_InValidObjectException_ReturnsNotFound()
        {
            ROrderIn o = new ROrderIn(2, "Duff", 10);
            o.OrderId = 1;
            mockRepo.Setup(repo => repo.IsInOrders(1)).Throws(new Exception());
            var response = kbController.PutOrder(2, 1, o);
            Assert.IsType<NotFoundObjectResult>(response);
        }
        [Fact]
        public void PUTOrder_ValidObject_ReturnsCorrectItem()
        {
            Client c = new Client("bart", "simpsonlaan 12");
            c.Id = 2;
            Order o = new Order(Product.Duvel, 10, c);
            o.Id = 1;
            Order gettedOrder = new Order(Product.Duvel, 10, c);
            gettedOrder.Client = c;
            gettedOrder.Id = 1;
            ROrderIn oi = new ROrderIn(c.Id, "Duvel", o.Amount) { OrderId = 1 };
            mockRepo.Setup(repo => repo.GetClient(oi.ClientId)).Returns(c);
            mockRepo.Setup(repo => repo.UpdateOrder(o)).Returns(gettedOrder);
            mockRepo.Setup(repo => repo.IsInOrders(o.Id)).Returns(true);
            mockRepo.Setup(repo => repo.GetOrder(o.Id)).Returns(o);
            var response = kbController.PutOrder(oi.ClientId,o.Id, oi) as OkObjectResult;
            Assert.Equal(oi.Amount, (response.Value as ROrderOut).Amount);
            Assert.Equal(Constants.URI + oi.ClientId, (response.Value as ROrderOut).ClientId);//
            Assert.Equal(Constants.URI + oi.ClientId + "/Bestelling/" + oi.OrderId, (response.Value as ROrderOut).OrderId);
            Assert.Equal(oi.Product, (response.Value as ROrderOut).Product);
        }
        [Fact]
        public void DeleteOrder_ValidObject_ReturnsNoContent()
        {
            var result = kbController.DeleteOrder(1,1);
            Assert.IsType<NoContentResult>(result);

        }
        [Fact]
        public void DeleteOrder_InValidObject_ReturnsNotFound()
        {
            kbController.ModelState.AddModelError("simulated exception", "duno client not in db");
            var result = kbController.DeleteOrder(1,1);
            Assert.IsType<NoContentResult>(result);
        }
        #endregion

    }
}
