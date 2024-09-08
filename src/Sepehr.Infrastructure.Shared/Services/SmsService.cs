using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sepehr.Application.DTOs.Sms;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Settings;
using RestSharp;


namespace Sepehr.Infrastructure.Shared.Services
{
    public class SmsService : ISmsService
    {
        private SmsSettings _smsSettings { get; }
        private ILogger<SmsService> _logger { get; }
        private IRestClient _restClient;

        public SmsService(
            IOptions<SmsSettings> smsSettings,
            IRestClient _restClient,
            ILogger<SmsService> logger)
        {
            _smsSettings = smsSettings.Value;
            IRestClient restClient = _restClient;
            _logger = logger;
        }

        public async Task SendAsync(SmsRequest smsRequest)
        {
            try
            {
                RestClient restClient = new RestClient(_smsSettings.apiUrl);
                RestRequest restRequest = new RestRequest("/v1/send/likeToLike");
                restRequest.AddHeader("x-api-key", _smsSettings.apikey);
                smsRequest.LineNumber = _smsSettings.sender;
                restRequest.AddBody(smsRequest);
                var result=await restClient.ExecuteAsync(restRequest,Method.Post);  

                //#region ارسال پیامک 
                //using (var client = new System.Net.WebClient())
                //{
                //    client.Headers.Add(System.Net.HttpRequestHeader.Authorization, _smsSettings.apikey);

                //    var data = new System.Collections.Specialized.NameValueCollection()
                //    {
                //        { "SendDateTime", DateTime.UtcNow.ToString() },
                //        { "SMSMessageText", smsRequest.Message  },
                //        { "LineNumber", _smsSettings.amootLineNumber },
                //        { "Mobiles",string.Join(",",smsRequest.Mobile ) },
                //    };

                //    byte[] bytes = client.UploadValues("+", data);

                //    string json = System.Text.UTF8Encoding.UTF8.GetString(bytes);//خروجی
                //}
                //#endregion

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}