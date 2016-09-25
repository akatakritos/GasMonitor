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
    public class VehicleController : ApiController
    {
        private readonly GasMonitorContext _context;
        public VehicleController()
        {
            _context = new GasMonitorContext();
        }

        /// <summary>
        /// Gets vehicles for an owner
        /// </summary>
        /// <param name="ownerId"></param>
        /// <response code="404">Owner does not exist</response>
        /// <response code="200">Successful</response>
        /// <returns></returns>
        [HttpGet]
        [Route("owners/{ownerId}/vehicles")]
        [ResponseType(typeof(IEnumerable<VehicleWithStats>))]
        public async Task<IHttpActionResult> GetByOwner(Guid ownerId)
        {
            var owner = await _context.Owners.FindAsync(ownerId);
            if (owner == null)
                return NotFound();

            var vehicles = await _context.Vehicles
                .Where(v => v.OwnerId == ownerId)
                .ProjectTo<VehicleWithStats>()
                .ToListAsync();

            return Ok(vehicles);
        }

        /// <summary>
        /// Gets details of a vehicle
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <response code="404">Vehicle not found</response>
        /// <response code="200">OK</response>
        /// <returns></returns>
        [HttpGet]
        [Route("vehicles/{vehicleId}", Name="Vehicles.GetById")]
        [ResponseType(typeof(VehicleWithStats))]
        public async Task<IHttpActionResult> GetById(Guid vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.Id == vehicleId)
                .ProjectTo<VehicleWithStats>()
                .SingleOrDefaultAsync();

            if (vehicle == null)
                return NotFound();

            return Ok(Mapper.Map<VehicleViewModel>(vehicle));
        }

        /// <summary>
        /// Creates a new vehicle
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="vehicle"></param>
        /// <response code="404">Owner not found</response>
        /// <response code="201">Vehicle created</response>
        /// <returns></returns>
        [HttpPost]
        [Route("owners/{ownerId}/vehicles")]
        [ResponseType(typeof(VehicleViewModel))]
        public async Task<IHttpActionResult> Post(Guid ownerId, CreateVehicleCommand vehicle)
        {
            var owner = await _context.Owners.FindAsync(ownerId);
            if (owner == null)
                return NotFound();

            var entity = Mapper.Map<Vehicle>(vehicle);
            entity.Id = Guid.NewGuid();
            entity.OwnerId = owner.Id;

            _context.Vehicles.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("Vehicles.GetById", new { id = entity.Id }, Mapper.Map<VehicleViewModel>(entity));
        }

        /// <summary>
        /// Update a vehicle
        /// </summary>
        /// <remarks>Properties not included in the PATCH body are not updated</remarks>
        /// <param name="vehicleId"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("vehicles/{vehicleId}")]
        [ResponseType(typeof(VehicleViewModel))]
        public async Task<IHttpActionResult> Update(Guid vehicleId, VehiclePatchCommand patch)
        {
            var entity = await _context.Vehicles.FindAsync(vehicleId);
            if (entity == null)
                return NotFound();

            Mapper.Map(patch, entity);

            await _context.SaveChangesAsync();
            return Ok(Mapper.Map<VehicleViewModel>(entity));

        }

        /// <summary>
        /// Delets a vehicle
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <response code="404">Vehicle does not exist</response>
        /// <response code="200">Successful</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("vehicles/{vehicleId}")]
        public async Task<IHttpActionResult> Delete(Guid vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.Id == vehicleId)
                .FirstOrDefaultAsync();

            if (vehicle == null)
                return NotFound();

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
