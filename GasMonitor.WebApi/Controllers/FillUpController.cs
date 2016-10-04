using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class FillUpController : ApiController
    {
        private readonly GasMonitorContext _context;

        public FillUpController()
        {
            _context = new GasMonitorContext();
        }

        /// <summary>
        ///     Get the fill up records for a given vehicle
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <response code="404">Vehicle not found</response>
        /// <response code="200">OK</response>
        /// <returns></returns>
        [HttpGet]
        [Route("vehicles/{vehicleId}/fillups")]
        [ResponseType(typeof(IEnumerable<FillUpViewModel>))]
        public async Task<IHttpActionResult> Get(Guid vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Where(v => v.Id == vehicleId)
                .FirstOrDefaultAsync();

            if (vehicle == null)
                return NotFound();

            return Ok(Mapper.Map<IEnumerable<FillUpViewModel>>(vehicle.FillUps));
        }

        /// <summary>
        ///     Gets a single fill up record by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">Fill up not found</response>
        /// <response code="200">OK</response>
        /// <returns></returns>
        [HttpGet]
        [Route("fillups/{id}", Name = "Fillups.GetById")]
        [ResponseType(typeof(FillUpViewModel))]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            var fillup = await _context.FillUps
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

            if (fillup == null)
                return NotFound();

            return Ok(Mapper.Map<FillUpViewModel>(fillup));
        }

        /// <summary>
        ///     Log a fill up
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="cmd"></param>
        /// <response code="404">Vehicle not found</response>
        /// <response code="201">Created successfully</response>
        /// <returns></returns>
        [HttpPost]
        [Route("vehicles/{vehicleId}/fillups")]
        [ResponseType(typeof(FillUpViewModel))]
        public async Task<IHttpActionResult> Post(Guid vehicleId, CreateFillUpCommand cmd)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null)
                return NotFound();

            if (cmd.FilledAt == null)
                cmd.FilledAt = DateTime.UtcNow;

            var entity = Mapper.Map<FillUp>(cmd);
            entity.Id = Guid.NewGuid();
            entity.VehicleId = vehicleId;

            _context.FillUps.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("Fillups.GetById", new { id = entity.Id }, Mapper.Map<FillUpViewModel>(entity));
        }

        /// <summary>
        ///     Deletes a fill up
        /// </summary>
        /// <response code="404">Fillup not found</response>
        /// <response code="200">OK</response>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("fillups/{id}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var entity = await _context.FillUps.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.FillUps.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}