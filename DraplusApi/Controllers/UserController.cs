using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Dtos;
using DraplusApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DraplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserController(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Users with pagination 
        /// </summary>
        /// <returns>List of user and total User</returns>
        [HttpGet("")]
        public async Task<ActionResult<PaginationResponse<IEnumerable<UserManageListDto>>>> GetAllUserManage([FromQuery] PaginationParameterDto pagination)
        {
            // Filter User Account
            var userFilter = Builders<User>.Filter.Eq("IsAdmin", false);

            var skipPage = (pagination.PageNumber - 1) * pagination.PageSize;

            // Get total User
            var totalUser = (await _userRepo.GetAll(filter: userFilter)).Count();

            var usersFromRepo = await _userRepo.GetAll(filter: userFilter, limit: pagination.PageSize, skip: skipPage);

            var users = _mapper.Map<IEnumerable<UserManageListDto>>(usersFromRepo);

            return Ok(new PaginationResponse<IEnumerable<UserManageListDto>>(totalUser, users));
        }

    }
}