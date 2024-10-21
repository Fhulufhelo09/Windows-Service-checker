using System.ServiceProcess;
using System.Net.Http;
using System.Diagnostics;
using Microsoft.Win32;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace WindowsServicesCheck
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string? _webHookUrl;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
           
        }

        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
         
            while (!stoppingToken.IsCancellationRequested)
            {
                var OsVersion = Environment.OSVersion;
                Console.WriteLine($"OS version: {OsVersion} is up and running");

                // code is storing the list of service names in memory to use them in late stage of the program 
                List<string> Services = Helpers.GetServicesByMachineName();

                foreach (string serviceName in Services)
                {
                    try
                    {
                        ServiceController WindowService = new ServiceController(serviceName);
                        if (WindowService.Status != ServiceControllerStatus.Running)
                        {
                            Console.WriteLine($"{serviceName} service is not running.");

                            if (WindowService.Status == ServiceControllerStatus.Stopped)
                            {
                                try
                                {
                                    WindowService.Start();
                                    WindowService.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                                }
                                catch (Exception ex)
                                {

                                    //send an alert
                                    Console.WriteLine($"An error occured while starting the service {serviceName}: " + ex.Message);

                                    //Ask the user to choose the message type
                                    Console.WriteLine("Enter the message type (email or teams:");
                                    string messageType = ex.Message;

                                    //check if the user choose email type message
                                    bool sendEmail = IsEmailMessage(messageType, serviceName);
                                    
                                    //send  the appropriate message based on the user's choice
                                    if (sendEmail)
                                    {
                                        SendEmailAlert("example@domain.com", "user", "Service Error Alert", "An error occured while running the service");
                                    }
                                    else
                                    {
                                        SendTeamsMessage("http://your-teams-webhookUrl, An error occured while running the service");
                                    }
                                }
                            }

                            
                        }                       
                      
                    }
                    //add a try catch for in case if the service does not exist
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Service {serviceName} was not found on the computer therefore it does not exist {ex.Message}");
                        await Alert.SendMsTeamsAlert($"{serviceName} is not found"); 


                    }


                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
                break;

           
            }
        }

        private bool IsEmailMessage(string messageType, string serviceName)
        {
            if (messageType.ToLower() == "email")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void SendTeamsMessage(string message)
        {
            HttpClient httpClient = new HttpClient();
            var content = new StringContent($"{{\"text\": \"{message}\"}})", Encoding.UTF8, "appstting/json");
            var response = await httpClient.PostAsync(_webHookUrl, content);
        }

        private void SendEmailAlert(string fromAddress, string toAddress, string subject, string body)
        {
            MailMessage alertMsg = new MailMessage(fromAddress, toAddress, subject, body);
            SmtpClient smtpClient = new SmtpClient("smtp.yourdomain.com", 25);

            //Adding any necessarry credentials for your SMTP Server
            smtpClient.Credentials = new NetworkCredential("username", "password");

            //Send the email message
            smtpClient.Send(alertMsg);
        }
    }
  
}