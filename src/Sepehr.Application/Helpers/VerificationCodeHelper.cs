using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Helpers
{
    public static class VerificationCodeHelper
    {
        public static string GenerateVerificationCode(out string verifyCode,int length = 6)
        {
            const string chars = "0123456789";
            var random = new Random();
            var code = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            verifyCode = code.ToString();
            return code;

        }

    }
}
