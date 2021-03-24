using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using SpringFestival.Card.Entity;

namespace SpringFestival.Card.Storage.Implements
{
    public class BaseRepository<TEntity> where TEntity : RootEntity
    {
        private readonly DynamoDBContext _dynamoDbContext;

        public BaseRepository(IAmazonDynamoDB amazonDynamoDb)
        {
            if (amazonDynamoDb == null) throw new ArgumentNullException(nameof(amazonDynamoDb));
            _dynamoDbContext = new DynamoDBContext(amazonDynamoDb);
        }

        public async Task Add(TEntity entity)
        {
            await _dynamoDbContext.SaveAsync(entity);
        }

        public async Task Edit(TEntity entity)
        {
            await _dynamoDbContext.SaveAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            await _dynamoDbContext.DeleteAsync<TEntity>(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            var allEntities = await _dynamoDbContext
                .ScanAsync<TEntity>(new List<ScanCondition>())
                .GetRemainingAsync();

            return allEntities;
        }

        public async Task<TEntity> Get(Guid id)
        {
            var entity = await _dynamoDbContext.LoadAsync<TEntity>(id);

            return entity;
        }
    }
}