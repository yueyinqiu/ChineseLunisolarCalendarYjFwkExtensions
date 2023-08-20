using System.Diagnostics;
using System.Globalization;
using YiJingFramework.PrimitiveTypes;

namespace ChineseLunisolarCalendarYjFwkExtensions.Extensions;

public static class ChineseLunisolarCalendarExtensions
{
    public static (Tiangan tiangan, Dizhi dizhi) GetYearGanzhi(
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

    public static (int month, bool isLeapMonth) GetMonthWithLeap(
        this ChineseLunisolarCalendar calendar, DateTime dateTime)
    {
        var month = calendar.GetMonth(dateTime);
        var leapMonth = calendar.GetLeapMonth(calendar.GetYear(dateTime));
        if (month < leapMonth)
            return (month, false);
        else
            return (month - 1, month == leapMonth);
    }

    public static string GetYearGanzhiInChinese(
        this ChineseLunisolarCalendar calendar, DateTime dateTime)
    {
        var (gan, zhi) = calendar.GetYearGanzhi(dateTime);
        return $"{gan:C}{zhi:C}";
    }

    private static IEnumerable<int> GetDigits(int value, int b = 10)
    {
        if (value < 0)
        {
            yield return -1;
            value = -value;
        }

        for (; value > 0;)
        {
            (value, var r) = Math.DivRem(value, b);
            yield return r;
        }
    }

    private static NotSupportedException PackageTooOldException()
    {
        return new NotSupportedException(
            $"如果出现此异常，请检查包" +
            $" {nameof(ChineseLunisolarCalendarYjFwkExtensions)} " +
            $"是否已经太长时间没有更新，以至于无法支持现在的 {nameof(ChineseLunisolarCalendar)} 。");
    }

    public static string GetYearInChinese(
        this ChineseLunisolarCalendar calendar, DateTime dateTime, char zero = '〇')
    {
        static char GetDigitCharacter(int iBetweenZeroAndTen, char zeroCharacter)
        {
            Debug.Assert(iBetweenZeroAndTen is >= 0 and < 10);
            return iBetweenZeroAndTen switch
            {
                1 => '一',
                2 => '二',
                3 => '三',
                4 => '四',
                5 => '五',
                6 => '六',
                7 => '七',
                8 => '八',
                9 => '九',
                _ => zeroCharacter
            };
        }

        var year = calendar.GetYear(dateTime);
        if (year <= 0)
            throw PackageTooOldException();
        var digits = GetDigits(year).Select(x => GetDigitCharacter(x, zero));
        return string.Concat(digits.Reverse());
    }

    public static string GetMonthInChinese(
        this ChineseLunisolarCalendar calendar, DateTime dateTime,
        bool useZhengFor1 = true,
        bool useDongFor11 = false,
        bool useLaFor12 = false)
    {
        var (month, isLeapMonth) = calendar.GetMonthWithLeap(dateTime);
        var result = month switch
        {
            1 => useZhengFor1 ? "正" : "一",
            2 => "二",
            3 => "三",
            4 => "四",
            5 => "五",
            6 => "六",
            7 => "七",
            8 => "八",
            9 => "九",
            10 => "十",
            11 => useDongFor11 ? "冬" : "十一",
            12 => useLaFor12 ? "腊" : "十二",
            _ => throw PackageTooOldException()
        };
        return isLeapMonth ? $"闰{result}" : result;
    }

    public static string GetDayInChinese(
        this ChineseLunisolarCalendar calendar, DateTime dateTime, bool useNianFor20s = true)
    {
        static char GetDigitCharacter(int iBetweenOneAndTen)
        {
            Debug.Assert(iBetweenOneAndTen is >= 1 and < 10);
            return iBetweenOneAndTen switch
            {
                1 => '一',
                2 => '二',
                3 => '三',
                4 => '四',
                5 => '五',
                6 => '六',
                7 => '七',
                8 => '八',
                _ => '九',
            };
        }
        var day = calendar.GetDayOfMonth(dateTime);
        var dayDigits = GetDigits(day).ToArray();

        switch (dayDigits.Length)
        {
            case 1:
                return $"初{GetDigitCharacter(dayDigits[0])}";
            case 2:
                switch (dayDigits[1])
                {
                    case 1:
                        if (dayDigits[0] is 0)
                            return "初十";
                        return $"十{GetDigitCharacter(dayDigits[0])}";
                    case 2:
                        if (dayDigits[0] is 0)
                            return "二十";
                        return useNianFor20s ?
                            $"廿{GetDigitCharacter(dayDigits[0])}" :
                            $"二十{GetDigitCharacter(dayDigits[0])}";
                    case 3:
                        if (dayDigits[0] is 0)
                            return "三十";
                        break;
                }
                break;
        }
        throw PackageTooOldException();
    }
}
