using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using System.Data;
using NHibernate.Cfg;

namespace QuantumCode.NHEx.Config
{
    public abstract class DbPreConfig
    {
        public Func<string, Configuration> DbPropertiesBuilderByName
        {
            get
            {
                return (name) =>
                    {
                        var retValue = new Configuration();

                        ConnectionStringName vname = name;

                        retValue.DataBaseIntegration(c =>
                        {
                            DataBaseIntegration(c);
                            AppendConnectionString(c, vname);
                        });

                        return retValue;
                    };
            }
        }

        public Func<string, Configuration> DbPropertiesBuilderByString
        {
            get
            {
                return (name) =>
                    {
                        var retValue = new Configuration();

                        ConnectionString vname = name;

                        retValue.DataBaseIntegration(c =>
                        {
                            DataBaseIntegration(c);
                            AppendConnectionString(c, vname);
                        });

                        return retValue;
                    };
            }
        }

        public Func<string, Configuration> DbPropertiesBuilderForUpdate
        {
            get
            {
                return (name) =>
                {
                    var retValue = new Configuration();

                    ConnectionString vname = name;

                    retValue.DataBaseIntegration(c =>
                    {
                        DataBaseIntegration(c);
                        c.SchemaAction = SchemaAutoAction.Update;
                    });

                    return retValue;
                };
            }
        }

        protected virtual void AppendConnectionString(IDbIntegrationConfigurationProperties c, ConnectionStringValueType value)
        {
            if (value.IsName)
                c.ConnectionStringName = value;
            else
                c.ConnectionString = value;
        }

        protected abstract void DataBaseIntegration(IDbIntegrationConfigurationProperties c);
    }
}
