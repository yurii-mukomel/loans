using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class BaseFilter
    {
        [JsonIgnore]
        public int CurrentPage { get; set; }
        [JsonIgnore]
        public int ItemsOnPage { get; set; }
        [JsonIgnore]
        public bool? SortingDirection { get; set; }
    }
}
