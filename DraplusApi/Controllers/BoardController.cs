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

        /// <summary>
        /// Add new board to database
        /// </summary>
        /// <param name="boardCreateDto">user info for creation of board</param>
        /// <returns>200 / 400 / 404</returns>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddBoard([FromBody] BoardCreateDto boardCreateDto)
        {
            // Validate input userId
            if (boardCreateDto.UserId == null)
            {
                return BadRequest(new ResponseDto(400, "UserId is required"));
            }

            var filter = Builders<User>.Filter.Eq("Id", boardCreateDto.UserId);
            var user = await _userRepo.GetByCondition(filter);

            if (user == null)
            {
                return NotFound(new ResponseDto(404, "User not found"));
            }

            // Create new chat room & board
            var createdBoard = await _boardRepo.Add(new Board
            {
                UserId = boardCreateDto.UserId,
            });

            return Ok(new ResponseDto(200, "Board created"));
        }

        /// <summary>
        /// Get all boards of user with userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of their board</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<BoardForListDto>>> GetUserBoards(string userId)
        {
            var filter = Builders<Board>.Filter.Eq("UserId", userId);

            var boardsFromRepo = await _boardRepo.GetAll(filter: filter);

            var boardForListDto = _mapper.Map<IEnumerable<BoardForListDto>>(boardsFromRepo);

            return Ok(boardForListDto);
        }

        /// <summary>
        /// Delete an board by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 / 404</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteBoard(string id)
        {
            var rs = await _boardRepo.Delete(id);

            if (rs == false)
            {
                return BadRequest(new ResponseDto(404, "Board not found"));
            }

            return Ok(new ResponseDto(200, "Board deleted"));
        }

        /// <summary>
        /// Update an board name by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 / 404</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateBoardName(string id, [FromBody] BoardForChangeNameDto boardForChangeNameDto)
        {
            var board = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", id));
            if (board == null)
            {
                return BadRequest(new ResponseDto(404, "Board not found"));
            }
            board.Name = boardForChangeNameDto.Name;
            var rs = await _boardRepo.Update(id, board);

            if (rs == false)
            {
                return BadRequest(new ResponseDto(404, "Change board name failed"));
            }

            return Ok(new ResponseDto(200, "Board Name Updated"));
        }
    }
}