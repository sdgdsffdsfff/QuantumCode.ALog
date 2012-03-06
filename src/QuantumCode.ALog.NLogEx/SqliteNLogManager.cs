using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.IO;
using QuantumCode.NHEx;

namespace QuantumCode.ALog.NLogEx
{
    public class SqliteNLogManager
    {
        private static object _ModifyDBLocker = new object();

        private static string _LogDirFullPath;

        static SqliteNLogManager()
        {
            _LogDirFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            if (!Directory.Exists(_LogDirFullPath))
            {
                lock (_ModifyDBLocker)
                {
                    if (!Directory.Exists(_LogDirFullPath))
                        Directory.CreateDirectory(_LogDirFullPath);
                }
            }

            CheckLogDB();
        }

        private static void CheckLogDB()
        {
            string fileName = "Logs.adb";

            string fileFullName = Path.Combine(_LogDirFullPath, fileName);

            if (!File.Exists(fileFullName))
            {
                lock (_ModifyDBLocker)
                {
                    if (!File.Exists(fileFullName))
                    {
                        System.Data.SQLite.SQLiteConnection.CreateFile(fileFullName);

                        SessionFactoryManager.AddMapping(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuantumCode.ALog.Domain.dll"));

                        SessionFactoryManager.InstallTablesBy(new ConnectionString("Data Source=" + fileFullName));

                        LogManager.Configuration = SqliteNLogConfig.CreateConfiguration("Data Source=" + fileFullName);
                    }
                }
            }
            else
            {
                if (null == LogManager.Configuration)
                {
                    lock (_ModifyDBLocker)
                    {
                        if (null == LogManager.Configuration)
                        {
                            LogManager.Configuration = SqliteNLogConfig.CreateConfiguration("Data Source=" + fileFullName);
                        }
                    }
                }
            }
        }

        public static ALogger GetLogger(string creator)
        {
            ALogger retValue = new ALogger();

            retValue.Logger = LogManager.GetLogger(creator);
            retValue.Creator = creator;

            return retValue;
        }
    }
}
