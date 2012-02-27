using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Dialect;
using NHibernate.Cfg;
using System.Data;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate;
using NHibernate.Cfg.Loquacious;
using QuantumCode.NHEx.Config;

namespace QuantumCode.NHEx
{
    public class SessionFactoryManager
    {
        private static InnerSessionFactoryManager m_Inner;

        private static object m_Locker = new object();

        static SessionFactoryManager()
        {
            if (null == m_Inner)
            {
                lock (m_Locker)
                {
                    if (null == m_Inner)
                        m_Inner = new InnerSessionFactoryManager();
                }
            }
        }

        public static SessionFactoryManager Create()
        {
            return new SessionFactoryManager();
        }

        public static ISessionFactory CreateSessionFactoryBy(ConnectionStringValueType connectionString)
        {
            if (connectionString.IsName)
                return CreateByConnectionStringName(connectionString);
            else
                return CreateByConnectionString(connectionString);
        }

        protected static ISessionFactory CreateByConnectionString(string connectionString)
        {
            lock (m_Locker)
            {
                return m_Inner.CreateSessionFactoryByConnectionString(connectionString, 
                    DbPreConfigsManager.Current.DefaultConfig.DbPropertiesBuilderByString);
            }
        }

        protected static ISessionFactory CreateByConnectionStringName(string name)
        {
            lock (m_Locker)
            {
                return m_Inner.CreateSessionFactoryByConnectionStringName(name,
                    DbPreConfigsManager.Current.DefaultConfig.DbPropertiesBuilderByName);
            }
        }

        public static void InstallTablesBy(ConnectionStringValueType connectionString)
        {
            lock (m_Locker)
            {
                if (connectionString.IsName)
                {
                    m_Inner.CreateInstallTableSessionFactoryByConnectionStringName(connectionString, DbPreConfigsManager.Current.DefaultConfig.DbPropertiesBuilderForUpdate);
                }
                else
                {
                    m_Inner.CreateInstallTableSessionFactoryByConnectionString(connectionString, DbPreConfigsManager.Current.DefaultConfig.DbPropertiesBuilderForUpdate);
                }
            }
        }

        public static void AddMapping(Type mappingType)
        {
            if(!m_Inner.HasType(mappingType))
            {
                lock(m_Locker)
                {
                    m_Inner.AddMapping(mappingType);
                }
            }
        }

        public static void AddMappings(IEnumerable<Type> mappingTypes)
        {
            lock (m_Locker)
            {
                m_Inner.AddMappings(mappingTypes);
            }
        }

        public static void AddMapping(string assemblyName)
        {
            lock (m_Locker)
            {
                m_Inner.AddMapping(assemblyName);
            }
        }
    }
}
