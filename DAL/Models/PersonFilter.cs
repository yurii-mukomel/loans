namespace DAL.Models
{
    public class PersonFilter : BaseFilter
    {
        public enum Sort
        {
            id,
            firsName,
            lastName,
            age
        }
        public Sort SortingData { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AgeMoreThan { get; set; }
        public int? AgeLessThan { get; set; }
    }
}
