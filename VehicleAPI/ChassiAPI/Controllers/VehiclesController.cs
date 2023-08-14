using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleAPI.Entities;
using VehicleAPI.Services;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleDbContext _context;

        public VehiclesController(VehicleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles.ToListAsync();

            foreach (var vehicle in vehicles)
            {
                string originalChassi = ChassiConverter.ConvertFromBinaryAndHex(vehicle.Chassi);
                vehicle.Chassi = originalChassi;
            }

            return vehicles;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            string decimalChassi = ChassiConverter.ConvertFromBinaryAndHex(vehicle.Chassi);
            vehicle.Chassi = decimalChassi.ToString();

            return vehicle;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            string chassiHexadecimal = ChassiConverter.ConvertToBinaryAndHex(vehicle.Chassi);

            vehicle.Chassi = chassiHexadecimal;
            _context.Entry(vehicle).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.Vehicles.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
          if (_context.Vehicles == null)
          {
              return Problem("Entity set 'VehicleDbContext.Vehicles'  is null.");
          }

            _context.Vehicles.Add(vehicle);

            string chassiHexadecimal = ChassiConverter.ConvertToBinaryAndHex(vehicle.Chassi);
            vehicle.Chassi = chassiHexadecimal;

            await _context.SaveChangesAsync();

            return Ok(await _context.Vehicles.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (_context.Vehicles == null)
            {
                return NotFound();
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(await _context.Vehicles.ToListAsync());
        }

        private bool VehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
