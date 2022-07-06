using System.Collections.Generic;

namespace DAL.Models
{
    public class PaginatedData<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int RecordsReturned { get; set; }
        public int TotalRecordsFound { get; set; }
        public int CurrentPage { get; set; }
        public int RecordPerRage { get; set; }
    }
}
