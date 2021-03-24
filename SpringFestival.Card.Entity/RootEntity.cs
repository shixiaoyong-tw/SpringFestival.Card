using System;
using Amazon.DynamoDBv2.DataModel;

namespace SpringFestival.Card.Entity
{
    public abstract class RootEntity
    {
        [DynamoDBHashKey] public Guid Id { get; set; }

        protected RootEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}