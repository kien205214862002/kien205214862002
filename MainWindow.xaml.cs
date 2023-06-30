using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Forms;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using System.IO;
using Microsoft.Win32;
using Org.BouncyCastle.Utilities.Encoders;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public String Source = "";

        private UserControl currentChildForm;
        private Button currentButton;
        //  IFirebaseConfig config = new FirebaseConfig { AuthSecret = "Fl7sCjIzoHwpSJjtVO501T21vVAXcsd6QyLKFSuv", BasePath = "https://wpff-debb4-default-rtdb.firebaseio.com" };
        //IFirebaseClient client;
        public static BitmapImage LoadImageFromFileName(string filename)
        {
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri(Source + filename);
            bm.EndInit();
            return bm;
        }

        public static string GetAndSaveImage()
        {
            Byte[] BytesOfImage;

            OpenFileDialog ofdPatient = new OpenFileDialog();


            bool? Result = ofdPatient.ShowDialog();

            if (Result == true)
            {

                string path_with_image = ofdPatient.FileName;

                try
                {
                    try
                    {
                        FileStream fsRead = new FileStream(path_with_image, FileMode.Open);
                        BytesOfImage = new Byte[fsRead.Length];
                        fsRead.Read(BytesOfImage, 0, BytesOfImage.Length);
                        fsRead.Close();
                    }
                    catch { return ""; }

                    string filename = System.IO.Path.GetFileName(path_with_image);
                    string destFolder = System.IO.Path.Combine(MainWindow.Source, filename);
                    System.IO.File.Copy(path_with_image, destFolder, true);

                    return path_with_image;
                }
                catch { }

            }
            return "";
        }


        public MainWindow()
        {
            InitializeComponent();

            // lấy source để chứa image
            Source = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Image\\";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Email.Text = Login.LuuThongTin.Email;
            currentButton = ButtonHome;
            OpenChildForm(new Home());
            string query = "Select * from User where User_Email = @Email ";
            DataTable data = DataBase.Instance.ExecuteQuery(query, new object[] { Login.LuuThongTin.Email });


            foreach (DataRow dr in data.Rows)
            {
                if (dr["User_Avatar"] != DBNull.Value)
                    avatar.Source = MainWindow.LoadImageFromFileName(dr["User_Avatar"].ToString());
            }

            //    client = new FireSharp.FirebaseClient(config);
            //if(client!= null) { MessageBox.Show("hehe"); };
        }

        void Button_Choose(object senderButton)
        {
            if(senderButton != null)
            {
                DisableButton();
                currentButton = (Button)senderButton;
                currentButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#F7F6F4");
                currentButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FB7657");
                currentButton.FontWeight = FontWeights.Bold;
            }
        }

        public void DisableButton()
        {
            if (currentButton != null)
            {
                currentButton.Background = Brushes.Transparent;
                currentButton.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#F7F6F4");
                currentButton.FontWeight = FontWeights.Normal;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                // lúc click vô image tour nó lỗi nên remove dòng dưới
                //this.DragMove();
            }
        }

        void OpenChildForm(UserControl form)
        {
            if (currentChildForm != null)
            {
                panelMain.Children.Clear();
            }

            currentChildForm = form;          
            panelMain.Children.Add(currentChildForm);
        }

        private void Home_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            OpenChildForm(new Home());
        }

        private void Tour_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            OpenChildForm(new Tour());
        }

        private void Hotel_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            OpenChildForm(new Hotel());
        }

        private void Admin_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            if (Login.LuuThongTin.Email != "admin@gmail.com")
            {
                MessageBox.Show("Bạn không có quyền truy cập vào mục này!");
            }
            else
            {
                OpenChildForm(new Administration());
                String a = "fdfdaf";
            }   
        }

        private void Flight_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            OpenChildForm(new Plane_Ticket());
        }

        private void Car_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            OpenChildForm(new Car_Rental());
        }

        private void Chart_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            OpenChildForm(new Chart());
        }

        private void AboutUs_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            OpenChildForm(new About_Us());
        }   

        private void Profile_Change_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            OpenChildForm(new Profile());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Button_Choose(sender);
            if (MessageBox.Show("Do you want to sign out?", "Notification", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();            
            }
        }
    }
}