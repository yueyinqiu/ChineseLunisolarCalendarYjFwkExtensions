using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using YiJingFramework.PrimitiveTypes;

namespace ChineseLunisolarCalendarYjFwkExtensions.Extensions.Tests;

[TestClass()]
public class ChineseLunisolarCalendarExtensionsTests
{
    #region 年干支和时辰地支
    [TestMethod()]
    public void GetYearGanzhiTest()
    {
        var calendar = new ChineseLunisolarCalendar();
        var dateTime = new DateTime(2023, 8, 16, 12, 41, 23);

        var (gan, zhi) = calendar.GetYearGanzhi(dateTime);
        Assert.AreEqual(Tiangan.Gui, gan);
        Assert.AreEqual(Dizhi.Mao, zhi);
    }

    [TestMethod()]
    public void GetShichenDizhiTest()
    {
        var calendar = new ChineseLunisolarCalendar();
        var dateTime = new DateTime(2023, 8, 16, 12, 41, 23);

        var shichen = calendar.GetShichenDizhi(dateTime);
        Assert.AreEqual(Dizhi.Wu, shichen);
    }
    #endregion

    #region 更好的月份信息
    [TestMethod()]
    public void GetMonthWithLeapTest()
    {
        var calendar = new ChineseLunisolarCalendar();

        var dateTime = new DateTime(2023, 8, 16, 12, 41, 23);
        Assert.AreEqual(8, calendar.GetMonth(dateTime));
        var (month, leap) = calendar.GetMonthWithLeap(dateTime);
        Assert.AreEqual(7, month);
        Assert.AreEqual(false, leap);

        dateTime = new DateTime(2023, 4, 19, 12, 41, 23);
        (month, leap) = calendar.GetMonthWithLeap(dateTime);
        Assert.AreEqual(2, month);
        Assert.AreEqual(true, leap);
    }
    #endregion

    #region 年月日的中文表示
    [TestMethod()]
    public void GetYearGanzhiInChineseTest()
    {
        var calendar = new ChineseLunisolarCalendar();
        var dateTime = new DateTime(2023, 8, 16, 12, 41, 23);

        var year = calendar.GetYearGanzhiInChinese(dateTime);
        Assert.AreEqual("癸卯", year);
    }

    [TestMethod()]
    public void GetYearInChineseTest()
    {
        var calendar = new ChineseLunisolarCalendar();
        var dateTime = new DateTime(2023, 8, 16, 12, 41, 23);

        var year = calendar.GetYearInChinese(dateTime);
        Assert.AreEqual("二〇二三", year);
        year = calendar.GetYearInChinese(dateTime, zero: '零');
        Assert.AreEqual("二零二三", year);
    }

    [TestMethod()]
    public void GetMonthInChineseTest()
    {
        var calendar = new ChineseLunisolarCalendar();

        var dateTime = new DateTime(2023, 8, 16, 12, 41, 23);
        var month = calendar.GetMonthInChinese(dateTime);
        Assert.AreEqual("七", month);

        dateTime = new DateTime(2023, 4, 19, 12, 41, 23);
        month = calendar.GetMonthInChinese(dateTime);
        Assert.AreEqual("闰二", month);

        dateTime = new DateTime(2022, 8, 5, 12, 41, 23);
        month = calendar.GetMonthInChinese(dateTime);
        Assert.AreEqual("七", month);

        dateTime = new DateTime(2023, 2, 17, 12, 41, 23);
        month = calendar.GetMonthInChinese(dateTime);
        Assert.AreEqual("正", month);
        month = calendar.GetMonthInChinese(dateTime, useZhengFor1: false);
        Assert.AreEqual("一", month);

        dateTime = new DateTime(2023, 12, 15, 12, 41, 23);
        month = calendar.GetMonthInChinese(dateTime);
        Assert.AreEqual("十一", month);
        month = calendar.GetMonthInChinese(dateTime, useDongFor11: true);
        Assert.AreEqual("冬", month);

        dateTime = new DateTime(2024, 1, 17, 12, 41, 23);
        month = calendar.GetMonthInChinese(dateTime);
        Assert.AreEqual("十二", month);
        month = calendar.GetMonthInChinese(dateTime, useLaFor12: true);
        Assert.AreEqual("腊", month);
    }

    [TestMethod()]
    public void GetDayInChineseTest()
    {
        var calendar = new ChineseLunisolarCalendar();
        var dateTime = new DateTime(2023, 8, 15, 12, 41, 23);

        var day = calendar.GetDayInChinese(dateTime);
        Assert.AreEqual("廿九", day);
        day = calendar.GetDayInChinese(dateTime, useNianFor20s: false);
        Assert.AreEqual("二十九", day);
    }
    #endregion
}