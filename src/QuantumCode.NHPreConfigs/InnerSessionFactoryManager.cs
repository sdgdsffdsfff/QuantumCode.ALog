using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Cfg.Loquacious;
using System.Reflection;
using NHibernate.Mapping.ByCode.Conformist;

namespace QuantumCode.NHEx
{
    internal class InnerSessionFactoryManager
    {
        /// <summary>
        /// 类型缓存，Key为Type.FullName
        /// </summary>
        private Dictionary<string, Type> m_Types;

        /// <summary>
        /// SessionFactory缓存，Key为ConnectionStringName或者ConnnectionString
        /// </summary>
        private Dictionary<string, ISessionFactory> m_SessionFactoryCache;

        /// <summary>
        /// HbmMapping缓存
        /// </summary>
        private HbmMapping m_Mapping;

        /// <summary>
        /// 缓存读写锁
        /// </summary>
        private static object m_Locker = new object();

        public InnerSessionFactoryManager()
        {
            m_Types = new Dictionary<string, Type>();
            m_SessionFactoryCache = new Dictionary<string, ISessionFactory>();
        }

        public ISessionFactory CreateSessionFactoryByConnectionStringName(string name, Func<string, Configuration> configurationBuilder)
        {
            if (m_SessionFactoryCache.ContainsKey(name))
            {
                return m_SessionFactoryCache[name];
            }
            else
            {
                var config = configurationBuilder(name);

                var mapping = GetMapping();

                config.AddMapping(mapping);

                ISessionFactory retValue = config.BuildSessionFactory();

                lock (m_Locker)
                {
                    if (!m_SessionFactoryCache.ContainsKey(name))
                    {
                        m_SessionFactoryCache.Add(name, retValue);
                    }
                }

                return m_SessionFactoryCache[name];
            }
        }

        public ISessionFactory CreateSessionFactoryByConnectionString(string connectionString, Func<string, Configuration> configurationBuilder)
        {
            if (m_SessionFactoryCache.ContainsKey(connectionString))
            {
                return m_SessionFactoryCache[connectionString];
            }
            else
            {
                var config = configurationBuilder(connectionString);

                var mapping = GetMapping();

                config.AddMapping(mapping);

                ISessionFactory retValue = config.BuildSessionFactory();

                lock (m_Locker)
                {
                    if (!m_SessionFactoryCache.ContainsKey(connectionString))
                    {
                        m_SessionFactoryCache.Add(connectionString, retValue);
                    }
                }

                return m_SessionFactoryCache[connectionString];
            }
        }

        public void AddMapping(Type mappingType)
        {
            if (!m_Types.ContainsKey(mappingType.FullName))
            {
                lock (m_Locker)
                {
                    if (!m_Types.ContainsKey(mappingType.FullName))
                        m_Types.Add(mappingType.FullName, mappingType);
                }
            }
        }

        public void AddMappings(IEnumerable<Type> mappingTypes)
        {
            lock (m_Locker)
            {
                IEnumerator<Type> enumerator = mappingTypes.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    if (!m_Types.ContainsKey(enumerator.Current.FullName))
                    {
                        m_Types.Add(enumerator.Current.FullName, enumerator.Current);
                    }
                }
            }
        }

        public void AddMapping(string assemblyName)
        {
            Assembly asm = Assembly.LoadFile(assemblyName);

            List<Type> mappingTypes = new List<Type>();

            foreach (Type t in asm.GetExportedTypes())
            {
                if (IsSubclassOfClassMapping(t))
                {
                    mappingTypes.Add(t);
                }
            }

            AddMappings(mappingTypes.ToArray());
        }

        private bool IsSubclassOfClassMapping(Type toCheck)
        {
            Type generic = typeof(ClassMapping<>);
            while (toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        private HbmMapping GetMapping()
        {
            if (null == m_Mapping)
            {
                lock (m_Locker)
                {
                    if (null == m_Mapping)
                    {
                        var mapper = new ModelMapper();

                        mapper.AddMappings(m_Types.Values.ToArray());

                        m_Mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
                    }
                }
            }

            return m_Mapping;
        }

        public bool HasType(Type type)
        {
            return m_Types.ContainsKey(type.FullName);
        }
    }
}
