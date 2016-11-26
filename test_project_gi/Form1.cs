using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

public class DaDatabaseIf { }

namespace test_project_gi
{
    public partial class Form1 : Form
    {
        public DataTable test_project_Tbl;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            //接続
            DatabaseIf.Instance.Connect();
            //登録
            var sql = new StringBuilder();

            sql.AppendLine("CREATE TABLE IF NOT EXISTS TEST(TITLE TEXT)");
            //SQL実行
            DatabaseIf.Instance.ExecuteSqlNonQuery(sql.ToString());

            sql = new StringBuilder();

            sql.AppendFormat("INSERT INTO TEST VALUES('{0}')",textBox1.Text);
            DatabaseIf.Instance.ExecuteSqlNonQuery(sql.ToString());

            //切断
            DatabaseIf.Instance.Disconnect();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //接続
            DatabaseIf.Instance.Connect();

            DataTable dt = DatabaseIf.Instance.ExecuteSql("SELECT TEXT FROM TEST");
            textBox1.Text = dt.Rows[0][0].ToString();

            //切断
            DatabaseIf.Instance.Disconnect();
        }
    }
    
}