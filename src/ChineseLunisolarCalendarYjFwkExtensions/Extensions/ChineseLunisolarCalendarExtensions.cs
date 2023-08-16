using System.Globalization;
using YiJingFramework.PrimitiveTypes;

namespace ChineseLunisolarCalendarYjFwkExtensions.Extensions;

public static class ChineseLunisolarCalendarExtensions
{
    public static (Tiangan, Dizhi) GetYearGanzhi(
        this ChineseLunisolarCalendar calendar, DateTime dateTime)
    {
        var year = calendar.GetSexagenaryYear(dateTime);
        var tiangan = calendar.GetCelestialStem(year);
        var dizhi = calendar.GetTerrestrialBranch(year);
        return (new(tiangan), new(dizhi));
    }

    public static Dizhi GetShichenDizhi(
        this ChineseLunisolarCalendar calendar, DateTime dateTime)
    {
        var hour = calendar.GetHour(dateTime);
        if (hour % 2 == 1)
            hour++;
        var result = hour / 2;
        return new(result + 1);
    }
}
