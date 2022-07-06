namespace DAL.Models
{
    public class EmployeeFilter : BaseFilter
    {
        public enum Sort
        {
            id,
            firsName,
            lastName,
        }
        public Sort SortingData { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
