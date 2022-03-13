using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Dtos;
using DraplusApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DraplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminControllers : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public AdminControllers(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AdminDto>> GetDashboard()
        {
            
            return Ok();
        }

    }
}