using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using AutoMapper;

using GasMonitor.Core.Database;
using GasMonitor.Core.Models;
using GasMonitor.WebApi.Models;

namespace GasMonitor.WebApi.Controllers
{
    public class OwnerController : ApiController
    {
        private readonly GasMonitorContext _context;

        public OwnerController()
        {
            _context = new GasMonitorContext();
        }

        [ResponseType(typeof(OwnerViewModel))]
        [Route("owners/{id}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
                return NotFound();

            return Ok(Mapper.Map<OwnerViewModel>(owner));
        }

        [HttpPut]
        [Route("owners/{id}")]
        public async Task<IHttpActionResult> Put(Guid id, CreateOwnerCommand owner)
        {
            var entity = Mapper.Map<Owner>(owner);
            entity.Id = id;

            _context.Owners.AddOrUpdate(entity);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("owners/{id}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var entity = await _context.Owners.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.Owners.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
