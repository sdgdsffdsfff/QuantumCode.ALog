using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using QuantumCode.NHEx;
using QuantumCode.ALog.NLogEx;
using System.Threading;

namespace QuantumCode.ALog.Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string testdir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test");

            if (Directory.Exists(testdir))
            {
                Directory.Delete(testdir, true);
            }

            Directory.CreateDirectory(testdir);

            for (int i = 0; i < 10; i++)
            {
                string dbName = Path.Combine(testdir, i.ToString() + ".db3");

                SQLiteConnection.CreateFile(dbName);

                SessionFactoryManager.AddMapping(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuantumCode.ALog.Domain.dll"));

                SessionFactoryManager.InstallTablesBy(new ConnectionString("Data Source=" + dbName));
            }

            MessageBox.Show("OK");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ALogger logger = SqliteNLogManager.GetLogger("test1");

            for (int j = 0; j < 10; j++)
            {
                logger.Debug("test.write", "测试数据：" + j.ToString());
            }
            MessageBox.Show("OK");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Thread> threads = new List<Thread>();

            threads.Add(new Thread(new ThreadStart(StartLog)));
            threads.Add(new Thread(new ThreadStart(StartLog)));
            threads.Add(new Thread(new ThreadStart(StartLog)));
            threads.Add(new Thread(new ThreadStart(StartLog)));
            threads.Add(new Thread(new ThreadStart(StartLog)));

            foreach (Thread t in threads)
                t.Start();

            foreach (Thread t in threads)
                t.Join();

            MessageBox.Show("OK");
        }

        private void StartLog()
        {
            ALogger logger = SqliteNLogManager.GetLogger(Thread.CurrentThread.ManagedThreadId.ToString());

            for (int j = 0; j < 10; j++)
            {
                logger.Debug("test.write", "测试数据：" + j.ToString());
            }
        }
    }
}
