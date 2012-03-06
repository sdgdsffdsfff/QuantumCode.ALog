using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog.Config;
using NLog.Targets;
using NLog;
using NLog.Targets.Wrappers;

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
            target.Parameters.Add(new DatabaseParameterInfo("@exception", "${exception}"));
            target.Parameters.Add(new DatabaseParameterInfo("@level", "${level:uppercase=true}"));
            target.Parameters.Add(new DatabaseParameterInfo("@message", "${message}"));
            target.Parameters.Add(new DatabaseParameterInfo("@origin", "${stacktrace}"));//"${callsite}"));

            AsyncTargetWrapper asyncTarget = new AsyncTargetWrapper(target);

            asyncTarget.BatchSize = 10;
            asyncTarget.TimeToSleepBetweenBatches = 1000;
            asyncTarget.OverflowAction = AsyncTargetWrapperOverflowAction.Block;

            retValue.AddTarget("SqliteTarget", target);

            LoggingRule r = new LoggingRule("*", LogLevel.Debug, target);

            retValue.LoggingRules.Add(r);

            return retValue;
        }
    }
}
