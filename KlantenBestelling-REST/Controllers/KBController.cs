using System;
using DomainLayer;
using KlantenBestelling_REST.BaseClasses;
using Microsoft.AspNetCore.Mvc;

namespace KlantenBestelling_REST.Controllers
{
    [Route("api/Klant")]
    [ApiController]
    public class KBController : ControllerBase
    {
        private IDomainController dc;

        public KBController(IDomainController dc)
        {
            this.dc = dc;
        }
        #region ClientApi
        //Get: api/Klant
        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<RClientOut> GetClient(int id)
        {
            try
            {
                return Mapper.ClientToRClientOut(dc.GetClient(id));
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<RClientOut> PostClient([FromBody] RClientIn rClientIn) 
        {
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
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RClientIn rClientIn) 
        {
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
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id) 
        {
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
        [HttpGet("{clientId}/Bestelling/{orderId}")]
        [HttpHead]
        public ActionResult<ROrderOut> GetOrder(int clientId, int orderId)
        {
            try
            {
                return Mapper.OrderToROrderOut(dc.GetOrder(orderId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("{clientId}/Bestelling")]
        public ActionResult<ROrderOut> PostOrder(int clientId,[FromBody] ROrderIn rOrderIn)
        {
            try
            {
                if (rOrderIn.ClientId != clientId)
                    return BadRequest("Input is invalid");
                if (!Enum.IsDefined(typeof(Product), rOrderIn.Product))
                    return NotFound("product not found.");
                Order toAdd = Mapper.ROrderInToOrder(rOrderIn, dc);
                Order added = dc.AddOrder(toAdd,clientId);
                return CreatedAtAction(nameof(GetClient), new { id = added.Id }, Mapper.OrderToROrderOut(added));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{clientId}/Bestelling/{orderId}")]
        public IActionResult PutOrder(int clientId, int orderId, [FromBody] ROrderIn rOrderIn)
        {
            try
            {
                if (rOrderIn == null || rOrderIn.ClientId != clientId || rOrderIn.OrderId != orderId)
                    return BadRequest();
                if (!dc.IsInOrders(orderId))
                {
                    if (rOrderIn.ClientId != clientId)
                        return BadRequest("Input is invalid");
                    if (!Enum.IsDefined(typeof(Product), rOrderIn.Product))
                        return NotFound("product not found.");
                    Order toAdd = Mapper.ROrderInToOrder(rOrderIn, dc);
                    Order added = dc.AddOrder(toAdd, clientId);
                    return CreatedAtAction(nameof(GetClient), new { id = added.Id }, Mapper.OrderToROrderOut(added));
                }
                Order toUpdate = Mapper.ROrderInToOrder(rOrderIn,dc);
                dc.UpdateOrder(toUpdate);
                ROrderOut updatedOrder = Mapper.OrderToROrderOut(dc.GetOrder(toUpdate.Id));
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion
    }
}
