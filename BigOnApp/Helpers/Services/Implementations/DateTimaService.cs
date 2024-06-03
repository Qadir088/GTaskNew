using BigOnApp.Helpers.Services.Interfaces;

namespace BigOnApp.Helpers.Services.Implementations;

public class DateTimaService : IDateTimeService
{
    public DateTime ExecutingTime
    {
        get
        {
            return DateTime.Now;
        }
    }
}

public class UtcDateTimaService : IDateTimeService
{
    public DateTime ExecutingTime
    {
        get
        {
            return DateTime.UtcNow.AddHours(4);
        }
    }
}
