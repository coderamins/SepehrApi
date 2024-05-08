using Sepehr.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sepehr.Application.Helpers
{
    public sealed class CustomPasswordValidator
    {
        public int minLength { get; set; }
        public CustomPasswordValidator(int minLength)
        {
            this.minLength = minLength;
        }
        public bool ValidateAsync(string password)
        {
            //First Check the Minimum Length Validator
            if (string.IsNullOrEmpty(password) || password.Length < minLength)
            {
                throw new ApiException($"Password Too Short, Minimum {minLength} Character Required");
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
