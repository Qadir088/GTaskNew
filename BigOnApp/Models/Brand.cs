using BigOnApp.Models.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace BigOnApp.Models;

public class Brand : BaseEntity<int>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}
