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
using WpfApp2;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        MySqlConnection strCon = Connection.GetSqlConnection();//new MySqlConnection("Server=localhost; Database=hotel_wpf; UId=root; Pwd=1111; Pooling=false; Character Set=utf8 ");
        MySqlCommand da;
        MySqlDataReader data;
        public SignUp()
        {
            InitializeComponent();
        }

        private void SU_iconClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void checkBoxPass_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            Modify modify = new Modify();
            strCon.Open();
            if (textSU_UserName.Text.Trim() == "") MessageBox.Show("Mời bạn nhập Email.");
            else
          if (textSU_Pass.Password.ToString().Trim() == "") MessageBox.Show("Mời bạn nhập mặt khẩu.");
        
            else if (modify.TK("SELECT *FROM USER WHERE USER_EMAIL ='" + textSU_UserName.Text + "'").Count != 0) 
            
                MessageBox.Show("Email này đã được đăng ký, vui lòng nhập Email khác.");
            
            else
            {
                string query = "INSERT INTO USER (USER_EMAIL,USER_PASS) VALUES('" + textSU_UserName.Text + "','" + textSU_Pass.Password.ToString() + "')";
                MySqlCommand command = new MySqlCommand(query, strCon);
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Đăng ký thành công.");

                }
            }

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login l1 = new Login();           
            l1.Show();
            this.Close();
        }
    }
}
