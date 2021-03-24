using System;
using System.ComponentModel.DataAnnotations;

namespace SpringFestival.Card.UICommand
{
    public class CardDeleteUICommand
    {
        [Required]
        public Guid Id { get; set; }
    }
}