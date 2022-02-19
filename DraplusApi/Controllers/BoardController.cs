using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Dtos;
using DraplusApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DraplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepo _boardRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public BoardController(IBoardRepo boardRepo, IUserRepo userRepo, IMapper mapper)
        {
            _boardRepo = boardRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BoardReadDto>> AddBoard([FromBody] BoardCreateDto boardCreateDto)
        {
            // Validate input userId
            if (boardCreateDto.UserId == null)
            {
                return BadRequest(new ResponseDto(4004, "UserId is required"));
            }

            var filter = Builders<User>.Filter.Eq("Id", boardCreateDto.UserId);
            var user = await _userRepo.GetByCondition(filter);

            if (user == null)
            {
                return BadRequest(new ResponseDto(400, "User not found"));
            }

            // Create new chat room & board
            var createdBoard = await _boardRepo.Add(new Board
            {
                UserId = boardCreateDto.UserId,
            });

            return Ok(new ResponseDto(200, "Board created"));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<BoardForListDto>>> GetUserBoard(string userId)
        {
            var filter = Builders<Board>.Filter.Eq("UserId", userId);

            var boardsFromRepo = await _boardRepo.GetAll(filter: filter);

            var boardForListDto = _mapper.Map<IEnumerable<BoardForListDto>>(boardsFromRepo);

            return Ok(boardForListDto);
        }
    }
}