namespace ReactRio.Utils;

public static class DateTimeExtensions
{
    public static int YearsFromNow(this DateTime date)
    {
        return YearsBetweenDates(date.ToUniversalTime(), DateTime.UtcNow);
    }

    public static int YearsBetweenDates(DateTime start, DateTime end)
    {
        var years = end.Year - start.Year;

        if (end.Month < start.Month || end.Month == start.Month && end.Day < start.Day)
            return years - 1;

        return years;
    }
}
