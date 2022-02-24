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
        private readonly IMapper _mapper;

        public BoardController(IBoardRepo boardRepo, IMapper mapper)
        {
            _boardRepo = boardRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BoardReadDto>> AddBoard([FromBody] BoardCreateDto boardCreateDto)
        {
            if (boardCreateDto == null)
            {
                return BadRequest();
            }

            var boardModel = _mapper.Map<Board>(boardCreateDto);

            var createdBoard = await _boardRepo.Add(boardModel);

            return Ok(createdBoard);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardReadDto>>> GetAllBoards()
        {
            var boards = await _boardRepo.GetAll();

            var boardReadDto = _mapper.Map<IEnumerable<BoardReadDto>>(boards);

            return Ok(boardReadDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoardReadDto>> GetBoard(string id)
        {
            
            var board = await _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id",id));
            var boardread = _mapper.Map<BoardReadDto>(board);
            return Ok(boardread);
        }
        
    }
}