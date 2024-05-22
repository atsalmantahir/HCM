namespace HumanResourceManagement.Application.Holidays.Queries.Get;

public class HolidayVM
{
    public string ExternalIdentifier { get; set; }
    public string HolidayName { get; set; }
    public DateTime HolidayDate { get; set; }
    public bool IsOfficial { get; set; }
    public bool IsActive { get; set; }
}
