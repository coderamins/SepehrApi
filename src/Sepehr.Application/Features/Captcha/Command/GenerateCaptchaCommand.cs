using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.Captcha.Command
{
    public class GenerateCaptchaCommand : IRequest<Response<CaptchaViewModel>>
    {
    }
    public class GenerateCaptchaCommandHandler : IRequestHandler<GenerateCaptchaCommand, Response<CaptchaViewModel>>
    {
        private readonly IDistributedCache _distributedCache;
        public GenerateCaptchaCommandHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<Response<CaptchaViewModel>> Handle(GenerateCaptchaCommand request, CancellationToken cancellationToken)
        {
            var data = CaptchaMaker.GenerateCaptcha(out var code);
            var singleUseKey = Guid.NewGuid().ToString();

            var options = new DistributedCacheEntryOptions()
                  .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5));
            await _distributedCache.SetStringAsync(singleUseKey, code, options);

            var result = new CaptchaViewModel
            {
                CImage = Convert.ToBase64String(data),
                Key = singleUseKey
            };

            return new Response<CaptchaViewModel>(result, "");

        }
    }
}
