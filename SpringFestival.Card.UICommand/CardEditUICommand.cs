using System;
using System.ComponentModel.DataAnnotations;
using SpringFestival.Card.Common.Enums;

namespace SpringFestival.Card.UICommand
{
    public class CardEditUICommand
    {
        [Required] public Guid Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public CardType CardType { get; set; }
    }
}