using BigOnApp.Models.BaseEntities;

namespace BigOnApp.Models;

public class Color : BaseEntity<int>
{
    public string Name { get; set; }
    public string HasCode { get; set; }

}
