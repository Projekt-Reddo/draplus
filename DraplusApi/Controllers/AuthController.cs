using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using DraplusApi.Helpers;
using DraplusApi.Models;

namespace DraplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IBoardRepo _boardRepo;
        private readonly IChatRoomRepo _chatroomRepo;

        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthController(IUserRepo userRepo, IBoardRepo boardRepo, IChatRoomRepo chatroomRepo, IMapper mapper, IJwtGenerator jwtGenerator)
        {
            _userRepo = userRepo;
            _boardRepo = boardRepo;
            _chatroomRepo = chatroomRepo;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] UserView userView)
        {
            try
            {
                var payload = GoogleJsonWebSignature.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
                (var user, var isNew) = await _userRepo.Authenticate(payload);
                if (isNew)
                {
                    var insertedChatRoom = await _chatroomRepo.Add(new ChatRoom()
                    {
                        Name = $"General {user.Name}",
                    });
                    await _boardRepo.Add(new Board()
                    {
                        Name = $"Default {user.Name}",
                        UserId = user.Id,
                        ChatRoomId = insertedChatRoom.Id
                    });
                }
                var claims = _jwtGenerator.GenerateClaims(user);
                var token = _jwtGenerator.GenerateJwtToken(claims);

                var userToReturn = _mapper.Map<AuthDto>(user);
                userToReturn.AccessToken = token;

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