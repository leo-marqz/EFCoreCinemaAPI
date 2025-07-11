﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCinemaAPI.DTOs;
using EFCoreCinemaAPI.Models;
using EFCoreCinemaAPI.Models.Keyless;
using EFCoreCinemaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly IUpdateObservableCollectionService _updateObservableCollectionService;

        public CinesController(ApplicationDbContext context, IMapper mapper,
            IUpdateObservableCollectionService updateObservableCollectionService)
        {
            _context = context;
            _mapper = mapper;
            _updateObservableCollectionService = updateObservableCollectionService;
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

        [HttpGet("queries/sql/string/interpolation/{id:int}")]
        public async Task<ActionResult<Cine>> GetById(int id)
        {
            var cine = await _context.Cines
                                .FromSqlInterpolated($"SELECT * FROM Cines WHERE Id = {id}")
                                .Include(c => c.CineOffer)
                                .Include(c => c.CineRooms)
                                .Include(c => c.CineProfile)
                                .FirstOrDefaultAsync();

            if (cine is null)
            {
                return NotFound($"Cine with ID {id} not found.");
            }

            // Exclude the location from the response if not needed
            cine.Location = null; // Set to null if you don't want to return the location

            return Ok(cine);

        }

        [HttpGet("cines-sin-ubicacion")]
        public async Task<ActionResult<IEnumerable<CineWithoutLocation>>> GetCinesWithoutLocation()
        {
            //return await _context.Set<CineWithoutLocation>().ToListAsync();
            return await _context.CinesWithoutLocations.ToListAsync();
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

        [HttpPost]
        public async Task<ActionResult> Post() //create new cine
        {
            //Google Maps: first parameter is latitude, second is longitude
            var latitudeY = 13.68066; // Example latitude
            var longitudeX = -89.2680663; // Example longitude

            // Example of creating a new cinema with a location
            // 4326 is the SRID for WGS 84
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var myLocation = geometryFactory.CreatePoint(new Coordinate(longitudeX, latitudeY));

            var cine = new Cine
            {
                Name = "Cine con detalle #2",
                CineProfile = new CineProfile
                {
                    History = "Cine con historia",
                    Mision = "Nuestra misión es ofrecer la mejor experiencia cinematográfica.",
                    CodeOfEthics = "Código de ética del cine.",
                },
                Location = myLocation,
                CineOffer = new CineOffer
                {
                    DiscountPercentage = 15,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30)
                },
                CineRooms = new ObservableCollection<CineRoom>
                {
                    new CineRoom
                    {
                        Price = 30.50m,
                        CineRoomType = CineRoomType.CRT_CXC,
                        Currency = Currency.USD
                    },
                    new CineRoom
                    {
                        Price = 25.00m,
                        CineRoomType = CineRoomType.CRT_3D,
                        Currency = Currency.EUR
                    }
                }
            };

            _context.Add(cine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = cine.Id }, _mapper.Map<CineDto>(cine));
        }

        [HttpPost("create-cine-with-dto")]
        public async Task<ActionResult> Post(CreateCineDto request)
        {
            var cine = _mapper.Map<Cine>(request);
            _context.Add(cine);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cine.Id }, _mapper.Map<CineDto>(cine));
        }

        [HttpGet("one/{id:int}")]
        public async Task<ActionResult<CineDto>> Get(int id)
        {
            // Retrieve a specific cinema by ID
            var cine = await _context.Cines
                                .Include(c => c.CineOffer)
                                .Include(c => c.CineRooms)
                                .FirstOrDefaultAsync(c => c.Id == id);
            if (cine is null)
            {
                return NotFound();
            }

            cine.Location = null; // Exclude the location from the response if not needed

            return Ok(cine);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CreateCineDto createCineDto)
        {
            Cine cinedb = await _context.Cines.AsTracking()
                            .Include((cn)=> cn.CineOffer)
                            .Include((cn) => cn.CineRooms)
                            .FirstOrDefaultAsync(c => c.Id == id);
            if(cinedb is null)
            {
                return NotFound();
            }

            // Update the cinema's properties
            cinedb = _mapper.Map(createCineDto, cinedb);

            _updateObservableCollectionService
                .Update(cinedb.CineRooms, createCineDto.CineRooms);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CineDto>(cinedb));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var cine = await _context.Cines
                //el include nos permite traer y modificar la oferta dejando en nullo
                //la relacion opcional CineOffer, si no se usa el include no se puede modificar la oferta
                //esto va en conjunto con uso de la propiedad nullable CineId en CineOffer
                .Include(c => c.CineOffer)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cine is null)
            {
                return NotFound($"Cine with ID {id} not found.");
            }
            _context.Remove(cine);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
