using System;
using SpringFestival.Card.Entity.Enums;

namespace SpringFestival.Card.Entity
{
    /// <summary>
    /// 节目单
    /// </summary>
    public class Card
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CardType CardType { get; set; }
    }
}