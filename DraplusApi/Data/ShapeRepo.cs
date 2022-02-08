using DraplusApi.Models;

namespace DraplusApi.Data
{
    public interface IShapeRepo : IRepository<Shape> { }

    public class ShapeRepo : Repository<Shape>, IShapeRepo
    {
        public ShapeRepo(IMongoContext context) : base(context)
        {
        }
    }
}