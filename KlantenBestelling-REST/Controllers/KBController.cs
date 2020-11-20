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
            //Rclient werkt enkel met Name & Address?
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
                return Ok($"Client {id} removed");
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }
    }
}
