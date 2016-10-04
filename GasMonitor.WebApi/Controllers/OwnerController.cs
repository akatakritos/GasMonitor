using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using AutoMapper;
using AutoMapper.QueryableExtensions;

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

        /// <summary>
        ///     Gets the details of an owner
        /// </summary>
        /// <param name="id">The identifier of the vehicle owner</param>
        /// <response code="404">Owner does not exist</response>
        /// <response code="200">Successful</response>
        /// <returns></returns>
        [ResponseType(typeof(OwnerWithVehicles))]
        [Route("owners/{id}", Name = "Owners.Get")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var owner = await _context.Owners
                .Where(o => o.Id == id)
                .ProjectTo<OwnerWithVehicles>()
                .FirstOrDefaultAsync();

            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        /// <summary>
        ///     Adds a new Owner. Check the Location header for its url, or parse the Id from the response body
        /// </summary>
        /// <param name="owner"></param>
        /// <response code="201">Successfully created</response>
        /// <returns></returns>
        [HttpPost]
        [Route("owners")]
        public async Task<IHttpActionResult> Post(CreateOwnerCommand owner)
        {
            var entity = Mapper.Map<Owner>(owner);
            entity.Id = Guid.NewGuid();

            _context.Owners.Add(entity);

            await _context.SaveChangesAsync();

            return CreatedAtRoute("Owners.Get", new { id = entity.Id }, Mapper.Map<OwnerViewModel>(entity));
        }

        /// <summary>
        ///     Deletes the owner identified by the URL
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">Owner does not exist</response>
        /// <response code="200">Successfully deleted</response>
        /// <returns></returns>
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