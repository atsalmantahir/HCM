namespace HumanResourceManagement.Application.Payrolls.Services.Implementations;

public class WorkingDaysCalculator
{
    public static int CalculateWorkingDays(int year, int month)
    {
        int totalDays = DateTime.DaysInMonth(year, month);
        int workingDays = 0;

        for (int day = 1; day <= totalDays; day++)
        {
            DateTime currentDate = new DateTime(year, month, day);
            //if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
            if (currentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                workingDays++;
            }
        }

        return workingDays;
    }
}
