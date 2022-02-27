using DraplusApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DraplusApi.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filter = default(FilterDefinition<TEntity>)!, BsonDocument? sort = null!, BsonDocument? lookup = null!);
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

        /// <summary>
        /// Add new document to selected collection
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>New entity if created</returns>
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        /// <summary>
        /// Delete a document in selected collection by id
        /// </summary>
        /// <param name="id">Id to delete</param>
        /// <returns>Yes(deleted) / No(not delete)</returns>
        public virtual async Task<bool> Delete(string id)
        {
            var rs = await _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("Id", id));
            return rs.DeletedCount > 0 ? true : false;
        }

        /// <summary>
        /// Get all document fit with filter condition
        /// </summary>
        /// <param name="filter">Filter builder for filter element</param>
        /// <param name="sort">Bson document for sort</param>
        /// <param name="lookup">Bson document for join collection</param>
        /// <returns>List of documents</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filter = null!, BsonDocument? sort = null!, BsonDocument? lookup = null!)
        {
            RegisterMissingClass();

            var query = _collection.Aggregate().Match(filter is null ? Builders<TEntity>.Filter.Empty : filter);

            if (lookup is not null)
            {
                query = query.AppendStage<TEntity>(lookup);
            }

            if (sort is not null)
            {
                query = query.Sort(sort);
            }

            var entities = await query.ToListAsync();
            return entities;
        }

        /// <summary>
        /// Get a collection by fitler
        /// </summary>
        /// <param name="filter">Bson filter</param>
        /// <returns>Fit condition document</returns>
        public virtual async Task<TEntity> GetByCondition(FilterDefinition<TEntity> filter = null!)
        {
            RegisterMissingClass();

            var entity = await _collection.Find(filter is null ? Builders<TEntity>.Filter.Empty : filter).FirstOrDefaultAsync();
            return entity;
        }

        /// <summary>
        /// Update a document in selected collection with new value
        /// </summary>
        /// <param name="id">Document id</param>
        /// <param name="entity">New value of document</param>
        /// <returns>Yes(updated) / No(not update)</returns>
        public virtual async Task<bool> Update(string id, TEntity entity)
        {
            var rs = await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("Id", id), entity);
            return rs.ModifiedCount > 0 ? true : false;
        }

        /// <summary>
        /// Release unmanage resources
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Call this method before accessing collection to handle missing discriminator class cause exception
        /// </summary>
        public void RegisterMissingClass()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(LinePathData)))
            {
                BsonClassMap.RegisterClassMap<LinePathData>();
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(TextData)))
            {
                BsonClassMap.RegisterClassMap<TextData>();
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(NoteData)))
            {
                BsonClassMap.RegisterClassMap<NoteData>();
            }
        }
    }
}