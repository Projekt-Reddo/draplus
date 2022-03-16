using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using DraplusApi.Helpers;
using DraplusApi.Models;
using MongoDB.Driver;

namespace DraplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IBoardRepo _boardRepo;
        private readonly ISignInRepo _signInRepo;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthController(IUserRepo userRepo, IBoardRepo boardRepo, IMapper mapper, IJwtGenerator jwtGenerator, ISignInRepo signInRepo)
        {
            _userRepo = userRepo;
            _boardRepo = boardRepo;
            _signInRepo = signInRepo;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] UserView userView)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings());
                (var user, var isNew) = await _userRepo.Authenticate(payload);
                if (isNew)
                {
                    var insertedBoard = await _boardRepo.Add(new Board()
                    {
                        Name = $"Default {user.Name}",
                        UserId = user.Id,
                    });
                }

                var claims = _jwtGenerator.GenerateClaims(user);
                var token = _jwtGenerator.GenerateJwtToken(claims);

                var userToReturn = _mapper.Map<AuthDto>(user);
                userToReturn.AccessToken = token;

                DateTime currentDay = new DateTime(
                                    DateTime.Now.Year,
                                    DateTime.Now.Month,
                                    DateTime.Now.Day);
                var filter = Builders<SignIn>.Filter.Eq("At", currentDay);
                var sigin = await _signInRepo.GetByCondition(filter);
                if (sigin == null)
                {
                    await _signInRepo.Add(new SignIn()
                    {
                        At = currentDay,
                        Times = 1
                    });
                }
                else
                {
                    sigin.Times += 1;
                    await _signInRepo.Update(sigin.Id, sigin);
                }
                return Ok(
                    userToReturn
                );
            }
            catch (Exception ex)
            {
                BadRequest(new ResponseDto(400, ex.Message));
            }
            return BadRequest(new ResponseDto(400));
        }
    }
}