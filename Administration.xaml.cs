
using System;
using System.Collections.Generic;
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
using FireSharp.Response;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.IO;
using Microsoft.Win32;
using System.Drawing;

namespace WpfApp2.Forms
{
    /// <summary>
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : UserControl
    {
        MySqlConnection strCon = Connection.GetSqlConnection();//new MySqlConnection("Server=localhost; Database=hotel_wpf; UId=root; Pwd=1111; Pooling=false; Character Set=utf8 ");
        private Button currentButton;
        private Border currentPanel;
        MySqlDataAdapter da;


        public Administration()
        {
            InitializeComponent();

        }
        public async void load_dataTour()
        {
            //strCon.Open();
            //da = new MySqlDataAdapter("SELECT TOUR_ID,TOUR_NAME, TOUR_TIME, TOUR_TYPE,TOUR_SIZE,TOUR_LOCA, TOUR_VIHICLES,TOUR_PRICE, TOUR_INTRODUCTION,TOUR_CONSISTS, TOUR_SCHEDULE FROM TOUR", strCon);
            //DataTable dataTable= new DataTable("Tour");
            //da.Fill(dataTable);
            //toursDataGrid.ItemsSource = dataTable.DefaultView;  
            // FirebaseResponse response = await client.GetTaskAsync("Tour/");
            //Data data=response.ResultAs<Data>();

            string query = "SELECT TOUR_ID,TOUR_NAME, TOUR_TIME,TOUR_DURATION,TOUR_SIZE,TOUR_LOCA, TOUR_VIHICLES,TOUR_PRICE, TOUR_INTRODUCTION,TOUR_CONSISTS, TOUR_SCHEDULE, Tour_Image FROM TOUR";
            DataTable dataTable = DataBase.Instance.ExecuteQuery(query);
            toursDataGrid.ItemsSource = dataTable.DefaultView;
            //DateTime date = DateTime.ParseExact(textT_Time.Text, "MM-dd-yyyy HH:mm:ss tt", null);
            //textT_Time.Text = (date.ToString("yyyy-MM-dd"));
            //MessageBox.Show(date.ToString());
        }
        public async void load_dataHotel()
        {
            //da = new MySqlDataAdapter("SELECT HOTEL_ID,HOTEL_NAME, HOTEL_TYPE, HOTEL_BED, HOTEL_LOCA,HOTEL_PRICE, HOTEL_DIS,HOTEL_POPU  FROM HOTEL", strCon);
            string query = "SELECT *  FROM HOTEL limit 100";
            DataTable dataTable = DataBase.Instance.ExecuteQuery(query);

            hotelDataGrid.ItemsSource = dataTable.DefaultView;
            // FirebaseResponse response = await client.GetTaskAsync("Tour/");
            //Data data=response.ResultAs<Data>();
        }
        public async void load_dataCar()
        {
            //da = new MySqlDataAdapter("SELECT CAR_ID,CAR_NAME, CAR_TYPE, CAR_SEAT,CAR_PICKUP,CAR_DROPOFF, CAR_PRICE,CAR_PACKAGE FROM CAR", strCon);
            //DataTable dataTable = new DataTable("Car");
            //da.Fill(dataTable);


            string query = "SELECT CAR_ID,CAR_NAME, CAR_TYPE, CAR_SEAT,CAR_PICKUP,CAR_DROPOFF, CAR_PRICE,CAR_PACKAGE,CAR_IMAGE FROM CAR";
            DataTable dataTable = DataBase.Instance.ExecuteQuery(query);
            carDataGrid.ItemsSource = dataTable.DefaultView;
            // FirebaseResponse response = await client.GetTaskAsync("Tour/");
            //Data data=response.ResultAs<Data>();
        }
        public async void load_dataPlane()
        {
            //da = new MySqlDataAdapter("SELECT TICKET_ID,TICKET_NAME, TICKET_COMPANY, TICKET_PRICE,TICKET_FROMID,TICKET_FROMLOCA, TICKET_TOID,TICKET_TOLOCA FROM PLANE_TICKET", strCon);
            //DataTable dataTable = new DataTable("Plane");
            //da.Fill(dataTable);


            string query = "SELECT * FROM PLANE_TICKET limit 100";
            DataTable dataTable = DataBase.Instance.ExecuteQuery(query);
            PlaneDataGrid.ItemsSource = dataTable.DefaultView;
            // FirebaseResponse response = await client.GetTaskAsync("Tour/");
            //Data data=response.ResultAs<Data>();
        }
        public async void load_dataUser()
        {
            //da = new MySqlDataAdapter("SELECT USER_ID,USER_NAME, USER_EMAIL, USER_PHONE,USER_BIRTH, USER_SEX  FROM USER", strCon);
            //DataTable dataTable = new DataTable("User");
            //da.Fill(dataTable);


            string query = "SELECT USER_ID,USER_NAME, USER_EMAIL, USER_PHONE, USER_SEX,USER_PASS,USER_AVATAR  FROM USER";
            DataTable dataTable = DataBase.Instance.ExecuteQuery(query);
            membersDataGrid.ItemsSource = dataTable.DefaultView;
            // FirebaseResponse response = await client.GetTaskAsync("Tour/");
            //Data data=response.ResultAs<Data>();
        }
        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

            var data = new Data
            {
                tourID = null,
                tourName = textT_Name.Text,
                tourTime = textT_Time.Text,
                tourDuration = textT_Duration.Text,
                tourSize = textT_Size.Text,
                tourVihicles = textT_Vehi.Text,
                tourPrice = textT_Price.Text,
                tourIntroduction = textT_Intro.Text,
                tourConsists = textT_Con.Text,
                tourSchedule = textT_Sche.Text,
                //   String tourImage = textT.Text;
            };
            //string query = "INSERT INTO TOUR(Tour_Name,Tour_Time,Tour_Type,Tour_Size,Tour_Loca,Tour_Vihicles,Tour_Price,Tour_InTroduction,Tour_Consists,Tour_Schedule) " +
            //    "VALUES('" + textT_Name.Text + "','" + textT_Time.Text + "','" + textT_Type.Text + "'" +
            //    "                                   ,'" + textT_Size.Text + "','" + textT_Loca.Text + "','" + textT_Vehi.Text + "','" + textT_Price.Text + "','" + textT_Intro.Text + "'" +
            //    "                                   ,'" + textT_Con.Text + "','" + textT_Sche.Text + "')";
            //MySqlCommand command=new MySqlCommand( query, strCon);

            if (imageT.Source != null)
            {
                data.tourImage = System.IO.Path.GetFileName(imageT.Source.ToString());
            }

            string query = "INSERT INTO TOUR(Tour_Name,Tour_Time,Tour_Duration,Tour_Size,Tour_Loca,Tour_Vihicles,Tour_Price,Tour_InTroduction,Tour_Consists,Tour_Schedule, Tour_Image) " +
                " Values( @Name , @Time , @Duration, @Size , @Loca , @Vihicles , @Price , @Intro , @Consists , @Schedule , @Image )";

            if (DataBase.Instance.ExecuteNonQuery(query,
                new object[] {textT_Name.Text, textT_Time.Text, textT_Duration.Text, textT_Size.Text, textT_Loca.Text,
                    textT_Vehi.Text, textT_Price.Text, textT_Intro.Text, textT_Con.Text, textT_Sche.Text, data.tourImage}) == 1)
            {
                MessageBox.Show("Successful");

            }
            textT_ID.Text = textT_Name.Text = textT_Time.Text = textT_Duration.Text = textT_Size.Text = textT_Vehi.Text
            = textT_Price.Text = textT_Intro.Text = textT_Con.Text = textT_Sche.Text = "";
        }
        private async void buttonAdd_Car_Click(object sender, RoutedEventArgs e)
        {
            add_DataCar();

        }
        private async void buttonAdd_Hotel_Click(object sender, RoutedEventArgs e)
        {
            add_DataHotel();

        }
        private async void buttonAdd_Ticket_Click(object sender, RoutedEventArgs e)
        {

            add_Data_PlaneTicket();
        }
        private async void buttonAdd_User_Click(object sender, RoutedEventArgs e)
        {
            add_DataUser();

        }

