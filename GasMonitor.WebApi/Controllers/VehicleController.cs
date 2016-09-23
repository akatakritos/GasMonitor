using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class VehicleController : ApiController
    {
        private readonly GasMonitorContext _context;
        public VehicleController()
        {
            _context = new GasMonitorContext();
        }

        [HttpGet]
        [Route("owners/{ownerId}/vehicles")]
        [ResponseType(typeof(IEnumerable<VehicleViewModel>))]
        public async Task<IHttpActionResult> Get(Guid ownerId)
        {
            var owner = await _context.Owners.FindAsync(ownerId);
            if (owner == null)
                return NotFound();

            var vehicles = Mapper.Map<IEnumerable<VehicleViewModel>>(owner.Vehicles);
            return Ok(vehicles);
        }

        [HttpGet]
        [Route("owners/{ownerId}/vehicles/{vehicleId}")]
        [ResponseType(typeof(VehicleViewModel))]
        public async Task<IHttpActionResult> Get(Guid ownerId, Guid vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.Id == vehicleId && v.Owner.Id == ownerId)
                .SingleOrDefaultAsync();

            if (vehicle == null)
                return NotFound();

            return Ok(Mapper.Map<VehicleViewModel>(vehicle));
        }

        // PUT: api/Vehicle/5
        [HttpPut]
        [Route("owners/{ownerId}/vehicles/{vehicleId}")]
        [ResponseType(typeof(VehicleViewModel))]
        public async Task<IHttpActionResult> Put(Guid ownerId, Guid vehicleId, CreateVehicleCommand vehicle)
        {
            var owner = await _context.Owners.FindAsync(ownerId);
            if (owner == null)
                return NotFound();

            var entity = Mapper.Map<Vehicle>(vehicle);
            entity.Id = vehicleId;
            entity.OwnerId = owner.Id;

            _context.Vehicles.AddOrUpdate(entity);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<VehicleViewModel>(entity));
        }

        // DELETE: api/Vehicle/5
        [HttpDelete]
        [Route("owners/{ownerId}/vehicles/{vehicleId}")]
        public async Task<IHttpActionResult> Delete(Guid ownerId, Guid vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.Id == ownerId && v.OwnerId == ownerId)
                .FirstOrDefaultAsync();

            if (vehicle == null)
                return NotFound();

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
