using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog.Config;
using NLog.Targets;

namespace QuantumCode.ALog.NLogEx
{
    public class SqliteNLogConfig
    {
        public static LoggingConfiguration CreateConfiguration(string connectionString)
        {
            LoggingConfiguration retValue = new LoggingConfiguration();

            DatabaseTarget target = new DatabaseTarget();

            target.ConnectionString = connectionString;
            target.DBProvider = "System.Data.SQLite";
            target.UseTransactions = true;


            retValue.AddTarget("SqliteTarget", target);
        }
    }
}
