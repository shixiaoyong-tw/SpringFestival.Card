using System;
using System.ComponentModel.DataAnnotations;

namespace SpringFestival.Card.UICommand
{
    public class AudienceVoteUICommand
    {
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "wrong phone number")]
        public string PhoneNumber { get; set; }

        [Required] public Guid CardId { get; set; }
    }
}