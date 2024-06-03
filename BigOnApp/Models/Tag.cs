using BigOnApp.Models.BaseEntities;

namespace BigOnApp.Models
{
    public class Tag : BaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
