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
using MongoDB.Driver;
using static Constant;

namespace DraplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = SystemAuthority.ADMIN)]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IBoardRepo _boardRepo;
        private readonly ISignInRepo _signInRepo;

        public AdminController(IUserRepo userRepo, IMapper mapper, IBoardRepo boardRepo, ISignInRepo signInRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _boardRepo = boardRepo;
            _signInRepo = signInRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="barTime"></param>
        /// <param name="lineTime"></param>
        /// <returns></returns>
        [HttpGet("/{barTime}/{lineTime}")]
        public async Task<ActionResult<DashboardDTO>> AdminDashBoardDetails(string barTime, string lineTime)
        {
            var returnDashBoard = new DashboardDTO();
            // Admin DTO
            var userList = await _userRepo.GetAll();
            var totalMem = userList.Count();
            var boardList = await _boardRepo.GetAll();
            var totalBoard = boardList.Count();

            var filterNewMem = Builders<User>.Filter.Gt("CreatedAt", DateTime.Now.AddDays(-7));
            var newMems = await _userRepo.GetAll(filter: filterNewMem);
            var newMemCount = newMems.Count();

            var filterNewBoard = Builders<Board>.Filter.Gt("CreatedAt", DateTime.Now.AddDays(-7));
            var newBoards = await _boardRepo.GetAll(filterNewBoard);
            var newBoardsCount = newBoards.Count();

            AdminDto returnAdmin = new AdminDto
            {
                NewAccount = newMemCount,
                NewBoard = newBoardsCount,
                TotalAccount = totalMem,
                TotalBoard = totalBoard,
            };
            returnDashBoard.adminDto = returnAdmin;

            // Bar Chart DTO
            List<BarChartDto> barChartReturn = new List<BarChartDto>();
            switch (barTime)
            {
                case "Day":
                    DateTime dayCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    for (int i = 10; i >= 0; i--)
                    {
                        var filter = Builders<SignIn>.Filter.Eq("At", dayCount.AddDays(-i));
                        var day = await _signInRepo.GetByCondition(filter: filter);
                        int value;
                        if (day == null)
                        {
                            value = 0;
                        }
                        else
                        {
                            value = day.Times;
                        }
                        barChartReturn.Add(
                            new BarChartDto
                            {
                                Country = "Day " + (i + 1),
                                Login = value,
                                LoginColor = "hsl(205, 70%, 50%)"
                            }
                        );
                    }
                    break;
                case "Month":
                    DateTime monthCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    for (int i = 10; i >= 0; i--)
                    {
                        var day = await _signInRepo.GetAll();
                        var timeGet = day.Where(d => d.At >= monthCount.AddMonths(-i) && d.At <= monthCount.AddMonths(-i).AddDays(DateTime.DaysInMonth(monthCount.Year, monthCount.Month)));
                        int timecount = 0;
                        if (timeGet == null)
                        {
                            timecount = 0;
                        }
                        else
                        {
                            foreach (var item in timeGet)
                            {
                                timecount += item.Times;
                            }
                        }
                        barChartReturn.Add(
                            new BarChartDto
                            {
                                Country = "Month " + i,
                                Login = timecount,
                                LoginColor = "hsl(205, 70%, 50%)"
                            }
                        );
                    }
                    break;
                case "Year":
                    DateTime yearCount = new DateTime(DateTime.Now.Year, 1, 1);
                    for (int i = 10; i >= 0; i--)
                    {
                        var day = await _signInRepo.GetAll();
                        var timeGet = day.Where(d => d.At >= yearCount.AddYears(-i) && d.At <= new DateTime(yearCount.Year - i, 12, 31));
                        int timecount = 0;
                        if (timeGet == null)
                        {
                            timecount = 0;
                        }
                        else
                        {
                            foreach (var item in timeGet)
                            {
                                timecount += item.Times;
                            }
                        }
                        barChartReturn.Add(
                            new BarChartDto
                            {
                                Country = "Year " + i,
                                Login = timecount,
                                LoginColor = "hsl(205, 70%, 50%)"
                            }
                        );
                    }
                    break;
                default:
                    break;
            }
            returnDashBoard.barChartDto = barChartReturn;

            // Line Chart 
            LineChartDto returnLineChar = new LineChartDto();
            List<LineData> lineDatas = new List<LineData>();
            switch (lineTime)
            {
                case "Day":
                    DateTime dayCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    for (int i = 10; i >= 0; i--)
                    {
                        var daySet = dayCount.AddDays(-i);
                        var timeGet = await _boardRepo.GetAll();
                        var times = timeGet.Where(tg => tg.CreatedAt.Day == daySet.Day && tg.CreatedAt.Month == daySet.Month && tg.CreatedAt.Year == daySet.Year).Count();
                        lineDatas.Add(
                            new LineData
                            {
                                x = "Day " + (i + 1),
                                y = times,
                            }
                        );
                    }
                    break;
                case "Month":
                    DateTime monthCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    for (int i = 10; i >= 0; i--)
                    {
                        var daySet = monthCount.AddMonths(-i);
                        var timeGet = await _boardRepo.GetAll();
                        var times = timeGet.Where(tg => tg.CreatedAt.Month == daySet.Month && tg.CreatedAt.Year == daySet.Year).Count();
                        lineDatas.Add(
                            new LineData
                            {
                                x = "Month " + (i + 1),
                                y = times,
                            }
                        );
                    }
                    break;
                case "Year":
                    DateTime yearCount = new DateTime(DateTime.Now.Year, 1, 1);
                    for (int i = 10; i >= 0; i--)
                    {
                        var daySet = yearCount.AddYears(-i);
                        var timeGet = await _boardRepo.GetAll();
                        var times = timeGet.Where(tg => tg.CreatedAt.Year == daySet.Year).Count();
                        lineDatas.Add(
                            new LineData
                            {
                                x = "Year " + (i + 1),
                                y = times,
                            }
                        );
                    }
                    break;
                default:
                    break;
            }
            returnLineChar.Id = lineTime;
            returnLineChar.Color = "hsl(205, 70%, 50%)";
            returnLineChar.Data = lineDatas;
            List<LineChartDto> addLineChart = new List<LineChartDto>();
            addLineChart.Add(returnLineChar);

            returnDashBoard.lineChartDto = addLineChart;

            return returnDashBoard;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/dashboard/Detail")]
        public async Task<ActionResult<AdminDto>> GetDashboardDetails()
        {
            var userList = await _userRepo.GetAll();
            var totalMem = userList.Count();
            var boardList = await _boardRepo.GetAll();
            var totalBoard = boardList.Count();

            var filterNewMem = Builders<User>.Filter.Gt("CreatedAt", DateTime.Now.AddDays(-7));
            var newMems = await _userRepo.GetAll(filter: filterNewMem);
            var newMemCount = newMems.Count();

            var filterNewBoard = Builders<Board>.Filter.Gt("CreatedAt", DateTime.Now.AddDays(-7));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kindOfTime"></param>
        /// <returns></returns>
        [HttpGet("/dashboard/bar/{kindOfTime}")]
        public async Task<ActionResult<IEnumerable<BarChartDto>>> GetDashboardBarChart(string kindOfTime)
        {
            List<BarChartDto> barChartReturn = new List<BarChartDto>();
            switch (kindOfTime)
            {
                case "Day":
                    DateTime dayCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    for (int i = 10; i >= 0; i--)
                    {
                        var filter = Builders<SignIn>.Filter.Eq("At", dayCount.AddDays(-i));
                        var day = await _signInRepo.GetByCondition(filter: filter);
                        int value;
                        if (day == null)
                        {
                            value = 0;
                        }
                        else
                        {
                            value = day.Times;
                        }
                        barChartReturn.Add(
                            new BarChartDto
                            {
                                Country = "Day " + (i + 1),
                                Login = value,
                                LoginColor = "hsl(205, 70%, 50%)"
                            }
                        );
                    }
                    break;
                case "Month":
                    DateTime monthCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    for (int i = 10; i >= 0; i--)
                    {
                        var filter = Builders<SignIn>.Filter.Gte("At", monthCount.AddMonths(-i)) & Builders<SignIn>.Filter.Lte("At", monthCount.AddMonths(-i + 1));
                        var day = await _signInRepo.GetAll(filter: filter);
                        var timeGet = day;
                        int timecount = 0;
                        if (timeGet == null)
                        {
                            timecount = 0;
                        }
                        else
                        {
                            foreach (var item in timeGet)
                            {
                                timecount += item.Times;
                            }
                        }
                        barChartReturn.Add(
                            new BarChartDto
                            {
                                Country = "Month " + i,
                                Login = timecount,
                                LoginColor = "hsl(205, 70%, 50%)"
                            }
                        );
                    }
                    break;
                case "Year":
                    DateTime yearCount = new DateTime(DateTime.Now.Year, 1, 1);
                    for (int i = 10; i >= 0; i--)
                    {
                        var day = await _signInRepo.GetAll();
                        var timeGet = day.Where(d => d.At >= yearCount.AddYears(-i) && d.At <= new DateTime(yearCount.Year - i, 12, 31));
                        int timecount = 0;
                        if (timeGet == null)
                        {
                            timecount = 0;
                        }
                        else
                        {
                            foreach (var item in timeGet)
                            {
                                timecount += item.Times;
                            }
                        }
                        barChartReturn.Add(
                            new BarChartDto
                            {
                                Country = "Year " + i,
                                Login = timecount,
                                LoginColor = "hsl(205, 70%, 50%)"
                            }
                        );
                    }
                    break;
                default:
                    break;
            }
            return barChartReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kindOfTime"></param>
        /// <returns></returns>
        [HttpGet("/dashboard/line/{kindOfTime}")]
        public async Task<ActionResult<IEnumerable<LineChartDto>>> GetDashBoardLineChar(string kindOfTime)
        {
            LineChartDto returnLineChar = new LineChartDto();
            List<LineData> lineDatas = new List<LineData>();
            switch (kindOfTime)
            {
                case "Day":
                    DateTime dayCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    for (int i = 10; i >= 0; i--)
                    {
                        var daySet = dayCount.AddDays(-i);
                        var filter = Builders<Board>.Filter.Gte("CreatedAt", daySet) & Builders<Board>.Filter.Lte("CreatedAt", daySet.AddDays(1));
                        var timeGet = await _boardRepo.GetAll(filter: filter);
                        var times = timeGet.Count();
                        lineDatas.Add(
                            new LineData
                            {
                                x = "Day " + (i + 1),
                                y = times,
                            }
                        );
                    }
                    break;
                case "Month":
                    DateTime monthCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    for (int i = 10; i >= 0; i--)
                    {
                        var daySet = monthCount.AddMonths(-i);
                        var filter = Builders<Board>.Filter.Gte("CreatedAt", daySet) & Builders<Board>.Filter.Lte("CreatedAt", daySet.AddMonths(1));
                        var timeGet = await _boardRepo.GetAll(filter: filter);
                        var times = timeGet.Count();
                        lineDatas.Add(
                            new LineData
                            {
                                x = "Month " + (i + 1),
                                y = times,
                            }
                        );
                    }
                    break;
                case "Year":
                    DateTime yearCount = new DateTime(DateTime.Now.Year, 1, 1);
                    for (int i = 10; i >= 0; i--)
                    {
                        var daySet = yearCount.AddYears(-i);
                        var filter = Builders<Board>.Filter.Gte("CreatedAt", daySet) & Builders<Board>.Filter.Lte("CreatedAt", daySet.AddYears(1));
                        var timeGet = await _boardRepo.GetAll(filter: filter);
                        var times = timeGet.Count();
                        lineDatas.Add(
                            new LineData
                            {
                                x = "Year " + (i + 1),
                                y = times,
                            }
                        );
                    }
                    break;
                default:
                    break;
            }
            returnLineChar.Id = kindOfTime;
            returnLineChar.Color = "hsl(205, 70%, 50%)";
            returnLineChar.Data = lineDatas;
            List<LineChartDto> addLineChart = new List<LineChartDto>();
            addLineChart.Add(returnLineChar);
            return addLineChart;
        }
    }
}