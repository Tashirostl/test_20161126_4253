using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_project_gi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
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
            DataTable dt = DatabaseIf.Instance.ExecuteSql("SELECT TITLE FROM TEST");
            if (dt.Rows.Count == 0)
            {
                sql.AppendFormat("INSERT INTO TEST VALUES('{0}')", textBox1.Text);
            }
            else
            {
                sql.AppendFormat("UPDATE INTO TEST VALUES('{0}')", textBox1.Text);
            }

            

            DatabaseIf.Instance.ExecuteSqlNonQuery(sql.ToString());

            //切断
            DatabaseIf.Instance.Disconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //接続
            DatabaseIf.Instance.Connect();

            DataTable dt = DatabaseIf.Instance.ExecuteSql("SELECT TITLE FROM TEST");
            textBox1.Text = dt.Rows[0][0].ToString();

            //切断
            DatabaseIf.Instance.Disconnect();
        }
    }
}
