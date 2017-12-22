using Newtonsoft.Json;
using SixNet_BBS_Data;
using SixNet_Logger;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SixNet_BBS.Classes
{
    class SlackPost
    {
        public string channel { get; set; }
        public string text { get; set; }
        public int pretty = 1;
    }

    class SlackIntegration
    {

        private readonly DataInterface _dataInterface;
        private readonly HttpClient _client;
        private readonly string _slackToken;
        private readonly string _slackLogChannel;
        private readonly string _slackUrl;

        public SlackIntegration(DataInterface dataInterface)
        {
            _dataInterface = dataInterface;
            _client = new HttpClient();
            _slackToken = _dataInterface.GetUserDefinedField(0, "SLACKTOKEN");
            _slackLogChannel = _dataInterface.GetUserDefinedField(0, "SLACKLOGCHANNEL");
            _slackUrl = _dataInterface.GetUserDefinedField(0, "SLACKURL");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _slackToken);

        }

        public void LogMessage(string notificationText)
        {
        
            try
            {
                var slackPost = new SlackPost() { channel = _slackLogChannel, text = notificationText };
                var content = new StringContent(JsonConvert.SerializeObject(slackPost));
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
                _client.PostAsync(_slackUrl, content);
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
            }
        }
    }
}
