using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace test_project_gi
{
    public class DatabaseIf
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        private static DatabaseIf instance = new DatabaseIf(Path.Combine(Application.StartupPath, "task.db"));

        /// <summary>
        /// インスタンス
        /// </summary>
        public static DatabaseIf Instance { get { return instance; } }

        #region 

        /// <summary>
        /// コネクション
        /// </summary>
        private SQLiteConnection sqlConnection;

        #endregion

        /// <summary>
        /// データベース
        /// </summary>
        public string DbFileName { get; set; }

        /// <summary>
        /// タイムアウト
        /// </summary>
        public int Timeout { get; set; }

        public static DatabaseIf Instance1
        {
            get
            {
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbFileName"></param>
        private DatabaseIf(string dbFileName)
        {
            DbFileName = dbFileName;
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 接続
        /// </summary>
        /// <returns></returns>
        public void Connect()
        {
            var scsb = new SQLiteConnectionStringBuilder();

            scsb.DataSource = DbFileName;
            scsb.DefaultTimeout = Timeout;

            if (sqlConnection != null) Disconnect();
            sqlConnection = new SQLiteConnection(scsb.ConnectionString);
            sqlConnection.Open();
        }

        /// <summary>
        /// 切断
        /// </summary>
        public void Disconnect()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open) sqlConnection.Close();
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns></returns>
        public DataTable ExecuteSql(string sql)
        {
            using (var sc = new SQLiteCommand(sql, sqlConnection))
            {
                using (SQLiteDataReader sdr = sc.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(sdr);
                    return dt;
                }
            }

        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns></returns>
        public int ExecuteSqlNonQuery(string sql)
        {
            Console.WriteLine(sql);
            using (var sc = new SQLiteCommand(sql, sqlConnection))
            {
                return sc.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
