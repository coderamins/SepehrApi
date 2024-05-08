using Sepehr.Application.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Sepehr.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;

        public DateTime ToDateTime(string objDate, string time = "00:00")
        {
            if (string.IsNullOrEmpty(objDate))
            {
                return DateTime.MinValue;
            }

            int year, month, day;
            var persianCalendar = new PersianCalendar();
            var date = objDate;
            if (Regex.IsMatch(date, @"^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](13|14)?\d{2})|((13|14)\d{2}[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))$"))
            {
                var match = Regex.Match(date, @"^((0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.]((13|14)?\d{2}))|(((13|14)\d{2})[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))$");
                if (match.Groups[1].Success)
                {
                    day = Convert.ToInt32(match.Groups[2].Value);
                    month = Convert.ToInt32(match.Groups[3].Value);
                    year = Convert.ToInt32(match.Groups[5].Success ? match.Groups[4].Value : string.Format("{0:00}{1:00}", persianCalendar.GetYear(DateTime.Now) / 100, match.Groups[4].Value));
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
            return persianCalendar.ToDateTime(year, month, day, hour: Convert.ToInt16(time.Substring(0, 2)), minute: Convert.ToInt16(time.Substring(3, 2)), second: 0, millisecond: 0);
        }

    }

}