        async void add_Data_PlaneTicket()
        {

            //var data = new Data_PlaneTicket
            //{
            //    PlaneID = textA_ID.Text,
            //    PlaneName = textA_Class.Text,
            //    PlaneCompany = textA_Company.Text,
            //    PlanePrice = textA_Price.Text,
            //    PlaneFromID = textA_FromID.Text,
            //    PlaneFromLoca = textA_FromLoca.Text,
            //    PlaneToID = textA_ToID.Text,
            //    PlaneToloca = textA_ToLoca.Text,

            //    if(Image_Plane.Source != null)
            //        Plane_Image = 

            //};
            //string query = "INSERT INTO PLANE_TICKET VALUES('" + textA_ID.Text + "','" + textA_Class.Text + "','" + textA_Company.Text + "','" + textA_Price.Text + "'" +
            //     "                                   ,'" + textA_FromID.Text + "','" + textA_FromLoca.Text + "','" + textA_ToID.Text + "','" + textA_ToLoca.Text + "')";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            string query = "INSERT INTO PLANE_TICKET(Ticket_Name, Ticket_Company, Ticket_Price, Ticket_TimeGo, Ticket_FromLocation, Ticket_TimeTo, Ticket_ToLocation, Ticket_Image) " +
                " Values( @Name , @Company , @Price , @FromID , @FromLoca , @ToID , @ToLoca , @image )";

            string image = System.IO.Path.GetFileName(Image_Plane.Source.ToString());


            if (DataBase.Instance.ExecuteNonQuery(query,
                new object[] {textA_Class.Text, textA_Company.Text, textA_Price.Text, textA_FromID.Text,
                textA_FromLoca.Text, textA_ToID.Text, textA_ToLoca.Text, image}) == 1)
            {
                MessageBox.Show("Successful");

            }
            //strCon.Close();
            textA_ID.Text = textA_Class.Text = textA_Company.Text = textA_Price.Text = textA_FromID.Text =
            textA_FromLoca.Text = textA_ToID.Text = textA_ToLoca.Text = "";
        }
        async void add_DataCar()
        {

            //var data = new Data_Car
            //{
            //    CarID = textC_ID.Text,
            //    CarName = textC_Name.Text,
            //    CarType = textC_Type.Text,
            //    CarSeat = textC_Seat.Text,
            //    CarPickUp = textC_PickUp.Text,
            //    CarDropOff = textC_DropOff.Text,
            //    CarPrice = textC_Price.Text,
            //    CarPackage = textC_Pack.Text,
            //    //   String tourImage = textT.Text;
            //};
            //string query = "INSERT INTO CAR VALUES('" + textC_ID.Text + "','" + textC_Name.Text + "','" + textC_Type.Text + "','" + textC_Seat.Text + "'" +
            //   "                                   ,'" + textC_PickUp.Text + "','" + textC_DropOff.Text + "','" + textC_Price.Text + "','" + textC_Pack.Text+"')";
            //MySqlCommand command = new MySqlCommand(query, strCon);

            string query = "INSERT INTO CAR(Car_Name, Car_Type, Car_Seat, Car_PickUp, Car_DropOff, Car_Price, Car_Package, Car_Image) " +
                " Values( @Name , @Type , @Seat , @Pickup , @Dropoff , @Price , @Package , @Image )";

            string image = System.IO.Path.GetFileName(ImageC.Source.ToString());

            if (DataBase.Instance.ExecuteNonQuery(query,
                new object[] {textC_Name.Text, textC_Type.Text, textC_Seat.Text, textC_PickUp.Text,
                textC_DropOff.Text, textC_Price.Text, textC_Pack.Text, image}) == 1)
            {
                MessageBox.Show("Successful");

            }
            textC_ID.Text = textC_Name.Text = textC_Type.Text = textC_Seat.Text = textC_PickUp.Text = textC_DropOff.Text =
                textC_Price.Text = textC_Pack.Text = "";
        }
        async void add_DataHotel()
        {
            //string query = "INSERT INTO HOTEL VALUES('" + textH_ID.Text + "','" + textH_Name.Text + "','" + textH_Fac.Text + "','" + textH_Type.Text + "'" +
            //   "                                   ,'" + textH_Size.Text + "','" + textR_Bed.Text + "','" + textH_Loca.Text + "','" + textH_Price.Text + "','" + textH_Dis.Text + "','" + textH_Pop.Text + "')";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            if (textH_Price.Text == null || textH_Price.Text=="Hotel Price") MessageBox.Show("Enter Price Room Hotel,please!");
            else
            
            {
                try
                {
                    string query = "INSERT INTO HOTEL " +
                        "(Hotel_Name, Hotel_Fac, Hotel_Type, Hotel_Size, Hotel_Bed, Hotel_Loca, Hotel_Price, Hotel_Dis, Hotel_Popu, Hotel_Image)" +
                        " VALUES( @Name , @Fac , @Type , @Size , @Bed , @Loca , @Price , @Dis , @Pop , @Image )";
                    string image = System.IO.Path.GetFileName(imageH.Source.ToString());
                    if (DataBase.Instance.ExecuteNonQuery(query,
                        new object[] {textH_Name.Text, textH_Fac.Text, textH_Type.Text, textH_Size.Text,
                textR_Bed.Text, textH_Loca.Text, textH_Price.Text, textH_Dis.Text, textH_Pop.Text, image}) == 1)
                    {
                        MessageBox.Show("Successful");

                    }

                }
                catch(Exception ex) { 
                    MessageBox.Show("Choose Image Hotel, please!"); };
                textH_ID.Text = textH_Name.Text = textH_Type.Text = textH_Size.Text = textH_Dis.Text = textH_Pop.Text =
                textH_Price.Text = textR_Bed.Text = textH_Loca.Text = textH_Fac.Text = "";
            }
            
        }
        async void add_DataUser()
            {

                var data = new Data_User
                {
                    UserID = textU_ID.Text,
                    UserName = textU_Name.Text,
                    UserEmail = textU_Email.Text,
                    UserPhone = textU_Phone.Text,
                    UserSex = "",

                    //   String tourImage = textT.Text;
                };
            Modify modify = new Modify();
            strCon.Open();
            if (modify.TK("SELECT *FROM USER WHERE USER_EMAIL ='" + textU_Email.Text + "'").Count != 0)

                MessageBox.Show("Email này đã được đăng ký, vui lòng nhập Email khác.");
            else
            {

                string query = "INSERT INTO USER(User_Name, User_Email, User_Pass, User_Phone, User_Sex) " +
                    " Values( @Name , @Email , @Pass , @Phone , @Sex )";
                //  string avatar = System.IO.Path.GetFileName(ImageU.Source.ToString());

                int sex = (Male.IsChecked == true) ? 1 : 0;
                if (sex == 1)
                {
                    if (DataBase.Instance.ExecuteNonQuery(query,
                        new object[] {textU_Name.Text, textU_Email.Text, textU_Pass.Text,
                textU_Phone.Text, "Male"}) == 1)
                    {
                        MessageBox.Show("Successful");
                    }
                    textU_ID.Text = textU_Name.Text = textU_Email.Text = textU_Phone.Text = textU_Pass.Text = "";
                }
                else
                if (sex == 0)
                {
                    if (DataBase.Instance.ExecuteNonQuery(query,
                        new object[] {textU_Name.Text, textU_Email.Text, textU_Pass.Text,
                textU_Phone.Text, "Female"}) == 1)
                    {
                        MessageBox.Show("Successful");
                    }
                    textU_ID.Text = textU_Name.Text = textU_Email.Text = textU_Phone.Text = textU_Pass.Text = "";
                }
            }
            //if (Male.IsChecked==true)
            //{
            //    string query = "INSERT INTO USER VALUES('" + textU_ID.Text + "','" + textU_Name.Text + "','" + textU_Email.Text + "','" + textU_Phone.Text + "'" +
            //                   "                                   ,'" + Time.Text + "','" + 1 + " ')";
            //    MySqlCommand command = new MySqlCommand(query, strCon);
            //    if (command.ExecuteNonQuery() == 1)
            //    {
            //        MessageBox.Show("Successful");

            //    }
            //    textU_ID.Text = textU_Name.Text = textU_Email.Text = textU_Phone.Text = "";
            //} else
            //     if (Female.IsChecked == true)
            //{
            //    string query = "INSERT INTO USER VALUES('" + textU_ID.Text + "','" + textU_Name.Text + "','" + textU_Email.Text + "','" + textU_Phone.Text + "'" +
            //                   "                                   ,'" + Time.Text + "','" + 0 + " ')";
            //    MySqlCommand command = new MySqlCommand(query, strCon);
            //    if (command.ExecuteNonQuery() == 1)
            //    {
            //        MessageBox.Show("Successful");

            //    }
            //    textU_ID.Text = textU_Name.Text = textU_Email.Text = textU_Phone.Text = "";
            //}

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

        private void buttonTour_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderTour);
        }
        private void buttonPlane_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderAirTicket);
        }

        private void buttonCar_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderCarRental);
        }
        
            private void buttonHotel_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderHotel);
        }
        private void buttonUser_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender, boderUser);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            load_dataTour();
            load_dataHotel();
            load_dataCar();
            load_dataPlane();
            load_dataUser();
            currentButton = buttonTour;
            currentPanel = boderTour;

            
        }
       
        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            //string query = "UPDATE TOUR SET TOUR_NAME='" + textT_Name.Text + "',TOUR_TIME='"+ textT_Time.Text + "',TOUR_TYPE'"+ textT_Type.Text + "',  TOUR_SIZE='"+ textT_Size.Text + "',TOUR_VIHICLES='"+ textT_Vehi.Text + "', TOUR_PRICE='"+ textT_Price.Text + "',TOUR_INTRODUCTION= '"+ textT_Intro.Text + "',TOUR_CONSISTS='"+ textT_Con.Text + "',TOUR_SCHEDULE= '"+ textT_Sche.Text + "' WHERE TOUR_ID='" + textT_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}

            string query;

            DateTime time = DateTime.Parse(textT_Time.Text);

            string tourImage = "";
            if (imageT.Source != null)
            {
                tourImage = System.IO.Path.GetFileName(imageT.Source.ToString());
                query = "UPDATE TOUR SET TOUR_NAME = @Name , Tour_Time = @Time , Tour_Duration = @Duration , Tour_Size = @Size , Tour_Vihicles = @Vehi , Tour_Price = @Price , Tour_Introduction = @Intro , Tour_Consists = @Consists , Tour_Schedule = @Schedule , Tour_Image = @Image " +
                " where Tour_ID = @ID ";

                if (DataBase.Instance.ExecuteNonQuery(query,
                new object[] {textT_Name.Text, time.ToString("yyyy-MM-dd"), textT_Duration.Text, textT_Size.Text, textT_Vehi.Text,
                    textT_Price.Text, textT_Intro.Text, textT_Con.Text, textT_Sche.Text, tourImage, int.Parse(textT_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");
                }
            }
            else
            {
                query = "UPDATE TOUR SET TOUR_NAME = @Name , Tour_Time = @Time , Tour_Duration = @Duration , Tour_Size = @Size , Tour_Vihicles = @Vehi , Tour_Price = @Price , Tour_Introduction = @Intro , Tour_Consists = @Consists , Tour_Schedule = @Schedule " +
                " where Tour_ID = @ID ";

                if (DataBase.Instance.ExecuteNonQuery(query,
                new object[] {textT_Name.Text, time.ToString("yyyy-MM-dd"), textT_Duration.Text, textT_Size.Text, textT_Vehi.Text,
                    textT_Price.Text, textT_Intro.Text, textT_Con.Text, textT_Sche.Text, int.Parse(textT_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");
                }
            }

            

        }
        private void buttonUpdate_Hotel_Click(object sender, RoutedEventArgs e)
        {
            string hotelImage = "";
            if (imageH.Source != null)
            {
                hotelImage = System.IO.Path.GetFileName(imageH.Source.ToString());

                string query = "Update Hotel Set Hotel_Name = @Name , Hotel_Fac = @Fac , Hotel_Type = @Type , Hotel_Size = @Size , Hotel_Bed = @Bed , Hotel_Loca = @Location , Hotel_Price = @Price , Hotel_Dis = @Dis , Hotel_Popu = @Popu , Hotel_Image = @Image " +
                " where Hotel_ID = @ID ";
                if (DataBase.Instance.ExecuteNonQuery(query,
                    new object[] {textH_Name.Text, textH_Fac.Text, textH_Type.Text, textH_Size.Text,
                textR_Bed.Text, textH_Loca.Text, textH_Price.Text, textH_Dis.Text, textH_Pop.Text, hotelImage, int.Parse(textH_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");

                }
            }
            else
            {
                string query = "Update Hotel Set Hotel_Name = @Name , Hotel_Fac = @Fac , Hotel_Type = @Type , Hotel_Size = @Size , Hotel_Bed = @Bed , Hotel_Loca = @Location , Hotel_Price = @Price , Hotel_Dis = @Dis , Hotel_Popu = @Popu " +
                " where Hotel_ID = @ID ";
                if (DataBase.Instance.ExecuteNonQuery(query,
                    new object[] {textH_Name.Text, textH_Fac.Text, textH_Type.Text, textH_Size.Text,
                textR_Bed.Text, textH_Loca.Text, textH_Price.Text, textH_Dis.Text, textH_Pop.Text, int.Parse(textH_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");

                }
            }
            /*if (imageH.Source != null)
            {

                hotelImage = System.IO.Path.GetFileName(imageH.Source.ToString());

                string query = "Update Hotel Set Hotel_Name = @Name , Hotel_Fac = @Fac , Hotel_Type = @Type , Hotel_Size = @Size , Hotel_Bed = @Bed , Hotel_Loca = @Location , Hotel_Price = @Price , Hotel_Image=@Image, Hotel_Dis = @Dis , Hotel_Popu = @Popu " +
                    " where Hotel_ID = @ID ";
                if (DataBase.Instance.ExecuteNonQuery(query,
                    new object[] {textH_Name.Text, textH_Fac.Text, textH_Type.Text, textH_Size.Text,
                textR_Bed.Text, textH_Loca.Text, textH_Price.Text,hotelImage, textH_Dis.Text, textH_Pop.Text, int.Parse(textH_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");

                }
            }
            else
            {


                string query = "Update Hotel Set Hotel_Name = @Name , Hotel_Fac = @Fac , Hotel_Type = @Type , Hotel_Size = @Size , Hotel_Bed = @Bed , Hotel_Loca = @Location , Hotel_Price = @Price , Hotel_Dis = @Dis , Hotel_Popu = @Popu " +
                    " where Hotel_ID = @ID ";
                if (DataBase.Instance.ExecuteNonQuery(query,
                    new object[] {textH_Name.Text, textH_Fac.Text, textH_Type.Text, textH_Size.Text,
                textR_Bed.Text, textH_Loca.Text, textH_Price.Text, textH_Dis.Text, textH_Pop.Text, int.Parse(textH_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");

                }
            }*/
        }
        private void buttonUpdate_Car_Click(object sender, RoutedEventArgs e)
        {
            //string query = "UPDATE CAR SET CAR_NAME='" + textC_Name.Text + "',CAR_TYPE='" + textC_Type.Text + "',CAR_SEAT'" + textC_Seat.Text + "',  CAR_PICKUP='" + textC_PickUp.Text + "',CAR_DROPOFF='" + textC_DropOff.Text + "', CAR_PRICE='" + textC_Price.Text + "',CAR_PACKAGE= '" + textC_Pack.Text + "' WHERE CAR_ID='" + textC_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}
            string carImage = "";
            if (ImageC.Source != null)
            {
                carImage = System.IO.Path.GetFileName(ImageC.Source.ToString());
                string query = "Update Car Set Car_Name = @Name , Car_Type = @Type , Car_Seat = @Seat , Car_PickUp = @PickUp , Car_DropOff = @DropOff , Car_Price = @Price , Car_Package = @Package , Car_Image = @Image " +
                " where Car_ID = @ID ";
                if (DataBase.Instance.ExecuteNonQuery(query,
                   new object[] {textC_Name.Text, textC_Type.Text, textC_Seat.Text, textC_PickUp.Text,
                textC_DropOff.Text, textC_Price.Text, textC_Pack.Text, carImage, int.Parse(textC_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");

                }
            }
            else
            {
                {
                    string query = "Update Car Set Car_Name = @Name , Car_Type = @Type , Car_Seat = @Seat , Car_PickUp = @PickUp , Car_DropOff = @DropOff , Car_Price = @Price , Car_Package = @Package " +
                    " where Car_ID = @ID ";
                    if (DataBase.Instance.ExecuteNonQuery(query,
                       new object[] {textC_Name.Text, textC_Type.Text, textC_Seat.Text, textC_PickUp.Text,
                textC_DropOff.Text, textC_Price.Text, textC_Pack.Text, int.Parse(textC_ID.Text)}) == 1)
                    {
                        MessageBox.Show("Successful");

                    }
                }
            }

        }
        private void buttonUpdate_Plane_Click(object sender, RoutedEventArgs e)
        {
            //string query = "UPDATE PLANE SET PLANE_NAME='" + textA_Class.Text + "',PLANE_COMPANY='" + textA_Company.Text + "',PLANE_PRICE'" + textA_Price.Text + "',  PLANE_FROMID='" + textA_FromID.Text + "',PLANE_FROMLOCA='" + textA_FromLoca.Text+"' WHERE PLANE_ID='" + textA_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}
            string planeImage = "";
            if (Image_Plane.Source != null)
            {
                planeImage = System.IO.Path.GetFileName(Image_Plane.Source.ToString());
                string query = "Update Plane_Ticket Set Ticket_Name = @Name , Ticket_Company = @Company , Ticket_Price = @Price , Ticket_TimeGo = @TimeGo,Ticket_TimeTo = @TimeTo , Ticket_FromLocation = @FromLocation, Ticket_ToLocation = @Tolocation, Ticket_Image = @Image " +
                " where Ticket_ID = @ID ";
                if (DataBase.Instance.ExecuteNonQuery(query,
                   new object[] {textA_Class.Text, textA_Company.Text, textA_Price.Text, textA_FromID.Text,textA_ToID.Text,
                textA_FromLoca.Text,textA_ToLoca.Text, planeImage, int.Parse(textA_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");

                }
            }
        }
        private void buttonUpdate_User_Click(object sender, RoutedEventArgs e)
        {
            //string query = "UPDATE USER SET USER_NAME='" + textU_Name.Text + "',USER_EMAIL='" + textU_Email.Text + "',USER_PHONE'" + textU_Phone.Text + "' WHERE USER_ID='" + textU_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}
            string image = "";
            if (ImageC.Source != null)
            {
                image = System.IO.Path.GetFileName(ImageU.Source.ToString());
                string query = "Update User Set User_Name = @Name , User_Email = @Email , User_Phone = @Phone ,User_Avatar=@image " +
                " where User_ID = @ID ";
                if (DataBase.Instance.ExecuteNonQuery(query,
                    new object[] {textU_Name.Text, textU_Email.Text,
                textU_Phone.Text, image, int.Parse(textU_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");
                }
            }
            {
                string query = "Update User Set User_Name = @Name , User_Email = @Email , User_Phone = @Phone " +
                " where User_ID = @ID ";
                if (DataBase.Instance.ExecuteNonQuery(query,
                    new object[] {textU_Name.Text, textU_Email.Text,
                textU_Phone.Text, int.Parse(textU_ID.Text)}) == 1)
                {
                    MessageBox.Show("Successful");
                }
            }
        }
        private void toursDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DateTime date;
            //MessageBox.Show(toursDataGrid.Items.Count.ToString(), toursDataGrid.SelectedIndex.ToString());
            //if (toursDataGrid.SelectedIndex.ToString() != null)
            if(toursDataGrid.SelectedIndex < toursDataGrid.Items.Count - 1)
            {
                DataRowView drv = (DataRowView)toursDataGrid.SelectedItem;
                if (drv != null)
                {
                    textT_ID.Text = drv["TOUR_ID"].ToString();
                    textT_Name.Text = drv["TOUR_NAME"].ToString();
                    textT_Time.Text = Convert.ToDateTime(drv["TOUR_TIME"]).ToString("yyyy/MM/dd");
                    textT_Duration.Text = drv["TOUR_Duration"].ToString();
                    textT_Size.Text = drv["TOUR_SIZE"].ToString();
                    textT_Loca.Text = drv["TOUR_LOCA"].ToString();
                    textT_Vehi.Text = drv["TOUR_VIHICLES"].ToString();
                    textT_Price.Text = drv["TOUR_PRICE"].ToString();
                    textT_Intro.Text = drv["TOUR_INTRODUCTION"].ToString();
                    textT_Con.Text = drv["TOUR_CONSISTS"].ToString();
                    textT_Sche.Text = drv["TOUR_SCHEDULE"].ToString();

                    imageT.Source = MainWindow.LoadImageFromFileName(drv["Tour_Image"].ToString());
                }
            }
            else MessageBox.Show("!");

        }
        private void tourHotelGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (hotelDataGrid.SelectedIndex < hotelDataGrid.Items.Count - 1)
            {
                DataRowView drv = (DataRowView)hotelDataGrid.SelectedItem;
                if (drv != null)
                {
                    textH_ID.Text = drv["HOTEL_ID"].ToString();
                    textH_Name.Text = drv["HOTEL_NAME"].ToString();
                    //textH_Fac.Text = drv["HOTEL_FAC"].ToString();
                    textH_Type.Text = drv["HOTEL_TYPE"].ToString();
                    textH_Size.Text = drv["HOTEL_SIZE"].ToString();
                    textR_Bed.Text = drv["HOTEL_BED"].ToString();
                    textH_Loca.Text = drv["HOTEL_LOCA"].ToString();
                    textH_Price.Text = drv["HOTEL_PRICE"].ToString();
                    textH_Dis.Text = drv["HOTEL_DIS"].ToString();
                    textH_Pop.Text = drv["HOTEL_POPU"].ToString();

                    if (drv["Hotel_Image"] != DBNull.Value)
                        imageH.Source = MainWindow.LoadImageFromFileName(drv["Hotel_Image"].ToString());
                }
                else MessageBox.Show("!");
            }
        }
        private void tourCarGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (carDataGrid.SelectedIndex < carDataGrid.Items.Count - 1)
            {
                DataRowView drv = (DataRowView)carDataGrid.SelectedItem;
                if (drv != null)
                {
                    textC_ID.Text = drv["CAR_ID"].ToString();
                    textC_Name.Text = drv["CAR_NAME"].ToString();
                    textC_Type.Text = drv["CAR_TYPE"].ToString();
                    textC_Seat.Text = drv["CAR_SEAT"].ToString();
                    textC_PickUp.Text = drv["CAR_PICKUP"].ToString();
                    textC_DropOff.Text = drv["CAR_DROPOFF"].ToString();
                    textC_Price.Text = drv["CAR_PRICE"].ToString();
                    textC_Pack.Text = drv["CAR_PACKAGE"].ToString();
                    if (drv["Car_Image"] != DBNull.Value)
                       ImageC.Source = MainWindow.LoadImageFromFileName(drv["Car_Image"].ToString());
                }
                else MessageBox.Show("!");
            }
        }
        private void tourPlaneGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (PlaneDataGrid.SelectedIndex < PlaneDataGrid.Items.Count - 1)
            {
                DataRowView drv = (DataRowView)PlaneDataGrid.SelectedItem;
                if (drv != null)
                {
                    textA_ID.Text = drv["TICKET_ID"].ToString();
                    textA_Class.Text = drv["TICKET_NAME"].ToString();
                    textA_Company.Text = drv["TICKET_COMPANY"].ToString();
                    textA_Price.Text = drv["TICKET_PRICE"].ToString();
                    textA_FromID.Text = drv["TICKET_TIMEGO"].ToString();
                    textA_FromLoca.Text = drv["TICKET_TIMETO"].ToString();
                    textA_ToID.Text = drv["TICKET_FROMLOCATION"].ToString();
                    textA_ToLoca.Text = drv["TICKET_TOLOCATION"].ToString();
                    if (drv["Ticket_Image"] != DBNull.Value)
                        Image_Plane.Source = MainWindow.LoadImageFromFileName(drv["Ticket_Image"].ToString());
                }
                else MessageBox.Show("!");
            }
        }
        private void tourUserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (membersDataGrid.SelectedIndex < membersDataGrid.Items.Count-1)
            {
                DataRowView drv = (DataRowView)membersDataGrid.SelectedItem;
                if (drv != null)
                {
                    textU_ID.Text = drv["USER_ID"].ToString();
                    textU_Name.Text = drv["USER_NAME"].ToString();
                    textU_Email.Text = drv["USER_EMAIL"].ToString();
                    textU_Phone.Text = drv["USER_PHONE"].ToString();
                    textU_Pass.Text = drv["USER_Pass"].ToString();
                    if (drv["USER_AVATAR"] != DBNull.Value)
                        ImageU.Source = MainWindow.LoadImageFromFileName(drv["USER_AVATAR"].ToString());
                }
                else MessageBox.Show("!");
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            //string query = "DELETE FROM TOUR WHERE TOUR_ID='"+ textT_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}
            string query = "Delete from Tour where Tour_ID = @ID ";
            if(DataBase.Instance.ExecuteNonQuery(query, new object[] {int.Parse(textT_ID.Text)}) == 1)
            {
                MessageBox.Show("Successful");
            }
        }
        private void buttonDelete_Car_Click(object sender, RoutedEventArgs e)
        {
            //string query = "DELETE FROM TOUR WHERE TOUR_ID='" + textC_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}
            string query = "Delete from Car where Car_ID = @ID ";
            if (DataBase.Instance.ExecuteNonQuery(query, new object[] { int.Parse(textC_ID.Text) }) == 1)
            {
                MessageBox.Show("Successful");
            }
        }
        private void buttonDelete_Hotel_Click(object sender, RoutedEventArgs e)
        {
            //string query = "DELETE FROM TOUR WHERE TOUR_ID='" + textH_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}
            string query = "Delete from Hotel where Hotel_ID = @ID ";
            if (DataBase.Instance.ExecuteNonQuery(query, new object[] { int.Parse(textH_ID.Text) }) == 1)
            {
                MessageBox.Show("Successful");
            }
        }
        private void buttonDelete_Plane_Click(object sender, RoutedEventArgs e)
        {
            //string query = "DELETE FROM TOUR WHERE TOUR_ID='" + textA_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}
            string query = "Delete from Plane_Ticket where Ticket_ID = @ID ";
            if (DataBase.Instance.ExecuteNonQuery(query, new object[] { int.Parse(textA_ID.Text) }) == 1)
            {
                MessageBox.Show("Successful");
            }
        }
        private void buttonDelete_User_Click(object sender, RoutedEventArgs e)
        {
            //string query = "DELETE FROM TOUR WHERE TOUR_ID='" + textU_ID.Text + "' ";
            //MySqlCommand command = new MySqlCommand(query, strCon);
            //if (command.ExecuteNonQuery() == 1)
            //{
            //    MessageBox.Show("Successful");

            //}
            string query = "Delete from User where User_ID = @ID ";
            if (DataBase.Instance.ExecuteNonQuery(query, new object[] { int.Parse(textU_ID.Text) }) == 1)
            {
                MessageBox.Show("Successful");
            }
        }

        private void ImageT_Clicked(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(MainWindow.GetAndSaveImage());
            bm.EndInit();
            imageT.Source = bm;
   
        }

        private void ImageH_Clicked(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(MainWindow.GetAndSaveImage());
            bm.EndInit();
            imageH.Source = bm;
        }

        private void ImageP_Clicked(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(MainWindow.GetAndSaveImage());
            bm.EndInit();
            Image_Plane.Source = bm;
        }

        private void ImageC_Clicked(object sender, MouseButtonEventArgs e)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(MainWindow.GetAndSaveImage());
            bm.EndInit();
            ImageC.Source = bm;
        }
        private void ImageU_Clicked(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void buttonUpdate_C_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDelete_C_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDelete_C_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void buttonUpdate_A_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
