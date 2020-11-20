using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer;
using KlantenBestelling_REST.BaseClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KlantenBestelling_REST.Controllers
{
    [Route("api/[controller]")]
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
        public ActionResult<RClient> Post([FromBody] Client client) 
        {
            Client added = dc.AddClient(client);
            return CreatedAtAction(nameof(Get),new { id = added.Id},Mapper.ClientToRClient(added));
        }
    }
}
