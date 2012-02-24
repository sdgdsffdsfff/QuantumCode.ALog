using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Dialect;
using System.Data;
using NHibernate.Cfg.Loquacious;

namespace QuantumCode.NHEx.Config
{
    public class SqlitePreConfig : DbPreConfig
    {
        public SqlitePreConfig()
        {
            
        }

        protected override void DataBaseIntegration(IDbIntegrationConfigurationProperties c)
        {
            c.Dialect<SQLiteDialect>();
            c.Driver<NHibernate.Driver.SQLite20Driver>();
            c.IsolationLevel = IsolationLevel.ReadCommitted;
            c.HqlToSqlSubstitutions = "true=1;false=0";
        }
    }
}
