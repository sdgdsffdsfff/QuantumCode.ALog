using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuantumCode.NHEx.Config
{
    public class DbPreConfigsManager
    {
        private static DbPreConfigsManager _Instance = null;

        private static object _Locker = new object();

        private DbPreConfig _DefaultConfig = null;

        private DbPreConfigsManager()
        { }

        public static DbPreConfigsManager Current
        {
            get
            {
                if (null == _Instance)
                {
                    lock (_Locker)
                    {
                        if (null == _Instance)
                            _Instance = new DbPreConfigsManager();
                    }
                }

                return _Instance;
            }
        }

        public DbPreConfig DefaultConfig
        {
            get
            {
                //TODO:这里需要加上能根据配置加载不同的Config。目前默认加载SqlitePreConfig
                if (null == _DefaultConfig)
                {
                    lock (_Locker)
                    {
                        if (null == _DefaultConfig)
                            _DefaultConfig = new SqlitePreConfig();
                    }
                }

                return _DefaultConfig;
            }
        }
    }
}
