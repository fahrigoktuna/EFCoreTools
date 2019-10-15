using EFCoreTools.Logging.Configuration;
using EFCoreTools.Logging.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace EFCoreTools.Extensions.Context
{
    internal static class ContextExtensions
    {
        

        public static void UseDbOptions(this DbContextOptionsBuilder optionsBuilder, ConnectionStringSettings stringSettings)
        {
            switch (stringSettings.ProviderName)
            {
                case "System.Data.SqlClient":
                    optionsBuilder.UseSqlServer(stringSettings.ConnectionString);
                    break;
                case "Npgsql":
                    optionsBuilder.UseNpgsql(stringSettings.ConnectionString);
                    break;
                default:
                    //provider any db provider for defaults.
                    break;
            }
        }

        public static void SetDefaultSchema(this ModelBuilder modelBuilder, ConnectionStringSettings stringSettings)
        {
            switch (stringSettings.ProviderName)
            {
                case "System.Data.SqlClient":
                    modelBuilder.HasDefaultSchema("your_sqlserver_schema_name");
                    break;
                case "Npgsql":
                    modelBuilder.HasDefaultSchema("your_plpgsql_schema_name");
                    break;
                default:
                    //provide any db schema for defaults.
                    break;
            }
        }
       

        private static List<ILoggerProvider> _loggerProviderList = new List<ILoggerProvider>()
        {
            new DebugLoggerProvider()
            ,new CustomLoggerProvider(new CustomLoggerConfiguration())
        };

        public static readonly LoggerFactory _myLoggerFactory = new LoggerFactory(
               _loggerProviderList
               );
        public static void DebugLogConfigure(this DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);


            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }
    }
}
