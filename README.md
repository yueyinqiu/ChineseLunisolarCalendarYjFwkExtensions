# ChineseLunisolarCalendarYjFwkExtensions

ChineseLunisolarCalendarYjFwkExtensions 为 `System.Globalization.ChineseLunisolarCalendar` 提供了一些扩展方法。以下是一些的示例：

## 年干支和时辰地支

```csharp
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
```

## 年月日的中文表示

```csharp
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

    var year2 = calendar.GetYearInChinese(dateTime, zero: '零');
    Assert.AreEqual("二零二三", year2);
}

[TestMethod()]
public void GetMonthInChineseTest()
{
    var calendar = new ChineseLunisolarCalendar();

    var dateTime1 = new DateTime(2023, 8, 16, 12, 41, 23);
    var month1 = calendar.GetMonthInChinese(dateTime1);
    Assert.AreEqual("七", month1);

    var dateTime2 = new DateTime(2023, 4, 19, 12, 41, 23);
    var month2 = calendar.GetMonthInChinese(dateTime2);
    Assert.AreEqual("闰二", month2);

    var dateTime3 = new DateTime(2023, 2, 17, 12, 41, 23);
    var month3 = calendar.GetMonthInChinese(dateTime3);
    Assert.AreEqual("正", month3);
}

[TestMethod()]
public void GetDayInChineseTest()
{
    var calendar = new ChineseLunisolarCalendar();
    var dateTime = new DateTime(2023, 8, 15, 12, 41, 23);

    var day1 = calendar.GetDayInChinese(dateTime);
    Assert.AreEqual("廿九", day1);

    var day2 = calendar.GetDayInChinese(dateTime, useNianFor20s: false);
    Assert.AreEqual("二十九", day2);
}
```