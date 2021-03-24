using System;
using Amazon.DynamoDBv2.DataModel;

namespace SpringFestival.Card.Entity
{
    /// <summary>
    /// 观众
    /// </summary>
    [DynamoDBTable("Audience")]
    public class Audience : RootEntity
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public Guid CardId { get; set; }

        /// <summary>
        /// 投票次数
        /// </summary>
        public int Time { get; set; }
    }
}