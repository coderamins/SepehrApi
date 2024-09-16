using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sepehr.Application.DTOs.Sms;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Settings;
using RestSharp;
using System.Text;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient.Models.Results;


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
                SmsIr smsIr = new SmsIr(_smsSettings.apikey);
                await smsIr.BulkSendAsync(long.Parse(_smsSettings.sender), smsRequest.messageText, smsRequest.mobiles.ToArray());

                //RestClient restClient = new RestClient(_smsSettings.apiUrl);
                //RestRequest restRequest = new RestRequest("/v1/send/bulk");
                //restRequest.AddHeader("x-api-key", _smsSettings.apikey);
                //smsRequest.LineNumber = _smsSettings.sender;
                //restRequest.AddBody(smsRequest);
                //var result=await restClient.ExecuteAsync(restRequest,Method.Post);  
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        public async Task SendVerifyCode(string mobile,string code)
        {
            SmsIr smsIr = new SmsIr(_smsSettings.apikey);
            VerifySendParameter[] verifySendParameters = {
                new VerifySendParameter("NAME", mobile),
                new VerifySendParameter("CODE", code),
            };

            var response = await smsIr.VerifySendAsync(mobile, _smsSettings.SmsTemplateId, verifySendParameters);

            VerifySendResult sendResult = response.Data;
            int messageId = sendResult.MessageId;
            decimal cost = sendResult.Cost;
        }
    }
}