using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace reexmonkey.xmisc.core.system.extensions
{
    /// <summary>
    /// Provides useful functional extensions to <see cref = "DateTime"/> instances
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Counts the number of days in a month for a given year
        /// </summary>
        /// <param name="month">The month number of the month in a year (1- 12)</param>
        /// <param name="year">The given year </param>
        /// <returns>The number of days in the month of a given year</returns>
        public static uint CountDaysInMonth(uint month, uint year)
        {
            switch (month)
            {
                case 1: return 31u;
                case 2: return IsLeapYear(year) ? 29u : 28u;
                case 3: return 31u;
                case 4: return 30u;
                case 5: return 31u;
                case 6: return 30u;
                case 7: return 31u;
                case 8: return 31u;
                case 9: return 30u;
                case 10: return 31u;
                case 11: return 30u;
                case 12: return 31u;
                default: return 28u;
            }
        }

        /// <summary>
        /// Counts the number of days within a month range in a given year
        /// </summary>
        /// <param name="startmonth">The month number of the first month in the range (1 - 12)</param>
        /// <param name="endmonth">The month number of the last month in the range (1-12) </param>
        /// <param name="year">The given year</param>
        /// <returns></returns>
        public static uint CountDaysInMonthRange(uint startmonth, uint endmonth, uint year)
        {
            //if (startmonth > endmonth) throw new ArgumentException("The end month should be bigger or equal to the start month", "startmonth");
            //if (startmonth < 1 || startmonth > 12) throw new ArgumentOutOfRangeException("startmonth", "Non-valid value for start month");
            //if (endmonth < 1 || endmonth > 12) throw new ArgumentOutOfRangeException("endmonth", "Non-valid value for end month");
            //if (year < 1 || year > 9999) throw new ArgumentOutOfRangeException("year", "Non-valid value for year");
            var days = 0u;
            for (var month = startmonth; month < endmonth; month++) days += CountDaysInMonth(month, year);
            return days;
        }

        /// <summary>
        /// Checks whether a given year is a leap year
        /// </summary>
        /// <param name="year">The given year</param>
        /// <returns>True if the given year is a leao year, otherwise false</returns>
        public static bool IsLeapYear(uint year) => year % 4 == 0 && year % 100 != 0 && year % 400 == 0;

        /// <summary>
        /// Counts the number of days in a given year
        /// </summary>
        /// <param name="year">The given year</param>
        /// <returns>The number of days in a given year</returns>
        public static uint CountDaysOfYear(uint year) => IsLeapYear(year) ? 366u : 365u;

        /// <summary>
        /// Counts the number of leap occurrences within a given number of years
        /// </summary>
        /// <param name="years">The given number of years</param>
        /// <returns>The number of leap occurrences</returns>
        public static uint CountLeaps(uint years) => (years - 1) / 4 - (years - 1) / 100 + (years - 1) / 400;

        /// <summary>
        /// Counts the number of days in a given number of years
        /// </summary>
        /// <param name="years">The given number of years</param>
        /// <returns></returns>
        public static uint CountDaysOfYears(this uint years) => (years - 1) * 365u + CountLeaps(years);

        /// <summary>
        /// Counts the number of months in a given <see cref = "DateTime"/> range
        /// </summary>
        /// <param name="start">The start <see cref = "DateTime"/> of the range</param>
        /// <param name="end">The end <see cref = "DateTime"/> of the range </param>
        /// <returns></returns>
        public static int CountMonths(this DateTime start, DateTime end) => (start.Year - end.Year) * 12 + start.Month - end.Month;

        /// <summary>
        /// Counts the number of years from a given number of days
        /// </summary>
        /// <param name="days">The number of days</param>
        /// <returns></returns>
        public static uint CountYears(this uint days) => 1 + (days - CountLeaps(days / 365u)) / 365u;

        /// <summary>
        /// Calculates the ordinal position (1-7) in a week from a given <see cref = "DayOfWeek"/> instance
        /// </summary>
        /// <param name="day">The <see cref = "DayOfWeek"/> instance</param>
        /// <returns>The ordinal position (1-7) of the day in the week </returns>
        public static int OrdinalWeekDay(this DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return 1;
                case DayOfWeek.Tuesday: return 2;
                case DayOfWeek.Wednesday: return 3;
                case DayOfWeek.Thursday: return 4;
                case DayOfWeek.Friday: return 5;
                case DayOfWeek.Saturday: return 6;
                case DayOfWeek.Sunday: return 7;
                default: return 0;
            }
        }

        /// <summary>
        /// Determines the <see cref = "DayOfWeek"/> instance from the ordinal position of a day in the week
        /// </summary>
        /// <param name="ordweekday">The ordinal position of a a day in the week</param>
        /// <returns>The determined <see cref = "DayOfWeek"/> instance </returns>
        public static DayOfWeek ToDayofWeeek(this int ordweekday)
        {
            switch (ordweekday)
            {
                case 1: return DayOfWeek.Monday;
                case 2: return DayOfWeek.Tuesday;
                case 3: return DayOfWeek.Wednesday;
                case 4: return DayOfWeek.Thursday;
                case 5: return DayOfWeek.Friday;
                case 6: return DayOfWeek.Saturday;
                case 7: return DayOfWeek.Sunday;
                default:
                    throw new ArgumentException("Ordinal day of week must range from 1 through 7");
            }
        }

        /// <summary>
        /// Calculates the ISO-8601 day of the year from a given <see cref = "DateTime"/> instance.
        /// </summary>
        /// <param name="value">The <see cref = "DateTime"/> instance</param>
        /// <returns>The ISO-8601 day of the year from a <see cref = "DateTime"/> instance </returns>
        /// <see>http://en.wikipedia.org/wiki/ISO_8601</see>
        public static int ISO8601DayOfYear(this DateTime value)
        {
            int Lookup(DateTime x)
            {
                if (DateTime.IsLeapYear(x.Year))
                {
                    switch (x.Month)
                    {
                        case 1:
                            return 0;
                        case 2:
                            return 31;
                        case 3:
                            return 60;
                        case 4:
                            return 91;
                        case 5:
                            return 121;
                        case 6:
                            return 152;
                        case 7:
                            return 182;
                        case 8:
                            return 213;
                        case 9:
                            return 224;
                        case 10:
                            return 274;
                        case 11:
                            return 305;
                        case 12:
                            return 335;
                        default:
                            throw new ArgumentException("Invalid Month");
                    }
                }
                switch (x.Month)
                {
                    case 1:
                        return 0;
                    case 2:
                        return 30;
                    case 3:
                        return 59;
                    case 4:
                        return 90;
                    case 5:
                        return 120;
                    case 6:
                        return 151;
                    case 7:
                        return 181;
                    case 8:
                        return 212;
                    case 9:
                        return 223;
                    case 10:
                        return 273;
                    case 11:
                        return 304;
                    case 12:
                        return 334;
                    default:
                        throw new ArgumentException("Invalid Month");
                }
            }

            return value.Day + Lookup(value);
        }

        /// <summary>
        /// Checks if a given <see cref = "DateTime"/> instance is in the first week of its year.
        /// </summary>
        /// <param name="value">The <see cref = "DateTime"/> instance</param>
        /// <returns>True if the <see cref = "DateTime"/> instance is the first week of its year, otherwise false</returns>
        public static bool InFirstWeekOfYear(this DateTime value)
        {
            var first = new DateTime(value.Year, 1, 1, 0, 0, 0, value.Kind);
            var fdays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday };
            return fdays.Contains(first.DayOfWeek);
        }

        /// <summary>
        ///  Checks if a given <see cref = "DateTime"/> instance is in the last week of its year.
        /// </summary>
        /// <param name="value">The <see cref = "DateTime"/> instance</param>
        /// <returns>True if the <see cref = "DateTime"/> instance is the last week of its year, otherwise false</returns>
        public static bool InLastWeekOfYear(this DateTime value)
        {
            var last = new DateTime(value.Year, 12, 31, 0, 0, 0, value.Kind);
            var ldays = new[] { DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            return ldays.Contains(last.DayOfWeek);
        }

        /// <summary>
        /// Calculates the week number in a year for a given <see cref = "DateTime"/> instance.
        /// </summary>
        /// <param name="value">The <see cref = "DateTime"/> instance</param>
        /// <param name="rule">The rule to determine the first day of the week</param>
        /// <param name="first">The day designated as the start of the week</param>
        /// <returns>The week number in a year of a <see cref = "DateTime"/> instance</returns>
        /// <see cref="http://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date"/>
        public static int WeekOfYear(this DateTime value,
            CalendarWeekRule rule = CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek first = DayOfWeek.Monday)
        {
            var day = value.DayOfWeek;

            //adjust for ISO 8601
            if (rule == CalendarWeekRule.FirstFourDayWeek
                && first == DayOfWeek.Monday
                && day >= DayOfWeek.Monday
                && day <= DayOfWeek.Wednesday)
                value.AddDays(3);

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(value, rule, first);
        }

        /// <summary>
        /// Calculates the week number in a month for a given <see cref = "DateTime"/> instance.
        /// </summary>
        /// <param name="value">The <see cref = "DateTime"/> instance</param>
        /// <param name="rule">The rule to determine the first day of the week</param>
        /// <param name="first">The day designated as the start of the week</param>
        /// <returns>The week number in a month of the given <see cref = "DateTime"/> instance</returns>
        /// <see cref="http://stackoverflow.com/questions/2136487/calculate-week-of-month-in-net/2136549#2136549"/>
        public static int WeekOfMonth(this DateTime value, CalendarWeekRule rule = CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek first = DayOfWeek.Monday)
        {
            var firstday = new DateTime(value.Year, value.Month, 1);
            return value.WeekOfYear() - firstday.WeekOfYear() + 1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="weekday"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static bool IsNthWeekdayOfMonth(this DateTime date, DayOfWeek weekday, int N)
        {
            if (N > 0) return (date.Day - 1) / 7 == N - 1 && date.DayOfWeek == weekday;
            var last = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            return (last.Day - date.Day) / 7 == Math.Abs(N) - 1 && date.DayOfWeek == weekday;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="weekday"></param>
        /// <param name="N"></param>
        /// <param name="rule"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static bool IsNthWeekdayOfYear(this DateTime date, DayOfWeek weekday, int N, CalendarWeekRule rule = CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek start = DayOfWeek.Monday)
        {
            var sylvester = new DateTime(date.Year, 12, 31);
            var weeks = sylvester.WeekOfYear(rule, start);
            if (N > 0) return (date.Day - 1) / 7 * weeks == N - 1 && date.DayOfWeek == weekday;
            return (sylvester.Day - date.Day) / 7 * weeks == Math.Abs(N) - 1 && date.DayOfWeek == weekday;
        }

        /// <summary>
        ///  Determines the date of the nth occurrence of a weekday within a given month.
        /// </summary>
        /// <param name="weekday">The week day, whose nth occurrence date in a month is being determined</param>
        /// <param name="N">Specifies the occurence of the week day in the month</param>
        /// <param name="year">The year of the determined date</param>
        /// <param name="month">The month containing the occurences of weekdays</param>
        /// <param name="hour">Optional hour of the determined date</param>
        /// <param name="minute">Optional minute of the determined date</param>
        /// <param name="second">Optional second of the determined date</param>
        /// <returns></returns>
        public static DateTime GetNthDateOfMonth(this DayOfWeek weekday, int N, int year, int month, int hour = 0, int minute = 0, int second = 0)
        {
            if (N > 0)
            {
                var date = new DateTime(year, month, 1, hour, minute, second);
                var d = 7 * (N - 1) + 1;
                while (date.DayOfWeek < weekday) date = date.AddDays(d++);
                return date;
            }
            if (N < 0)
            {
                var date = new DateTime(year, month, 1, hour, minute, second).AddMonths(1).AddDays(-1); //get last day of month
                var d = date.Day - 7 * (Math.Abs(N) - 1);
                while (date.DayOfWeek > weekday) date = date.AddDays(d--);
                return date;
            }
            throw new ArgumentException("Zero values of N not allowed");
        }

        public static DateTime GetNthDateOfYear(this DayOfWeek weekday, int N, int year, CalendarWeekRule rule = CalendarWeekRule.FirstFourDayWeek, DayOfWeek start = DayOfWeek.Monday)
        {
            var sylvester = new DateTime(year, 12, 31);
            var weeks = sylvester.WeekOfYear(rule, start);

            if (N > 0)
            {
                var date = new DateTime(year, 1, 1);
                var d = weeks * 7 * (N - 1) + 1;
                while (date.DayOfWeek < weekday) date = date.AddDays(d++);
                return date;
            }
            if (N < 0)
            {
                var date = sylvester;
                var d = date.Day - weeks * 7 * (Math.Abs(N) - 1);
                while (date.DayOfWeek > weekday) date = date.AddDays(d--);
                return date;
            }
            throw new ArgumentException("Zero values of N not allowed");
        }

        public static IEnumerable<DateTime> GetSimilarDatesOfMonth(this DayOfWeek weekday, int year, int month, int hour = 0, int minute = 0, int second = 0)
        {
            var first = new DateTime(year, month, 1, hour, minute, second);
            return weekday.GetSimilarDatesInRange(first, first.AddMonths(1).AddDays(-1));
        }

        public static IEnumerable<DateTime> GetSimilarDatesOfYear(this DayOfWeek weekday, int year)
        {
            return weekday.GetSimilarDatesInRange(new DateTime(year, 1, 1, 0, 0, 0), new DateTime(year, 12, 31, 23, 59, 59));
        }

        public static IEnumerable<DateTime> GetSimilarDatesInRange(this DayOfWeek weekday, DateTime start, DateTime end)
        {
            var dates = new List<DateTime>();
            var date = start;
            while (date < end)
            {
                if (date.DayOfWeek == weekday) dates.Add(date);
                date = date.AddDays(1);
            }
            return dates;
        }

        public static DateTime AddWeeks(this DateTime value, int weeks) => value.AddDays(7 * weeks);

        public static DateTime PreviousMinute(this DateTime value) => value.AddMinutes(-1);

        public static DateTime NextMinute(this DateTime value) => value.AddMinutes(1);

        public static DateTime NextMinutes(this DateTime value, int offset) => value.AddMinutes(Math.Abs(offset));

        public static DateTime LastMinutes(this DateTime value, int offset) => value.AddMinutes(-Math.Abs(offset));

        public static DateTime LastSecond(this DateTime value) => value.AddSeconds(-1);

        public static DateTime NextSecond(this DateTime value) => value.AddSeconds(1);

        public static DateTime NextSeconds(this DateTime value, int offset) => value.AddSeconds(offset);

        public static DateTime LastSeconds(this DateTime value, int offset) => value.AddSeconds(-Math.Abs(offset));

        public static DateTime LastHour(this DateTime value) => value.AddHours(-1);

        public static DateTime NextHour(this DateTime value) => value.AddHours(1);

        public static DateTime NextHours(this DateTime value, int offset) => value.AddHours(Math.Abs(offset));

        public static DateTime LastHours(this DateTime value, int offset) => value.AddHours(-Math.Abs(offset));

        public static DateTime Yesterday(this DateTime value) => value.Subtract(new TimeSpan(0, 1, 0, 0, 0));

        public static DateTime Tomorrow(this DateTime value) => value.AddDays(1);

        public static DateTime NextDays(this DateTime value, int offset) => value.AddDays(Math.Abs(offset));

        public static DateTime LastDays(this DateTime value, int offset) => value.AddDays(-Math.Abs(offset));

        public static DateTime PreviousWeek(this DateTime value) => value.AddDays(-7);

        public static DateTime NextWeek(this DateTime value) => value.AddDays(7);

        public static DateTime NextWeeks(this DateTime value, int offset) => value.AddDays(7 * Math.Abs(offset));

        public static DateTime PreviousWeeks(this DateTime value, int offset) => value.AddDays(-7 * Math.Abs(offset));

        public static DateTime LastMonth(this DateTime value) => value.AddMonths(-1);

        public static DateTime NextMonth(this DateTime value) => value.AddMonths(1);

        public static DateTime NextMonths(this DateTime value, int offset) => value.AddMonths(Math.Abs(offset));

        public static DateTime LastMonths(this DateTime value, int offset) => value.AddMonths(-Math.Abs(offset));

        public static DateTime LastYear(this DateTime value) => value.AddYears(-1);

        public static DateTime NextYear(this DateTime value) => value.AddYears(1);


        /// <summary>
        /// Generates a time stamp from a date time source
        /// </summary>
        /// <param name="source">The date time object providing the temporal value for the time stamp</param>
        /// <param name="format">The format of the generated time stamp</param>
        /// <param name="provider">The format provider for the formattung</param>
        /// <returns>A time stamp value</returns>
        public static string GenerateTimeStamp(this DateTime source, IFormatProvider provider, string format = "yyyyMMddHHmmssfff")
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            return source.ToString(format, provider);
        }
    }
}