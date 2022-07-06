namespace BLL.DTO
{
    public class StatementTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Percentage { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public int MinTerm { get; set; }
        public int MaxTerm { get; set; }
    }
}
