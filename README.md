# ChineseLunisolarCalendarYjFwkExtensions

ChineseLunisolarCalendarYjFwkExtensions 为 `ChineseLunisolarCalendar` 提供了一些扩展方法。

## `GetYearGanzhi`

原本的 `ChineseLunisolarCalendar` 有 `GetSexagenaryYear` 的功能，但返回的结果是一个数字，不便于使用。因此此包提供了 `GetYearGanzhi` 方法：

```csharp
using ChineseLunisolarCalendarYjFwkExtensions.Extensions;
using System.Globalization;

var dateTime = new DateTime(2023, 8, 16, 12, 41, 23);
var calendar = new ChineseLunisolarCalendar();
var (gan, zhi) = calendar.GetYearGanzhi(dateTime);
Console.WriteLine($"{gan:C}{zhi:C}年"); // 癸卯年
```

## `GetShichenDizhi`

原本的 `ChineseLunisolarCalendar` 缺少时辰等功能。因此此包提供了 `GetShichenDizhi` 方法：

```csharp
using ChineseLunisolarCalendarYjFwkExtensions.Extensions;
using System.Globalization;

var dateTime = new DateTime(2023, 8, 16, 12, 41, 23);
var calendar = new ChineseLunisolarCalendar();
var shichen = calendar.GetShichenDizhi(dateTime);
Console.WriteLine($"{shichen:C}时"); // 午时
```