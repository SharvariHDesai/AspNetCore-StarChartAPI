using System;
using StarChart.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using StarChart.Models;
using System.Linq;
namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]

    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
            {
                return NotFound();
            }
            else
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(p => p.OrbitedObjectId == id).ToList();

                return Ok(celestialObject);
            }
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.Where(p => p.Name == name);
            if (!celestialObject.Any())
            {
                return NotFound();
            }
            else
            {
                foreach (var item in celestialObject)

                {
                    item.Satellites = _context.CelestialObjects.Where(p => p.OrbitedObjectId == item.Id).ToList();
                }


                return Ok(celestialObject);
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObject = _context.CelestialObjects.ToList();
            foreach (var item in celestialObject)

            {
                item.Satellites = _context.CelestialObjects.Where(p => p.OrbitedObjectId == item.Id).ToList();
            }


            return Ok(celestialObject);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CelestialObject celestialObject)
        {

            var celestialObjects = _context.CelestialObjects;
            celestialObjects.Add(celestialObject);
            _context.SaveChanges();
            return CreatedAtRoute("GetById", new { id = celestialObject.Id }, celestialObject);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var bob = _context.CelestialObjects.Find(id);
            if (bob==null)
            {
                return NotFound();
            }
            else
            {
                bob.Name = celestialObject.Name;
                bob.OrbitalPeriod =celestialObject.OrbitalPeriod;
                bob.OrbitedObjectId = celestialObject.OrbitedObjectId;
                _context.Update(bob);
                _context.SaveChanges();
                return NoContent();

            }
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var bob = _context.CelestialObjects.Find(id);
            if (bob == null)
            {
                return NotFound();
            }
            else
            {
                bob.Name = name;
                  _context.Update(bob);
                _context.SaveChanges();
                return NoContent();

            }
        }

        [HttpDelete("{id}}")]
        public IActionResult Delete(int id)
        {
            var bob = _context.CelestialObjects.Where(p=>p.Id==id ||p.OrbitedObjectId==id);
            if (!bob.Any())
            {
                return NotFound();
            }
            else
            {
               
                _context.RemoveRange(bob);
                _context.SaveChanges();
                return NoContent();

            }
        }
    }

}
