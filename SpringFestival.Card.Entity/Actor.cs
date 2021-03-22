using SpringFestival.Card.Entity.Enums;

namespace SpringFestival.Card.Entity
{
    /// <summary>
    /// 演员
    /// </summary>
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}