using System;
using System.Globalization;

namespace Sepehr.Domain
{
    public static class ShamsiFormat
    {
        public static string En2Fa(string sNum)
        {
            if (string.IsNullOrEmpty(sNum))
                return string.Empty;

            var sFrNum = "";
            const string vInt = "1234567890";

            sNum = sNum.Trim();

            var mystring = sNum.ToCharArray(0, sNum.Length);

            for (var i = 0; i <= (mystring.Length - 1); i++)
                if (vInt.IndexOf(mystring[i]) == -1)
                    sFrNum += mystring[i];
                else
                    sFrNum += ((char)((int)mystring[i] + 1728));

            return sFrNum;
        }
        public static string Fa2En(string sNum)
        {
            try
            {
                char[][] numbers = new char[][]
                    {
                    "0123456789".ToCharArray(),
                    "۰۱۲۳۴۵۶۷۸۹".ToCharArray()
                    };
                for (int x = 0; x <= 9; x++)
                {
                    sNum = sNum.Replace(numbers[1][x], numbers[0][x]);
                }
                return sNum;
            }
            catch 
            {
                return sNum;
            }
        }

        public static string En2Fa(object p)
        {
            return En2Fa(p.ToString());
        }

        public static string NumberGroupping(string sNum)
        {
            if (string.IsNullOrEmpty(sNum)) return null;

            var result = string.Empty;
            var words = sNum.Split(' ');

            foreach (var word in words)
            {
                double num;
                result += " " + (double.TryParse(word, out num) ? num.ToString("0,0.##", CultureInfo.InvariantCulture) : word);
            }

            return result.Substring(1);
        }

        public static string AddSlashToDate(string numDate)
        {
            if (numDate == null) return null;

            if (numDate.Length == 6)
            {
                numDate = "13" + numDate;
            }
            if (numDate.Length != 8) return numDate;

            return En2Fa(numDate.Substring(0, 4) + '/' + numDate.Substring(4, 2) + '/' + numDate.Substring(6, 2));
        }
    }
}
