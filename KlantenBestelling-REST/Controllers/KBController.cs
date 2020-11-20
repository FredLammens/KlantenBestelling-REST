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
        [HttpGet("{id}", Name = "Get")]
        [HttpHead("{id}")]
        public ActionResult<RClientOut> Get(int id)
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
        public ActionResult<RClientOut> Post([FromBody] RClientIn rClientIn) 
        {
            //Rclient werkt enkel met Name & Address?
            try
            {
                Client toAdd = Mapper.RClientInToClient(rClientIn);
                Client added = dc.AddClient(toAdd);
                return CreatedAtAction(nameof(Get), new { id = added.Id }, Mapper.ClientToRClientOut(added));
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
                    return CreatedAtAction(nameof(Get), new { id = added.Id }, Mapper.ClientToRClientOut(added));
                }
                Client toUpdate = Mapper.RClientInToClient(rClientIn);
                dc.UpdateClient(toUpdate);
                return new NoContentResult();
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }
    }
}
