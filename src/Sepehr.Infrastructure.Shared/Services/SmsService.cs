using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sepehr.Application.DTOs.Sms;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Settings;

namespace Sepehr.Infrastructure.Shared.Services
{
    public class SmsService : ISmsService
    {
        public SmsSettings _smsSettings { get; }
        public ILogger<SmsService> _logger { get; }

        public SmsService(IOptions<SmsSettings> smsSettings, ILogger<SmsService> logger)
        {
            _smsSettings = smsSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(SmsRequest smsRequest)
        {
            try
            {
                //RestClient client;

                //client = new RestClient("https://api.kavenegar.com");
                //RestRequest request = 
                //    new RestRequest($"/v1/{_smsSettings.apikey}/verify/lookup.json?receptor={_smsSettings.receptor}&token={smsRequest.Message}&template=verify", Method.Get);
                //var response =await client.ExecuteAsync(request);

                #region ارسال پیامک 
                using (var client = new System.Net.WebClient())
                {
                    client.Headers.Add(System.Net.HttpRequestHeader.Authorization, _smsSettings.amootToken);

                    var data = new System.Collections.Specialized.NameValueCollection()
                    {
                        { "SendDateTime", DateTime.UtcNow.ToString() },
                        { "SMSMessageText", smsRequest.Message  },
                        { "LineNumber", _smsSettings.amootLineNumber },
                        { "Mobiles",string.Join(",",smsRequest.Mobile ) },
                    };

                    byte[] bytes = client.UploadValues("+", data);

                    string json = System.Text.UTF8Encoding.UTF8.GetString(bytes);//خروجی
                }
                #endregion

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}