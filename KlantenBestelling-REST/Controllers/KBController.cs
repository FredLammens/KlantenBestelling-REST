using DomainLayer;
using KlantenBestelling_REST.BaseClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace KlantenBestelling_REST.Controllers
{
    [Route("api/Klant")]
    [ApiController]
    public class KBController : ControllerBase
    {
        /// <summary>
        /// represents a type used for storage and methods of Repository.
        /// </summary>
        private readonly IDomainController dc;
        private readonly ILogger logger;

        /// <summary>
        /// Constructor for API controller
        /// </summary>
        /// <param name="dc">RepositoryController</param>
        /// <param name="loggerFactory">LoggingObject</param>
        public KBController(IDomainController dc, ILoggerFactory loggerFactory)
        {
            this.dc = dc;
            this.logger = loggerFactory.AddFile("KlantenServiceLogs.txt").CreateLogger("KlantenService");
        }
        #region ClientApi
        //Get: api/Klant
        /// <summary>
        /// Gets the Client and returns mapped to the output Rest Client format.
        /// </summary>
        /// <param name="id">clientId</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<RClientOut> GetClient(int id)
        {
            logger.LogInformation(11, "GetClient Called");
            try
            {
                return Ok(Mapper.ClientToRClientOut(dc.GetClient(id)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Adds the Client and returns createdAction with object mapped to the output Rest Client format.
        /// </summary>
        /// <param name="rClientIn">clientObject to add</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<RClientOut> PostClient([FromBody] RClientIn rClientIn)
        {
            logger.LogInformation(12, "PostClient Called");
            try
            {
                Client toAdd = Mapper.RClientInToClient(rClientIn);
                Client added = dc.AddClient(toAdd);
                return CreatedAtAction(nameof(GetClient), new { id = added.Id }, Mapper.ClientToRClientOut(added));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Adds the Client if not in database or updates if already in database and returns mapped to the output Rest Client format.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rClientIn"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<RClientOut> PutClient(int id, [FromBody] RClientIn rClientIn)
        {
            logger.LogInformation(13, "PutClient Called");
            try
            {
                if (rClientIn == null || rClientIn.ClientID != id)
                    return BadRequest();
                if (!dc.IsInClients(id))
                {
                    Client toAdd = Mapper.RClientInToClient(rClientIn);
                    Client added = dc.AddClient(toAdd);
                    return CreatedAtAction(nameof(GetClient), new { id = added.Id }, Mapper.ClientToRClientOut(added));
                }
                Client toUpdate = Mapper.RClientInToClient(rClientIn);
                dc.UpdateClient(toUpdate);
                RClientOut updatedClient = Mapper.ClientToRClientOut(dc.GetClient(toUpdate.Id));
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Deletes the Client and returns NoContent.
        /// </summary>
        /// <param name="id">id from client to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            logger.LogInformation(14, "DeleteClient Called");
            try
            {
                dc.DeleteClient(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion
        #region OrderApi
        /// <summary>
        /// Gets the Order and returns mapped to the output Rest Order format.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="orderId">orderId to Get</param>
        /// <returns></returns>
        [HttpGet("{clientId}/Bestelling/{orderId}")]
        [HttpHead]
        public ActionResult<ROrderOut> GetOrder(int clientId, int orderId)
        {
            logger.LogInformation(21, "GetOrder Called");
            try
            {
                return Ok(Mapper.OrderToROrderOut(dc.GetOrder(orderId)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Adds the Order and returns createdAction with object mapped to the output Rest Order format.
        /// </summary>
        /// <param name="clientId">clientId from order to add</param>
        /// <param name="rOrderIn">order to add</param>
        /// <returns></returns>
        [HttpPost("{clientId}/Bestelling")]
        public ActionResult<ROrderOut> PostOrder(int clientId, [FromBody] ROrderIn rOrderIn)
        {
            logger.LogInformation(22, "PostOrder Called");
            try
            {
                if (rOrderIn.ClientId != clientId)
                    return BadRequest("Input is invalid");
                if (!Enum.IsDefined(typeof(Product), rOrderIn.Product))
                    return NotFound("product not found.");
                Order toAdd = Mapper.ROrderInToOrder(rOrderIn, dc);
                Order added = dc.AddOrder(toAdd, clientId);
                return CreatedAtAction(nameof(GetClient), new { id = added.Id }, Mapper.OrderToROrderOut(added));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Adds the Order if not in database else updates the order and returns createdAction with object mapped to the output Rest Order format.
        /// </summary>
        /// <param name="clientId">clientId from Order</param>
        /// <param name="orderId">orderId</param>
        /// <param name="rOrderIn">Order to Add</param>
        /// <returns></returns>
        [HttpPut("{clientId}/Bestelling/{orderId}")]
        public IActionResult PutOrder(int clientId, int orderId, [FromBody] ROrderIn rOrderIn)
        {
            logger.LogInformation(23, "PutOrder Called");
            try
            {
                if (rOrderIn == null || rOrderIn.ClientId != clientId || rOrderIn.OrderId != orderId)
                    return BadRequest();
                if (!dc.IsInOrders(orderId))
                {
                    if (!Enum.IsDefined(typeof(Product), rOrderIn.Product))
                        return NotFound("product not found.");
                    Order toAdd = Mapper.ROrderInToOrder(rOrderIn, dc);
                    Order added = dc.AddOrder(toAdd, clientId);
                    return CreatedAtAction(nameof(GetClient), new { id = added.Id }, Mapper.OrderToROrderOut(added));
                }
                Order toUpdate = Mapper.ROrderInToOrder(rOrderIn, dc);
                dc.UpdateOrder(toUpdate);
                ROrderOut updatedOrder = Mapper.OrderToROrderOut(dc.GetOrder(toUpdate.Id));
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Delete the Order and returns noContent.
        /// </summary>
        /// <param name="clientId">clientId</param>
        /// <param name="orderId">orderId to delete</param>
        /// <returns></returns>
        [HttpDelete("{clientId}/Bestelling/{orderId}")]
        public IActionResult DeleteOrder(int clientId, int orderId)
        {
            logger.LogInformation(24, "DeleteOrder Called");
            try
            {
                dc.DeleteOrder(orderId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion
    }
}
