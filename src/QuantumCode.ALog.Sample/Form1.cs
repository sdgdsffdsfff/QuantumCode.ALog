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
        }
    }
}
