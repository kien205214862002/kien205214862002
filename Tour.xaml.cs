using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace WpfApp2.Forms
{
    /// <summary>
    /// Interaction logic for Tour.xaml
    /// </summary>
    public partial class Tour : UserControl
    {
        MySqlConnection strCon = Connection.GetSqlConnection();//new MySqlConnection("Server=localhost; Database=hotel_wpf; UId=root; Pwd=1111; Pooling=false; Character Set=utf8 ");
        MySqlCommand da;
        MySqlDataReader data;
        DataTable data_Tour;
        public Tour()
        {
            InitializeComponent();
        }
        private void get_Data()

        {
            Panel.Children.Clear();

            string query = "SELECT TOUR_ID,TOUR_NAME,TOUR_SIZE,TOUR_LOCA,TOUR_PRICE,TOUR_IMAGE FROM tour  TOUR_NAME LIMIT 1000";
            DataTable dataTable = DataBase.Instance.ExecuteQuery(query);
            data_Tour = DataBase.Instance.ExecuteQuery(query);


            int row = 0;
            int column = 0;
            foreach (DataRow data in dataTable.Rows)
            {
                Tour_Booking item = new Tour_Booking();
                item.iD = data["TOUR_ID"].ToString();
                item.namE = data["TOUR_NAME"].ToString();
                item.sizE = data["TOUR_SIZE"].ToString();
                item.locatioN = data["TOUR_LOCA"].ToString();
                item.pricE = data["TOUR_PRICE"].ToString();

                if (data["Tour_Image"] != DBNull.Value)
                {
                    item.image = MainWindow.LoadImageFromFileName(data["Tour_Image"].ToString());
                }

                if (column == Panel.ColumnDefinitions.Count)
                {
                    row++;
                    column = 0;
                }
                Grid.SetRow(item, row);
                Grid.SetColumn(item, column++);

                Panel.Children.Add(item);
            }


            

        }
        private void Tour_Search(object sender, RoutedEventArgs e)
        {

            string query = "Tour_Name like '%" + textLocationBoxSearch.Text + "%'";
            DataRow[] search = data_Tour.Select(query);

            Panel.Children.Clear();
            int row = 0;
            int column = 0;
            foreach (DataRow data in search)
            {
                Tour_Booking item = new Tour_Booking();
                item.iD = data["TOUR_ID"].ToString();
                item.namE = data["TOUR_NAME"].ToString();
                item.sizE = data["TOUR_SIZE"].ToString();
                item.locatioN = data["TOUR_LOCA"].ToString();
                item.pricE = data["TOUR_PRICE"].ToString();

                if (data["Tour_Image"] != DBNull.Value)
                {
                    item.image = MainWindow.LoadImageFromFileName(data["Tour_Image"].ToString());
                }

                if (column == Panel.ColumnDefinitions.Count)
                {
                    row++;
                    column = 0;
                }
                Grid.SetRow(item, row);
                Grid.SetColumn(item, column++);

                Panel.Children.Add(item);
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            get_Data();
        }
        private void Abc(object sender, RoutedEventArgs e)
        {
            DateTime? datepicker = date.SelectedDate;
            var date1 = Convert.ToDateTime(datepicker).ToString("yyyy/MM/dd");

        }   
    }
}
