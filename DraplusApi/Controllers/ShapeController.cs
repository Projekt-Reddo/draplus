using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DraplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeRepo _shapeRepo;
        private readonly IMapper _mapper;

        public ShapeController(IShapeRepo shapeRepo, IMapper mapper)
        {
            _shapeRepo = shapeRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Shape>> AddShape([FromBody] Shape shape)
        {
            if (shape == null)
            {
                return BadRequest();
            }

            var createdShape = await _shapeRepo.Add(shape);
            return Ok(createdShape);
        }
    }
}