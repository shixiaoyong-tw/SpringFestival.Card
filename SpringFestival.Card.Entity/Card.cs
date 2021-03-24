using Amazon.DynamoDBv2.DataModel;
using SpringFestival.Card.Common.Enums;

namespace SpringFestival.Card.Entity
{
    /// <summary>
    /// 节目单
    /// </summary>
    [DynamoDBTable("Card")]
    public class Card : RootEntity
    {
        public string Name { get; set; }

        public CardType CardType { get; set; }
    }
}