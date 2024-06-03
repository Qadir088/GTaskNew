namespace BigOnApp.Helpers.Services.Interfaces;

public interface IAudiTableEntity
{
     int CreatedBy { get; set; }
     DateTime CreatedAt { get; set; }
     int? ModifiedBy { get; set; }
     DateTime? ModifiedAt { get; set; }
     int? DeletedBy { get; set; }
     DateTime? DeletedAt { get; set; }
}
