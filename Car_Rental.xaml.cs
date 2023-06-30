using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2.Forms
{
    /// <summary>
    /// Interaction logic for Car_Rental.xaml
    /// </summary>
    public partial class Car_Rental : UserControl
    {
        MySqlConnection strCon = Connection.GetSqlConnection();//new MySqlConnection("Server=localhost; Database=hotel_wpf; UId=root; Pwd=1111; Pooling=false; Character Set=utf8 ");
        MySqlCommand da;
        MySqlDataReader data;
        DataTable data_Tour;
        public Car_Rental()
        {
            InitializeComponent();
        }
        private void get_Data()

        {
            Panel.Children.Clear();

            string query = "Select * FROM CAR Limit 1000";
            DataTable dataTable = DataBase.Instance.ExecuteQuery(query);
            data_Tour = DataBase.Instance.ExecuteQuery(query);

            int row = 0;
            int column = 0;
            foreach (DataRow data in dataTable.Rows)
            {
                Car_Rental_Bookingxaml item = new Car_Rental_Bookingxaml();
                item.iD = data["CAR_ID"].ToString();
                item.namE = data["CAR_NAME"].ToString();
                item.pricE = data["CAR_PRICE"].ToString();
                item.packagE = data["CAR_PACKAGE"].ToString();
                item.seaT = data["CAR_SEAT"].ToString();
                item.typE = data["CAR_TYPE"].ToString();

                if (data["Car_Image"] != DBNull.Value)
                {
                    item.image = MainWindow.LoadImageFromFileName(data["Car_Image"].ToString());
                }

                if (row == Panel.RowDefinitions.Count)
                    row = 0;
                Grid.SetRow(item, row++);
                Grid.SetColumn(item, 0);
                Panel.Children.Add(item);
            }


        }
        private void Car_Search(object sender, RoutedEventArgs e)
        {
            string query = "Car_Name like '%" + textLocationBoxSearch.Text + "%'";
            DataRow[] search = data_Tour.Select(query);

            Panel.Children.Clear();
            int row = 0;
            int column = 0;
            foreach (DataRow data in search)
            {
                Car_Rental_Bookingxaml item = new Car_Rental_Bookingxaml();
                item.iD = data["CAR_ID"].ToString();
                item.namE = data["CAR_NAME"].ToString();
                item.pricE = data["CAR_PRICE"].ToString();
                item.packagE = data["CAR_PACKAGE"].ToString();
                item.seaT = data["CAR_SEAT"].ToString();
                item.typE = data["CAR_TYPE"].ToString();

                if (data["Car_Image"] != DBNull.Value)
                {
                    item.image = MainWindow.LoadImageFromFileName(data["Car_Image"].ToString());
                }

                if (row == Panel.RowDefinitions.Count)
                    row = 0;
                Grid.SetRow(item, row++);
                Grid.SetColumn(item, 0);
                Panel.Children.Add(item);
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            get_Data();
        }
    }
}
