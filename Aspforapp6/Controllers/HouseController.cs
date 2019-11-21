using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aspforapp6.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aspforapp6.Controllers
{
    [Route("api/[controller]")]
    public class HouseController : Controller
    {
        private readonly HouseContext _context;
        public HouseController(HouseContext context)
        {
            _context = context;
        }
        
        // GET: api/House
        [HttpGet]
        public async Task<ActionResult<IEnumerable<House>>> Gethouses()
        {
            return await _context.houses.ToListAsync();
        }

        

        // GET api/House/5
        [HttpGet("{HouseName}")]
        
        
        public async Task<ActionResult<House>> GetHouse(string id)
        {
            var house = await _context.houses.FindAsync(id);

            if (house == null)
            {
                return NotFound();
            }

            return house;
        }
        
        // POST api/House
        [HttpPost]
        public async Task<ActionResult<House>> PostHouse(House house)
        {
            _context.houses.Add(house);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HouseExists(house.HouseName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHouse", new { Hn = house.HouseName }, house);
        }

        // PUT api/House/5
        [HttpPut("{HouseName}")]
        public async Task<IActionResult> PutHouse(string Hn, House house)
        {
            if (Hn != house.HouseName)
            {
                return BadRequest();
            }

            _context.Entry(house).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HouseExists(Hn))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/House/5
        [HttpDelete("{HouseName}")]
        public async Task<ActionResult<House>> DeleteHouse(string Hn)
        {
            var house = await _context.houses.FindAsync(Hn);
            if (house == null)
            {
                return NotFound();
            }

            _context.houses.Remove(house);
            await _context.SaveChangesAsync();

            return house;
        }

        private bool HouseExists(string Hn)
        {
            return _context.houses.Any(e => e.HouseName == Hn);
        }
    }
}
