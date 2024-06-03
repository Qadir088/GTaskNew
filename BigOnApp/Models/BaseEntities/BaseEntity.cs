using BigOnApp.Models.AudiTables;

namespace BigOnApp.Models.BaseEntities;

public abstract class BaseEntity<Tkey> : AudiTableEntity 
    where Tkey : struct
{
    public Tkey Id { get; set; }
}
