using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DomainLayer;
using KlantenBestelling_REST.BaseClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public ActionResult<RClient> Get(int id)
        {
            try
            {
                return Mapper.ClientToRClient(dc.GetClient(id));
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<RClient> Post([FromBody] RClient rclient) 
        {
            //Rclient werkt enkel met Name & Address?
            try
            {
                Client toAdd = Mapper.RClientToClient(rclient);//JsonConvert.DeserializeObject<RClient>(rclient)
                Client added = dc.AddClient(toAdd);
                return CreatedAtAction(nameof(Get), new { id = added.Id }, Mapper.ClientToRClient(added));
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RClient rclient) 
        {
            try
            {
                if (rclient == null || rclient.ClientId != id.ToString()) //klopt niet want clientId = http:localhost...
                    return BadRequest();
                if (!dc.IsInClients(id))
                {
                    Client toAdd = Mapper.RClientToClient(rclient);
                    Client added = dc.AddClient(toAdd);
                    return CreatedAtAction(nameof(Get), new { id = added.Id }, Mapper.ClientToRClient(added));
                }
                //RClient rclientDB = Mapper.ClientToRClient(dc.GetClient(id));
                Client toUpdate = Mapper.RClientToClient(rclient);
                toUpdate.Id = id;
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
