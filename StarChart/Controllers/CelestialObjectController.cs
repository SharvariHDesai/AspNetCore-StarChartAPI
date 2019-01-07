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
    
    public class CelestialObjectController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController( ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet ("{id:int}",Name ="GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if(celestialObject==null)
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
            var celestialObject = _context.CelestialObjects.Where(p=>p.Name==name);
            if (celestialObject == null)
            {
                return NotFound();
            }
            else
            {
                foreach(var item in celestialObject)

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
    }
}
