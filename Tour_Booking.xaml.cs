using MySql.Data.MySqlClient;
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
using System.Data;
using MySql.Data.MySqlClient;
namespace WpfApp2.Forms
{
    /// <summary>
    /// Interaction logic for Tour_Booking.xaml
    /// </summary>
    public partial class Tour_Booking : UserControl

    {
        public string Data_ID;
       

        public string ID;
        public string iD
        {
            get { return ID; }
            set { ID = value; Id.Text = value; }
        }
        public string NAME;
        public string namE
        {
            get { return NAME; }
            set { NAME = value; name.Text = value; }
        }
        public string LOCATION;
        public string locatioN
        {
            get { return LOCATION; }
            set { LOCATION = value; Location.Text = value; }
        }
        public string SIZE;
        public string sizE
        {
            get { return SIZE; }
            set { SIZE = value; Size.Text = value; }
        }
        public string PRICE;
        public string pricE
        {
            get { return PRICE; }
            set { PRICE = value; Price.Text = value; }
        }
        public BitmapImage Image;
        public BitmapImage image
        {
            get { return Image; }
            set { Image = value; ImageT.ImageSource = value; }
        }

        public Tour_Booking()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            Data_ID = Id.Text;
            Tour_Details tour_Details = new Tour_Details(Data_ID);
            tour_Details.ShowDialog();
        }
    }
}
