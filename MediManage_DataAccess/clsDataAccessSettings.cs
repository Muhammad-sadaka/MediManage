using System;
using System.Configuration;
using System.Diagnostics;

namespace MediManage_DataAccess
{
    static class clsDataAccessSettings
    {
        public static string sourceName = "MediManage";
        
        //public static string ConnectionString = "Server=.;Database=MediManage;User Id=sa;Password=123456;";
        // public static string ConnectionString = "Server=.;Database=MediManage;integrated Security=True;";
        public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        public static void EventLogCreate()
        {
            if (!EventLog.SourceExists(sourceName)) EventLog.CreateEventSource(sourceName, "Application");
        }
    }
}
