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

            target.CommandText = "insert into alog(action, creator, datetimestamp, exception, level, message, origin) "+
                "values(@action, @creator, @datetimestamp, @exception, @level, @message, @origin)";

            target.Parameters.Add(new DatabaseParameterInfo("@action", "${event-context:item=action}"));
            target.Parameters.Add(new DatabaseParameterInfo("@creator", "${event-context:item=creator}"));
            target.Parameters.Add(new DatabaseParameterInfo("@datetimestamp", "${longdate}"));
            target.Parameters.Add(new DatabaseParameterInfo("@exception", "${event-context:item=exception}"));
            target.Parameters.Add(new DatabaseParameterInfo("@level", "${level:uppercase=true}"));
            target.Parameters.Add(new DatabaseParameterInfo("@message", "${message}"));
            target.Parameters.Add(new DatabaseParameterInfo("@origin", "${event-context:item=origin}"));

            retValue.AddTarget("SqliteTarget", target);

            return retValue;
        }
    }
}
