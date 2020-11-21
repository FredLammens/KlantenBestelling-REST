using KlantenBestelling_REST.Controllers;
using DomainLayer;
using Moq;
using Xunit;
using System;
using Microsoft.AspNetCore.Mvc;
using KlantenBestelling_REST.BaseClasses;

namespace RESTLayerTests
{
    public class ControllerTest
    {
        private readonly Mock<IDomainController> mockRepo;
        private readonly KBController kbController;
        
        public ControllerTest()
        {
            mockRepo = new Mock<IDomainController>();
            kbController = new KBController(mockRepo.Object);
        }
        [Fact]
        public void GETClient_UnknownID_ReturnsNotFound() 
        {
            mockRepo.Setup(repo => repo.GetClient(2))
                .Throws(new Exception("Client already in database."));
            var result = kbController.GetClient(2);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public void GETClient_UnknownID_ReturnsOkResult()
        {
            mockRepo.Setup(repo => repo.GetClient(2))
               .Returns(new Client("bart", "simpsonlaan 12"));
            var result = kbController.GetClient(2);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GETClient_UnknownID_ReturnsRClientOut()
        {

        }
        [Fact]
        public void POSTClient_ValidObject_ReturnsCreatedAtAction() 
        {
        }
    }
}
