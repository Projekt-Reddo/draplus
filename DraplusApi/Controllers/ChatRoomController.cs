using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DraplusApi.Data;
using DraplusApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DraplusApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomRepo _chatRoomRepo;
        private readonly IMapper _mapper;

        public ChatRoomController(IChatRoomRepo chatRoomRepo, IMapper mapper)
        {
            _chatRoomRepo = chatRoomRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ChatRoom>> AddChatRoom([FromBody] ChatRoom chatRoom)
        {
            if (chatRoom == null)
            {
                return BadRequest();
            }

            var createdChatRoom = await _chatRoomRepo.Add(chatRoom);
            return Ok(createdChatRoom);
        }
    }
}