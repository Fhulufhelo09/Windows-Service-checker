using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServicesCheck.Data;
using WindowsServicesCheck.Models;


namespace WindowsServicesCheck
{
    public static class ServicesExtensions
    {
        //we define an extention method  called addconfigSettings 
        public static void AddConfigSettings(this IServiceCollection services , ConfigurationSettings settings)
        {

            //this is configuring a specific section of the applications configuration settings
            services.AddDbContext<ServiceNameContext>(options => options.
            UseSqlServer(settings.DbConnectionString));

// Set Configuration Settings here


            // created a new ServiceCollection object and called the addConfigSettings method passing in an
            //IConfiguration object called configuration that contains the configuration settings we want to add
            var service = new ServiceCollection();
            service.AddConfigSettings(settings);
        }

        private static void  SetAlertSettings(ConfigurationSettings settings)
        {
            
            var configBuilder = new ConfigurationBuilder();

            //added the json file to the builder
            configBuilder.AddJsonFile("appsettings.json");

            //building the configuration data added to the configBuilder
            var config = configBuilder.Build();

            

            if (settings.AlertType == 2)
            {
                //get a value from json file
                string value = config["Configurations:MsTeamsAlert_Url"];
                Console.WriteLine(value);
                //Send a Microsoft teams message
                
            }

            else if (settings.AlertType == 1)
            {
                var appSettings = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appSettings.json"));
                var smtpSettings = appSettings["SMTPSettings"].ToObject<SMTPSettings>();
                Console.WriteLine(smtpSettings);
                //Send email message
                
            }

        else if (settings.AlertType == 0)
            {
                Console.WriteLine("No alert type specified");
            }
        }
    }
}

