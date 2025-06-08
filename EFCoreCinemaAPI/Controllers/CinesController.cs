using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCinemaAPI.DTOs;
using EFCoreCinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreCinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public CinesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CineDto>>> Get()
        {
            // Retrieve all cinemas from the database
            var data = await _context.Cines
                                .ProjectTo<CineDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("cines-mas-cercanos")]
        public async Task<ActionResult> Get(double latitudeY, double longitudeX)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var myLocation = geometryFactory.CreatePoint(new Coordinate(longitudeX, latitudeY));
            var radius = 2000; // Radius in meters
            var cines = await _context.Cines
                                    .OrderBy(c => c.Location.Distance(myLocation))
                                    .Where(c => c.Location.IsWithinDistance(myLocation, radius))
                                    .Select(c =>new
                                    {
                                        Name = c.Name,
                                        Distance = Math.Round(
                                                c.Location.Distance(myLocation)
                                            )
                                    }).ToListAsync();
            return Ok(cines);
        }
    }
}
