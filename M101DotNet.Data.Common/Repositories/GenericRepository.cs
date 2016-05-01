namespace M101DotNet.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using M101DotNet.Data.Common.Contracts;
    using M101DotNet.Data.Common.Extensions;
    using M101DotNet.Data.Common.Repositories.Contracts;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;

    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly IMongoDatabase db;
        private string collectionName;

        public GenericRepository(IMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.db = provider.Create();
            this.CollectionName = typeof(T).Name;
        }

        protected IMongoCollection<T> Collection => this.db.GetCollection<T>(this.CollectionName);

        private string CollectionName
        {
            get
            {
                return this.collectionName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.CollectionName));
                }

                string name = value.ToLower();
                int nameLength = name.Length;
                if (name.ToCharArray()[nameLength - 1] == 'y')
                {
                    this.collectionName = $"{name.Substring(0, nameLength - 1)}ies";
                }
                else
                {
                    this.collectionName = $"{name}s";
                }
            }
        }

        public virtual async Task<object> Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.Collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual Task<IQueryable<T>> All()
        {
            return Task.FromResult<IQueryable<T>>(this.Collection.AsQueryable());
        }

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter);
            return result;
        }

        public virtual async Task<object> Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            return await this.Delete(id);
        }

        public virtual async Task<T> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync();
            return entity;
        }

        public virtual async Task<object> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, entity);
            return result;
        }

        private FilterDefinition<T> GetFilterById(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return filter;
        }
    }
}