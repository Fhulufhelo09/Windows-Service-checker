using Microsoft.EntityFrameworkCore;
using System.Linq;
using WindowsServicesCheck.Data;
using WindowsServicesCheck.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.ServiceProcess;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlTypes;
internal static class Helpers
{
    // Since we want to query data from a sqlServerDB using entity framework:

    public static List<string> GetServicesByMachineName()
    {
        //defining the name of local machine
        var machineName = Environment.MachineName;

        List<string> serviceName = new List<string>();

        const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ServiceName.Data;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        
        //created an instance using options that will configure with the DbContextOptionBuilder
        var optionsBuilder = new DbContextOptionsBuilder<ServiceNameContext>();

      
        //added the connectionstring to configure the connection with a SqlServer database for an entity Framework 
        optionsBuilder.UseSqlServer(connectionString);

        //created an instance of optionBuilder that will enable me to create new dbContext instance with the configured options 
        var options = optionsBuilder.Options;
        var dbContext = new ServiceNameContext(options);

        //this retrieves the serviceNames objects in the database and store them in the serviceNames variable
        var serviceNames = dbContext.ServiceNames.ToList().Where(x => x.Type == GetServiceNameTypeByMachineName("P")).Select(x => x.Name).ToList();
        dbContext.Database.EnsureCreated();

        return serviceNames;
    }

    private static char GetServiceNameTypeByMachineName(string machineName)
    {
        if (machineName.ToUpper().Contains("P"))
            return 'P';
        if (machineName.ToUpper().Contains("S001") || machineName.ToUpper().Contains("S01"))
            return 'S';
        else
            return 'A'; //Headoffice, will amend later
    }
}