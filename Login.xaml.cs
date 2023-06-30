using MaterialDesignThemes.Wpf;
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

namespace WpfApp2
{
       /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        public class LuuThongTin
        {
            static public string Email = "Đang cập nhật";
            static public string ID;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void iconClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection strCon = Connection.GetSqlConnection();//new MySqlConnection("Server=localhost; Database=hotel_wpf; UId=root; Pwd=1111; Pooling=false; Character Set=utf8 ");
            strCon.Open();
            string user = textUserName.Text;
            string pass= textPass.Password.ToString();
            if (textUserName.Text.Trim() == "") MessageBox.Show("Mời bạn nhập Email đăng nhập.");
            else
            if (textPass.Password.ToString().Trim() == "") MessageBox.Show("Mời bạn nhập mặt khẩu.");
            else

            {
                string sql = "select * from User where User_Email='" + textUserName.Text + "'and User_Pass='" + textPass.Password.ToString() + "'";
                MySqlCommand cmd = new MySqlCommand(sql, strCon);
                MySqlDataReader data = cmd.ExecuteReader();
                if (data.Read() == true)
                {
                    LuuThongTin.Email = textUserName.Text;
                    LuuThongTin.ID = data["User_ID"].ToString();
                    MessageBox.Show(" Đăng nhập thành công.");
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else MessageBox.Show(" Đăng nhập không thành công, lỗi mật khẩu hoặc tài khoản.");
                data.Close();
            }
            strCon.Close();
        }

        private void checkBoxPass_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUp s1 = new SignUp();
            s1.Show();
            this.Close();
            
        }      
    }
}
