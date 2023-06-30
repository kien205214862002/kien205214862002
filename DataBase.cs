using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text.Unicode;
using System.Data;

namespace WpfApp2
{
    class DataBase
    {
        private static DataBase instance;

        public static DataBase Instance
        {
            get { if (instance == null) instance = new DataBase(); return instance; }
            private set { DataBase.instance = value; }
        }

        private DataBase() { }

        public static string con = @"Server=localhost; Database=Hotel_WPF; UId=root; Pwd=1111; Pooling=false; Character Set = utf8; Convert Zero Datetime=True";

        // trả về dữ liệu
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(con))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);


                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listPara)
                        if (item.Contains('@'))
                            command.Parameters.AddWithValue(item, parameter[i++]);


                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        // trả về số dòng thành công
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (MySqlConnection connection = new MySqlConnection(con))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);


                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listPara)
                        if (item.Contains('@'))
                            command.Parameters.AddWithValue(item, parameter[i++]);


                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        //để sử dụng cho câu lệnh select count(*)
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (MySqlConnection connection = new MySqlConnection(con))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);


                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listPara)
                        if (item.Contains('@'))
                            command.Parameters.AddWithValue(item, parameter[i++]);


                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }



    }
}
