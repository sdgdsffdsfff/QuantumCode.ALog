using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog.Targets;
using NLog.Config;
using System.ComponentModel;
using System.IO;

namespace QuantumCode.ALog.NLogEx
{
    [Target("NHTarget")]
    public sealed class NHibernateTarget : Target
    {
        private string _CurrentFilePreFixName;

        private string _FileExtName = ".db3";

        private string _BaseLogDir = "";

        private static object _Locker = new object();

        public NHibernateTarget()
        {
            _CurrentFilePreFixName = DateTime.Now.ToString("yyyyMMdd");
        }

        [RequiredParameter]
        [DefaultValue(".")]
        public string BaseLogDir
        {
            get
            {
                return _BaseLogDir;
            }
            set
            {
                if (Path.IsPathRooted(value))
                {
                    if (!value.StartsWith("\\") || !value.StartsWith("/"))
                    {
                        _BaseLogDir = value;
                    }
                    else
                    {
                        _BaseLogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value.TrimStart(new char[] { '\\' }).TrimStart(new char[] { '/' }));
                    }
                }
                else
                {
                    _BaseLogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value.TrimStart(new char[] { '\\' }).TrimStart(new char[] { '/' }));
                }

                _BaseLogDir = Path.Combine(_BaseLogDir, "Log");

                if (!Directory.Exists(_BaseLogDir))
                {
                    Directory.CreateDirectory(_BaseLogDir);
                }
            }
        }

        private void CheckDatabaseFile(Action<string> CreateDatabaseFile)
        {
            string fullFilePath = Path.Combine(BaseLogDir, _CurrentFilePreFixName + _FileExtName);

            if (!File.Exists(fullFilePath))
            {
                CreateDatabaseFile(fullFilePath);
            }
        }

        private void CreateDB(string dbFullFileName)
        {
            if (!File.Exists(dbFullFileName))
            {
                lock (_Locker)
                {
                    if (!File.Exists(dbFullFileName))
                    {
                        string connecionString = "Data Source=" + dbFullFileName;
                    }
                }
            }
        }
    }
}
