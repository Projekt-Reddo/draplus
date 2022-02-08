using MongoDB.Driver;

namespace DraplusApi.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filter = default(FilterDefinition<TEntity>)!);
        Task<TEntity> GetByCondition(FilterDefinition<TEntity> filter = default(FilterDefinition<TEntity>)!);
        Task<TEntity> Add(TEntity entity);
        Task<bool> Update(string id, TEntity entity);
        Task<bool> Delete(string id);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> _collection;
        protected readonly IMongoDatabase _database;

        public Repository(IMongoContext context)
        {
            _database = context.Database;
            _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task<bool> Delete(string id)
        {
            var rs = await _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("Id", id));
            return rs.DeletedCount > 0 ? true : false;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filter = null!)
        {
            var entites = await _collection.Find(filter is null ? Builders<TEntity>.Filter.Empty : filter).ToListAsync();
            return entites;
        }

        public virtual async Task<TEntity> GetByCondition(FilterDefinition<TEntity> filter = null!)
        {
            var entity = await _collection.Find(filter is null ? Builders<TEntity>.Filter.Empty : filter).FirstOrDefaultAsync();
            return entity;
        }

        public virtual async Task<bool> Update(string id, TEntity entity)
        {
            var rs = await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("Id", id), entity);
            return rs.ModifiedCount > 0 ? true : false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}