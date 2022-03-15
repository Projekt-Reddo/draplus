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
    public class AdminControllers : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IBoardRepo _boardRepo;

        public AdminControllers(IUserRepo userRepo, IMapper mapper, IBoardRepo boardRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _boardRepo = boardRepo;
        }

        [HttpGet]
        public async Task<ActionResult<AdminDto>> GetDashboardDetails()
        {
            var userList = await _userRepo.GetAll();
            var totalMem = userList.Count();
            var boardList = await _boardRepo.GetAll();
            var totalBoard = boardList.Count();

            var filterNewMem = Builders<User>.Filter.Gt("CreatedAt",DateTime.Now.AddDays(-7));
            var newMems = await _userRepo.GetAll(filter: filterNewMem);
            var newMemCount = newMems.Count();

            var filterNewBoard = Builders<Board>.Filter.Gt("CreatedAt",DateTime.Now.AddDays(-7));
            var newBoards = await _boardRepo.GetAll(filterNewBoard);
            var newBoardsCount = newBoards.Count();

            AdminDto returnValue = new AdminDto
            {
                NewAccount = newMemCount,
                NewBoard = newBoardsCount,
                TotalAccount = totalMem,
                TotalBoard = totalBoard,
            };
            return Ok(returnValue);
        }

        [HttpGet("/chart")]
        public async Task<ActionResult<IEnumerable<BarChartDto>>> GetDashboardChart(string kindOfTime)
        {
            List<BarChartDto> barChartReturn = new List<BarChartDto>();
            switch (kindOfTime)
            {
                case "Day" :

                    break;
                case "Month":

                    break;
                case "Year":

                    break;
                default: 
                    break;
            }

            return null;
        }
    }
}