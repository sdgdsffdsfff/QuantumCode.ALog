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

        static SqliteNLogManager()
        {
            string fileName = DateTime.Now.DayOfYear.ToString();

            string directoryName = DateTime.Now.Year.ToString("yyyy");

            string dirFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs\\" + directoryName);

            if (!Directory.Exists(dirFullPath))
                Directory.CreateDirectory(dirFullPath);

            string fileFullPath = Path.Combine(dirFullPath, fileName + ".al");

            if (!File.Exists(fileFullPath))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(fileFullPath);
            }

            Connect2LogDB(fileFullPath);
        }

        private static void Connect2LogDB(string fileFullName)
        {
            lock (_ModifyDBLocker)
            {
                LogManager.Configuration = SqliteNLogConfig.CreateConfiguration("Data Source=" + fileFullName);

                SessionFactoryManager.AddMapping("QuantumCode.ALog.Domain.dll");

                SessionFactoryManager.InstallTablesBy(new ConnectionString("Data Source=" + fileFullName));
            }
        }
    }
}
