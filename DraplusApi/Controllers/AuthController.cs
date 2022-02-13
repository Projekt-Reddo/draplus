using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Dtos;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DraplusApi.Helper;
=======
using Google.Apis.Auth;
using DraplusApi.Helpers;
>>>>>>> cc8e21b0c25c43545bb9560422542599fe6c132a
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
<<<<<<< HEAD
        
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthController(IUserRepo userRepo,IBoardRepo boardRepo, IChatRoomRepo chatroomRepo, IMapper mapper, IJwtGenerator jwtGenerator)
=======

        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthController(IUserRepo userRepo, IBoardRepo boardRepo, IChatRoomRepo chatroomRepo, IMapper mapper, IJwtGenerator jwtGenerator)
>>>>>>> cc8e21b0c25c43545bb9560422542599fe6c132a
        {
            _userRepo = userRepo;
            _boardRepo = boardRepo;
            _chatroomRepo = chatroomRepo;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("google")]
<<<<<<< HEAD
        public async Task<IActionResult> Google([FromBody]UserView userView)
=======
        public async Task<IActionResult> Google([FromBody] UserView userView)
>>>>>>> cc8e21b0c25c43545bb9560422542599fe6c132a
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
<<<<<<< HEAD
                        ChatRoomId =  insertedChatRoom.Id
=======
                        ChatRoomId = insertedChatRoom.Id
>>>>>>> cc8e21b0c25c43545bb9560422542599fe6c132a
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