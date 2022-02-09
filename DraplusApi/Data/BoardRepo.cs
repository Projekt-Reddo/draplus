using DraplusApi.Models;

namespace DraplusApi.Data
{
    public interface IBoardRepo : IRepository<Board> { }

    public class BoardRepo : Repository<Board>, IBoardRepo
    {
        public BoardRepo(IMongoContext mongoContext) : base(mongoContext) { }
    }
}