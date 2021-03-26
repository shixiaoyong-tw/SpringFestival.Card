using System.Collections.Generic;

namespace SpringFestival.Card.Common
{
    public static class ObjectExtensions
    {
        public static List<KeyValuePair<string, string>> GetKeyValuePairs(this object obj)
        {
            var result = new List<KeyValuePair<string, string>>();
            foreach (var property in obj.GetType().GetProperties())
            {
                result.Add(new KeyValuePair<string, string>(property.Name, property.GetValue(obj).ToString()));
            }

            return result;
        }
    }
}