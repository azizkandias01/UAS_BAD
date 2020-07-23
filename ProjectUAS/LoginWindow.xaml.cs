using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectUAS
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        SqlDataAdapter dadapter;
        DataSet dset;
        static string connstring = @"Data Source = (localdb)\MSSQLLOCALDB.; Initial Catalog = DBGudang; Integrated Security = True; Pooling = False";
        SqlConnection con = new SqlConnection(connstring);
        string username = "";
        string password = "";
        public LoginWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            /*if (CekLogin())
            {
                new MainWindow().Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Bad Credential!!");
            }*/
            new MainWindow().Show();
            this.Close();
        }
        private Boolean CekLogin()
        {
            Boolean hasil = false;
            try
            {
                con.Open();
                string query = "select * from Akun WHERE username='" + usernameInput.Text + "' AND password = '" + passwordInput.Password + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                var Reader = cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        username = Reader.GetString(1);
                        password = Reader.GetString(2);
                    }
                    hasil = true;
                    return hasil;
                }
                else
                {
                    hasil = false;
                    return hasil;
                }
            }
            finally
            {
                con.Close();
               
            }
        }
    }
}
