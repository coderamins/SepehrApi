using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.DTOs.Sms;

namespace Sepehr.Application.Interfaces
{
    public interface ISmsService
    {
        Task SendAsync(SmsRequest request);
        Task SendVerifyCode(string mobile, string code);
    }
}