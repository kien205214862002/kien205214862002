using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2.Forms
{
    /// <summary>
    /// Interaction logic for Tour_Details.xaml
    /// </summary>
    public partial class Tour_Details : Window
    {
        

        private Button currentButton;
        private Border currentPanel;
        MySqlConnection strCon = Connection.GetSqlConnection();//new MySqlConnection("Server=localhost; Database=hotel_wpf; UId=root; Pwd=1111; Pooling=false; Character Set=utf8 ");
        MySqlCommand da;
        MySqlDataReader data;

        string Id_Tour = null;
        public Tour_Details()
        {
            InitializeComponent();
        }

        public Tour_Details(string Id)
        {
            InitializeComponent();
            Id_Tour = Id;
            load_Data();
        }
        private void get_Data()

        {

            string query = "SELECT TOUR_ID,TOUR_NAME,TOUR_SIZE,TOUR_LOCA,TOUR_PRICE,TOUR_IMAGE FROM tour  TOUR_NAME LIMIT 1000";
            DataTable dataTable = DataBase.Instance.ExecuteQuery(query);
            }

            public void load_Data()
        {
            //Tour_Booking item = new Tour_Booking();
            //string data_id = item.Data_ID;
            //    strCon.Open();
            //da = new MySqlCommand("SELECT TOUR_NAME,TOUR_DURATION,TOUR_TYPE,TOUR_VIHICLES,TOUR_DEPARTURE,TOUR_INTRODUCTION,TOUR_SIZE,TOUR_LOCA, TOUR_PRICE FROM TOUR  WHERE TOUR_ID= '" + 1 + "'", strCon);
            //data = da.ExecuteReader();
            //while (data.Read())
            //{
            //    Name.Text = data["TOUR_NAME"].ToString();
            //    Duration.Text = data["TOUR_DURATION"].ToString();
            //    Durations.Text = data["TOUR_DURATION"].ToString();
            //    Location.Text = data["TOUR_LOCA"].ToString();
            //    Type.Text = data["TOUR_Type"].ToString();
            //    Size.Text = data["TOUR_Size"].ToString();
            //    Vehicles.Text = data["TOUR_Vihicles"].ToString();
            //    Departure.Text = data["TOUR_Departure"].ToString();
            //    Price.Text = data["TOUR_Price"].ToString();
            //    Intro.Text = data["TOUR_Introduction"].ToString();

            //}
            //data.Close();
            //strCon.Close();

            string query = "SELECT TOUR_NAME,TOUR_TIME,TOUR_DURATION,TOUR_VIHICLES,TOUR_DEPARTURE,TOUR_INTRODUCTION,TOUR_SIZE,TOUR_LOCA, TOUR_PRICE, Tour_Image FROM TOUR  WHERE TOUR_ID = @Id";

            DataTable data = DataBase.Instance.ExecuteQuery(query, new object[] { Id_Tour });
            if(data.Rows.Count != 0)
            {
                Name.Text = data.Rows[0]["TOUR_NAME"].ToString();
                Durations.Text = data.Rows[0]["TOUR_Duration"].ToString();
                Duration.Text = Convert.ToDateTime(data.Rows[0]["TOUR_Time"]).ToString("yyyy/MM/dd");
                //Durations.Text = data.Rows[0]["TOUR_DURATION"].ToString();
                Location.Text = data.Rows[0]["TOUR_LOCA"].ToString();
                Size.Text = data.Rows[0]["TOUR_Size"].ToString();
                Vehicles.Text = data.Rows[0]["TOUR_Vihicles"].ToString();
                Departure.Text = data.Rows[0]["TOUR_Departure"].ToString();
                Price.Text = data.Rows[0]["TOUR_Price"].ToString();
                Intro.Text = data.Rows[0]["TOUR_Introduction"].ToString();
                imageT_main.ImageSource = MainWindow.LoadImageFromFileName(data.Rows[0]["Tour_Image"].ToString());
            }
            
        }
        public void set_Data_Cus()
        {
            //strCon.Open();
            //string query = "INSERT INTO TOUR_CUS (CUS_NAME,CUS_EMAIL,CUS_PHONE,CUS_REGION,CUS_DATESTART,CUS_DATEEND,CUS_MEMBER) VALUES('" + Name_Cus.Text + "','" + Email.Text+ "','" + Phone.Text + "','" + Adress.Text +  "','" + Date_Start.Text + "','" + Date_End.Text + "','" + Size_Cus.Text + "')";
            //MySqlCommand command = new MySqlCommand(query, strCon);


            Total.Text = (int.Parse(Price.Text) * int.Parse(Size_Cus.Text)).ToString();
            // Này thêm điều kiện nhập, ko nhập ko cho book
            int temp = 0; // để dùng tryParse
            if(Name_Cus.Text == "" || Email.Text == "" || Phone.Text == ""  
                 || !int.TryParse(Size_Cus.Text, out temp)
                || !int.TryParse(Total.Text, out temp))
            {
                MessageBox.Show("Yêu cầu nhập đủ thông tin trước khi đặt tour.");
                return;
            }
            
            string query = "INSERT INTO TOUR_CUS( Cus_Name, Cus_Email, Cus_Phone, " +
                "Cus_member, Cus_Price, User_Email) " +
                "values ( @Name , @Mail , @Phone  ,  @Member , @Price , @Email )";

            
            if (DataBase.Instance.ExecuteNonQuery(query, 
                new object[] {Name_Cus.Text, Email.Text, Phone.Text, 
                     Size_Cus.Text, Total.Text, Login.LuuThongTin.Email }) == 1)
            {
                MessageBox.Show("Successful");

            }
            //strCon.Close();


        }

        void Button_Choose(object senderButton, Border boder)
        {
            if (senderButton != null)
            {
                DisableButton();
                currentButton = (Button)senderButton;
                boder.Visibility = Visibility.Visible;
                currentPanel = boder;
                currentButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FB7657");
                currentButton.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FB7657");
            }
        }

        public void DisableButton()
        {
            if (currentButton != null)
            {
                currentPanel.Visibility = Visibility.Hidden;
                currentButton.Background = Brushes.Transparent;
                currentButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#121518");
                currentButton.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("Transparent");
            }
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void buttonFacili_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderFacil);
        }

        private void buttonIntro_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderIntro);
        }

        private void buttonSchedule_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderSchedule);
        }

        private void buttonConsists_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderConsists);
        }

        private void buttonComment_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderComment);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            currentButton = buttonFacili;
            currentPanel = boderFacil;
            load_Data();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonBook_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment();
            set_Data_Cus();
            payment.ShowDialog();
        }

        private void Size_Cus_TextChanged(object sender, TextChangedEventArgs e)


        {



        }
    }
}
