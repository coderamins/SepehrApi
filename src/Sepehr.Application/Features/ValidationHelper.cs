using Sepehr.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sepehr.Application.Features
{
    public static class ValidationHelper
    {
        public static Boolean IsValidNationalCode(this String nationalCode)
        {
            //در صورتی که کد ملی وارد شده تهی باشد

            if (String.IsNullOrEmpty(nationalCode))
                throw new Exception("لطفا کد ملی را صحیح وارد نمایید");


            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                throw new Exception("طول کد ملی باید ده کاراکتر باشد");

            //در صورتی که کد ملی ده رقم عددی نباشد
            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode)) return false;


            //عملیات شرح داده شده در بالا
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        public class CustomPasswordValidator
        {
            public int MinLength { get; set; }
            //While Creating CustomPasswordValidator Instance we need to pass the Minimum Length of the Password
            public CustomPasswordValidator(int minLength)
            {
                MinLength = minLength;
            }
            // Validate Password: count how many types of characters exists in the password  
            // Provide Implementation for the ValidateAsync method of IIdentityValidator Interface
            public bool ValidateAsync(string password)
            {
                //First Check the Minimum Length Validator
                if (string.IsNullOrEmpty(password) || password.Length < MinLength)
                {
                    throw new ApiException($"Password Too Short, Minimum {MinLength} Character Required");
                }
                int counter = 0;
                //Create a List of String to store the different patterns to be checked in the password
                List<string> patterns = new List<string>
            {
                @"[a-z]", // Lowercase  
                @"[A-Z]", // Uppercase  
                @"[0-9]", // Digits  
                @"[!@#$%^&*\(\)_\+\-\={}<>,\.\|""'~`:;\\?\/\[\]]" // Special Symbols
            };
                // Count Type of Different Chars present in the Password  
                foreach (string p in patterns)
                {
                    if (Regex.IsMatch(password, p))
                    {
                        counter++;
                    }
                }
                //If the counter is less than or equals to 3, means password doesnot contain all the required patterns
                if (counter <= 3)
                {
                    throw new ApiException("Please Use a Combination of Lowercase, Uppercase, Digits, Special Symbols Characters");
                }
                return true;
            }
        }

    }
}
