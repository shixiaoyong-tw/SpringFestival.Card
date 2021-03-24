using System;
using Amazon.DynamoDBv2.DataModel;
using SpringFestival.Card.Common.Enums;

namespace SpringFestival.Card.Entity
{
    /// <summary>
    /// 演员
    /// </summary>
    [DynamoDBTable("Actor")]
    public class Actor : RootEntity
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public Guid CardId { get; set; }
    }
}