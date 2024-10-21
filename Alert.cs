using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace WindowsServicesCheck
{
    // TODO : This class needs to be modified to send an alert based on the alertType set by the user
    public class Alert
    {
      
        private static HttpClient _client;
        private string _webHookUrl;

        public Alert(HttpClient httpClient)
        {
            // read the webhookurl from the appsettings json file by:

            //created a new instance of configurationBuilder
            var configBuilder = new ConfigurationBuilder();

            //added the json file to the builder
            configBuilder.AddJsonFile("appsettings.json");

            //building the configuration data added to the configBuilder
            var config = configBuilder.Build();

            //get a value from json file
            string value = config["Configurations:MsTeamsAlert_Url"];
            Console.WriteLine(value);
            _webHookUrl = value;
            _client = httpClient ?? throw new ArgumentNullException(); _client.BaseAddress = new Uri(_webHookUrl);
        }


        //This method is used to send a microsoft teams alert message to the user
        public static async Task SendMsTeamsAlert(string message)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
                var body = new AlertMessage
                { Text = $"Windows Service Checker app {message}", };
                var serializeJson = JsonConvert.SerializeObject(body);
                var content = new StringContent(serializeJson, Encoding.UTF8, "application/json");
                _ = await _client.PostAsync(_client.BaseAddress, content);
                await SendMsTeamsAlert(message);
                Console.WriteLine("Alert sent successful");


            }
            catch (Exception ex)
            {
                //log a message if it fails to send an alert
                Console.WriteLine($"Error sending alert: {ex.Message}");

            }
        }
       
    }

   
    public class AlertMessage
    {
        public string Text { get; set; }
    }

   

}

