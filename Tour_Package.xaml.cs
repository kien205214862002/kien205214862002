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
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp2.Forms
{
    /// <summary>
    /// Interaction logic for Tour_Package.xaml
    /// </summary>
    public partial class Tour_Package : UserControl
    {
        MySqlConnection strCon = Connection.GetSqlConnection();
        public Tour_Package()
        {
            InitializeComponent();
        }
        public string NAME;
        public string namE
        {
            get { return NAME; }
            set { NAME = value; labelNameTour.Content = value; }
        }
        public string LOCATION;
        public string locatioN
        {
            get { return LOCATION; }
            set { LOCATION = value; labelTour_Country.Content = value; }
        }
        public BitmapImage Image;
        public BitmapImage image
        {
            get { return Image; }
            set { Image = value; ImageT.ImageSource = value; }
        }

    }
}
