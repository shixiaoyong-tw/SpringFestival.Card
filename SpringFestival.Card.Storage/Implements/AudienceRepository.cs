using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace SpringFestival.Card.Storage.Implements
{
    public class AudienceRepository : BaseRepository<Entity.Audience>, IAudienceRepository
    {
        private readonly DynamoDBContext _dynamoDbContext;

        public AudienceRepository(IAmazonDynamoDB amazonDynamoDb) : base(amazonDynamoDb)
        {
            if (amazonDynamoDb == null) throw new ArgumentNullException(nameof(amazonDynamoDb));
            _dynamoDbContext = new DynamoDBContext(amazonDynamoDb);
        }
    }
}