namespace Core.Entities
{
    public class Municipality
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        
        public int CountyId { get; set; }
        public County? County { get; set; }
    }
}
