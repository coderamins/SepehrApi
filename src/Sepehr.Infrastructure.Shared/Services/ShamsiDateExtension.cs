using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sepehr.Infrastructure.Shared
{
    public static class ShamsiDateExtension
    {
        //public static string GetShamsiDateString(DateTime date)
        //{
        //    string ShamsiDate = "";
        //    if (!date.HasValue)
        //    {
        //        return ShamsiDate;
        //    }
        //    var p = new System.Globalization.PersianCalendar();
        //    var MPS_Id = p.GetYear(date.Value);
        //    var Month = p.GetMonth(date.Value);
        //    var Day = p.GetDayOfMonth(date.Value);
        //    var MonthName = "فروردین";
        //    switch (Month)
        //    {
        //        case 1: { MonthName = "فروردین"; break; }
        //        case 2: { MonthName = "اردیبهشت"; break; }
        //        case 3: { MonthName = "خرداد"; break; }
        //        case 4: { MonthName = "تیر"; break; }
        //        case 5: { MonthName = "مرداد"; break; }
        //        case 6: { MonthName = "شهریور"; break; }
        //        case 7: { MonthName = "مهر"; break; }
        //        case 8: { MonthName = "آبان"; break; }
        //        case 9: { MonthName = "آذر"; break; }
        //        case 10: { MonthName = "دی"; break; }
        //        case 11: { MonthName = "بهمن"; break; }
        //        case 12: { MonthName = "اسفند"; break; }
        //        default: { MonthName = "فروردین"; break; }
        //    }
        //    ShamsiDate = Day.ToString() + " " + MonthName + " " + MPS_Id.ToString();
        //    return ShamsiDate;
        //}

        public static string GetShamsiDateTimeStrFrm(DateTime date)
        {
            var shamsiDateTime = "";

            var p = new PersianCalendar();
            var year = p.GetYear(date).ToString();
            var month = (p.GetMonth(date) < 10) ? "0" + p.GetMonth(date) : p.GetMonth(date).ToString();
            var day = (p.GetDayOfMonth(date) < 10) ? "0" + p.GetDayOfMonth(date) : p.GetDayOfMonth(date).ToString();

            shamsiDateTime = year + "/" + month + "/" + day;

            var minutes = (p.GetMinute(date) < 10) ? "0" + p.GetMinute(date) : p.GetMinute(date).ToString();
            var hours = (p.GetHour(date) < 10) ? "0" + p.GetHour(date) : p.GetHour(date).ToString();

            shamsiDateTime += "-" + hours + ":" + minutes;
            return shamsiDateTime;
        }

        public static string GetShamsiDateTimeStrFaFrm(DateTime date)
        {
            return ShamsiFormat.En2Fa(GetShamsiDateTimeStrFrm(date));
        }

        public static string GetShamsiDateTimeStringFormat(long date) // date is 12 digits number : 8 digits for date, 4 digits for time
        {
            var str = date.ToString();
            if (string.IsNullOrEmpty(str) || str.Equals("0") || str.Length != 12) return string.Empty;

            var shamsiDateTime = "";
            var year = str.Substring(0, 4);
            var month = str.Substring(4, 2);
            var day = str.Substring(6, 2);

            shamsiDateTime = year + "/" + month + "/" + day;

            var hours = str.Substring(8, 2);
            var minutes = str.Substring(10, 2);

            shamsiDateTime += " - " + hours + ":" + minutes;

            return shamsiDateTime;
        }

        public static string GetShamsiDateStrFrm(long date) // date is 8 digits number for date
        {
            var str = date.ToString();
            if (string.IsNullOrEmpty(str) || str.Equals("0") || str.Length != 8) return string.Empty;

            var shamsiDateTime = "";
            var year = str.Substring(0, 4);
            var month = str.Substring(4, 2);
            var day = str.Substring(6, 2);

            shamsiDateTime = year + "/" + month + "/" + day;

            return shamsiDateTime;
        }

        public static string GetShamsiDateStrFaFrm(long date) // date is 8 digits number for date
        {
            return ShamsiFormat.En2Fa(GetShamsiDateStrFrm(date));
        }

        public static long GetShamsiDateTimeLongFormat(DateTime date)
        {
            var str = GetShamsiDateTimeStrFaFrm(date);
            str = str.Replace("/", "");
            str = str.Replace(":", "");
            str = str.Replace(" ", "");

            return long.Parse(str);
        }

        public static long GetShamsiDateLongFormat(DateTime date)
        {
            var str = GetShamsiDateTimeStrFaFrm(date);
            str = str.Replace("/", "");
            return long.Parse(str.Substring(0, 8));
        }

        public static String ToShamsiDate(this DateTime dateTime)
        {
            if (dateTime.Year < 623) return string.Empty;

            var shamsiCalendar = new PersianCalendar();
            return string.Format("{0}/{1:00}/{2:00}", shamsiCalendar.GetYear(dateTime), shamsiCalendar.GetMonth(dateTime), shamsiCalendar.GetDayOfMonth(dateTime));
        }

        public static String ToEnNumber(this string FaNum)
        {
            return ShamsiFormat.Fa2En(FaNum);
        }

        public static DateTime FromShamsiToDate(this int shamsiDate)
        {
            if (shamsiDate < 13000000) shamsiDate += 13000000;

            var sDate = shamsiDate.ToString();
            var pYear = int.Parse(sDate.Substring(0, 4));
            var pMonth = int.Parse(sDate.Substring(4, 2));
            var pDay = int.Parse(sDate.Substring(6, 2));

            var pCalendar = new PersianCalendar();
            return pCalendar.ToDateTime(pYear, pMonth, pDay, 0, 0, 0, 0);
        }

        public static DateTime FromShamsiToDate(this long shamsiDate)
        {
            return ((int)shamsiDate).FromShamsiToDate();
        }

        public static int ToShamsiDate8(this DateTime dateTime)
        {
            var pDate = dateTime.ToShamsiDate();
            var noSlash = pDate.Replace("/", "");
            var res = 0;
            int.TryParse(noSlash, out res);
            return res;
        }

        public static int ToShamsiDate6(this DateTime dateTime)
        {
            return dateTime.ToShamsiDate8() - 13000000;
        }

        public static int ToShamsiDate6(this int shamsiDate)
        {
            return shamsiDate > 13000000 ? shamsiDate - 13000000 : shamsiDate;
        }

        public static int ToShamsiDate8(this int shamsiDate)
        {
            return shamsiDate < 13000000 ? shamsiDate + 13000000 : shamsiDate;
        }

        public static String ToShamsiDateTime(this DateTime dateTime)
        {
            try
            {
                if (dateTime.Year < 623) return string.Empty;

                var shamsiCalendar = new PersianCalendar();
                return string.Format("{0}/{1:00}/{2:00} {3:00}:{4:00}", shamsiCalendar.GetYear(dateTime), shamsiCalendar.GetMonth(dateTime), shamsiCalendar.GetDayOfMonth(dateTime), shamsiCalendar.GetHour(dateTime), shamsiCalendar.GetMinute(dateTime));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static String ToShamsiTime(this DateTime dateTime)
        {
            var shamsiCalendar = new PersianCalendar();
            return string.Format("ساعت {0:00}:{1:00}", shamsiCalendar.GetHour(dateTime), shamsiCalendar.GetMinute(dateTime));
        }

        public static DateTime? ToDateTime(long date) // date is 12 digits number : 8 digits for date, 4 digits for time
        {
            var str = date.ToString();
            if (string.IsNullOrEmpty(str) || str.Equals("0") || str.Length != 12) return null;

            var year = str.Substring(0, 4);
            var month = str.Substring(4, 2);
            var day = str.Substring(6, 2);

            var hours = str.Substring(8, 2);
            var minutes = str.Substring(10, 2);

            return ToDateTime(year + "/" + month + "/" + day, hours + ":" + minutes);
        }

        public static DateTime ToDateTime(this string objDate, string time = "00:00")
        {
            objDate = string.IsNullOrEmpty(objDate) ? objDate : objDate.StartsWith("13") ? objDate : "13" + objDate;
            time = string.IsNullOrEmpty(time) ? "00:00" : time;
            if (string.IsNullOrEmpty(objDate))
            {
                return DateTime.MinValue;
            }

            int year, month, day;
            var shamsiCalendar = new PersianCalendar();
            var date = objDate;
            if (Regex.IsMatch(date, @"^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](13|14)?\d{2})|((13|14)\d{2}[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))$"))
            {
                var match = Regex.Match(date, @"^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.]((13|14)?\d{2}))|(((13|14)\d{2})[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))$");
                if (match.Groups[1].Success)
                {
                    day = Convert.ToInt32(match.Groups[2].Value);
                    month = Convert.ToInt32(match.Groups[3].Value);
                    year = Convert.ToInt32(match.Groups[5].Success ? match.Groups[4].Value : string.Format("{0:00}{1:00}", shamsiCalendar.GetYear(DateTime.Now) / 100, match.Groups[4].Value));
                }
                else
                {
                    day = Convert.ToInt32(match.Groups[10].Value);
                    month = Convert.ToInt32(match.Groups[9].Value);
                    year = Convert.ToInt32(match.Groups[7].Value);
                }
            }
            else
            {
                throw new Exception("Invalid Date Expression");
            }
            return shamsiCalendar.ToDateTime(year, month, day, hour: Convert.ToInt16(time.Substring(0, 2)), minute: Convert.ToInt16(time.Substring(3, 2)), second: 0, millisecond: 0);
        }

        public static bool IsValidShamsiDate(this string objDate)
        {
            if
                (Regex.IsMatch(objDate,
                               @"^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](13|14)?\d{2})|((13|14)\d{2}[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))$"))
            {
                if (!(Convert.ToInt16(objDate.Substring(8, 2)) >= 1) |
                    !(Convert.ToInt16(objDate.Substring(8, 2)) <= 31) |
                    !(Convert.ToInt16(objDate.Substring(5, 2)) >= 1) |
                    !(Convert.ToInt16(objDate.Substring(5, 2)) <= 12) |
                    !(Convert.ToInt16(objDate.Substring(0, 4)) <= 1400) |
                    !(Convert.ToInt16(objDate.Substring(0, 4)) >= 1300))
                    return false;
                return true;
            }
            return false;
        }

        public static string ToShamsiMonth(this int mon)
        {
            switch (mon)
            {
                case 1: return "فروردین";
                case 2: return "اردیبهشت";
                case 3: return "خرداد";
                case 4: return "تیر";
                case 5: return "مرداد";
                case 6: return "شهریور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دی";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "فروردین";
            }
        }

        #region محاسبه ی اختلاف زمان رخدادی در گذشته با زمان فعلی به فارسی

        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        public static string RelativeTimeCalculator(DateTime dateTime)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "لحظه ای قبل" : ts.Seconds + " ثانیه قبل";
            }
            if (delta < 2 * MINUTE)
            {
                return "یک دقیقه قبل";
            }
            if (delta < 45 * MINUTE)
            {
                return ShamsiFormat.En2Fa(ts.Minutes + " دقیقه قبل");
            }
            if (delta < 90 * MINUTE)
            {
                return "یک ساعت قبل";
            }
            if (delta < 24 * HOUR)
            {
                return ShamsiFormat.En2Fa(ts.Hours + " ساعت قبل");
            }
            if (delta < 48 * HOUR)
            {
                return "دیروز";
            }
            if (delta < 30 * DAY)
            {
                return ShamsiFormat.En2Fa(ts.Days + " روز قبل");
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "یک ماه قبل" : months + " ماه قبل";
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "یک سال قبل" : years + " سال قبل";
        }
        #endregion

        #region دریافت نام ماه
        public static string GetMonth(int mon)
        {
            switch (mon)
            {
                case 1: return "فروردین";
                case 2: return "اردیبهشت";
                case 3: return "خرداد";
                case 4: return "تیر";
                case 5: return "مرداد";
                case 6: return "شهریور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دی";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "فروردین";
            }
        }
        #endregion

        public static string ToSlashedDate(this int date)
        {
            if (date > 100000 && date < 13000000) date += 13000000;
            var sdate = date.ToString();
            return sdate.Length == 8
                ? string.Format("{0:D4}/{1:D2}/{2:D2}", sdate.Substring(0, 4), sdate.Substring(4, 2), sdate.Substring(6, 2))
                : string.Empty;
        }

        public static string ToSlashedDate(this decimal date1)
        {
            int date = Convert.ToInt32(date1);
            if (date > 100000 && date < 13000000) date += 13000000;
            var sdate = date.ToString();
            return sdate.Length == 8
                ? string.Format("{0:D4}/{1:D2}/{2:D2}", sdate.Substring(0, 4), sdate.Substring(4, 2), sdate.Substring(6, 2))
                : string.Empty;
        }
        public static string ToShamsiDate6(this string date1)
        {
            if (string.IsNullOrEmpty(date1))
                return "";
            //if (Regex.IsMatch(date1, @"^\d{4}\/\d{2}\/\d{2}");
            return date1.Replace("/", "").Substring(2, 6);
        }

        public static string ToShamsiDate8(this string date1)
        {
            if (string.IsNullOrEmpty(date1))
                return "";
            return date1.Replace("/", "").Substring(0, 8);
        }

        public static string ToReadableAgeString(this TimeSpan span)
        {
            return string.Format("{0:0}", span.Days / 365.25);
        }

        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} روز{1}, ", span.Days, span.Days == 1 ? string.Empty : " ") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} ساعت{1}, ", span.Hours, span.Hours == 1 ? string.Empty : " ") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} دقیقه{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : " ") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} ثانیه{1}", span.Seconds, span.Seconds == 1 ? string.Empty : " ") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

        public static string ToSlashedDate(this string stdate)
        {
            try
            {
                if (string.IsNullOrEmpty(stdate))
                    return stdate;
                int date = Convert.ToInt32(stdate);
                if (date > 100000 && date < 13000000) date += 13000000;
                var sdate = date.ToString();
                return sdate.Length == 8
                    ? string.Format("{0:D4}/{1:D2}/{2:D2}", sdate.Substring(0, 4), sdate.Substring(4, 2), sdate.Substring(6, 2))
                    : string.Empty;
            }
            catch
            {
                return stdate;
            }
        }
    }
}
