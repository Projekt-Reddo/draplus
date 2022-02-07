using DraplusApi.Models;

namespace DraplusApi.Data
{
    public interface IChatRoomRepo : IRepository<ChatRoom> { }

    public class ChatRoomRepo : Repository<ChatRoom>, IChatRoomRepo
    {
        public ChatRoomRepo(IMongoContext context) : base(context)
        {
        }
    }
}