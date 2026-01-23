using System.Collections.Generic;

namespace Core.Entities
{
    public class County
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public List<Municipality> Municipalities { get; set; } = new();
    }
}
