using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServicesCheck.Models;

namespace WindowsServicesCheck
{ 
    // TODO : To add some validation when the user specifies the email or teams
    public class ConfigurationSettings
    {

        public List<string> ServiceName { get; set; }

        public string DatabaseProvider { get; }

        public string DbConnectionString { get; set; }

        //TODO : Read the value from the appsettings file
        public SMTPSettings SMTPSettings { get; set; }
        

        //TODO : Read the value from the appsettings file
        public string MsTeamsAlert_Url { get; set; }

        public int AlertType { get; set; }

        //this constructor  allows the configurationSettings class to be initialized with the list  of ServiceNames
        //and the serviceName property appears to be a property of the ConfigurationSettings class that stores a list of service names
        public ConfigurationSettings(List<string> serviceNames) { 
            ServiceName= serviceNames ;
        }

     
    }
}
