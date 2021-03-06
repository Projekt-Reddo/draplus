using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Dtos;
using DraplusApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using static Constant;

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
        [Authorize(Roles = SystemAuthority.ADMIN)]
        public async Task<ActionResult<PaginationResponse<IEnumerable<UserManageListDto>>>> GetAllUserManage([FromQuery] PaginationParameterDto pagination)
        {
            // Filter User Account
            var userFilter = Builders<User>.Filter.Eq("IsAdmin", false) | Builders<User>.Filter.Eq("IsAdmin", BsonNull.Value);

            var skipPage = (pagination.PageNumber - 1) * pagination.PageSize;

            // Get total User
            var totalUser = (await _userRepo.GetAll(filter: userFilter)).Count();

            var usersFromRepo = await _userRepo.GetAll(filter: userFilter, limit: pagination.PageSize, skip: skipPage);

            var users = _mapper.Map<IEnumerable<UserManageListDto>>(usersFromRepo);

            return Ok(new PaginationResponse<IEnumerable<UserManageListDto>>(totalUser, users));
        }

        /// <summary>
        /// Get Users with pagination 
        /// </summary>
        /// <returns>List of user and total User</returns>
        [HttpPut("ban/{userId}")]
        [Authorize(Roles = SystemAuthority.ADMIN)]
        public async Task<ActionResult> BanUser(string userId)
        {
            var userFilter = Builders<User>.Filter.Eq("Id", userId);
            var user = await _userRepo.GetByCondition(userFilter);

            if (user == null)
            {
                return NotFound(new ResponseDto(404, "User not found"));
            }

            user.IsBanned = !user.IsBanned;

            await _userRepo.Update(userId, user);

            return Ok();
        }
    }
}