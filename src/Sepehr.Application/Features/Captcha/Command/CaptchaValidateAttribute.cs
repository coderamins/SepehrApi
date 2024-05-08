using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Captcha.Command
{
    public class CaptchaValidateAttribute : Attribute
    {
        private readonly IDistributedCache _distributedCache;
        private readonly string _captchaCode;
        public CaptchaValidateAttribute(IDistributedCache distributedCache, string CaptchaCode)
        {
            _distributedCache = distributedCache;
            _captchaCode = CaptchaCode;
        }

        public async Task<bool> OnActionExecuting(
            CaptchaValidateAttribute attribute)
        {
            var cached_captchaCode = await _distributedCache.GetStringAsync(_captchaCode);
            if (_captchaCode != cached_captchaCode)
                return false;

            return true;
        }
    }
}
