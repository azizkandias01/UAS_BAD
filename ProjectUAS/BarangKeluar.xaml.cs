using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace ProjectUAS
{
    /// <summary>
    /// Interaction logic for BarangKeluar.xaml
    /// </summary>
    public partial class BarangKeluar : Window
    {
        SqlDataAdapter dadapter;
        DataSet dset;
        static string connstring = @"Data Source = (localdb)\MSSQLLOCALDB.; Initial Catalog = DBGudang; Integrated Security = True; Pooling = False";
        SqlConnection con = new SqlConnection(connstring);
        public BarangKeluar()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            fillTable();
            
        }
        public void fillTable()
        {
            dadapter = new SqlDataAdapter("select * from BarangKeluar", connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            dataBarangKeluar.ItemsSource = dset.Tables[0].AsDataView();
        }
        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void daftarBarangBtn_Click(object sender, RoutedEventArgs e)
        {
            new Barang().Show();
            this.Close();
        }

        private void barangMasukBtn_Click(object sender, RoutedEventArgs e)
        {
            new BarangMasuk().Show();
            this.Close();
        }

        private void barangKeluarBtn_Click(object sender, RoutedEventArgs e)
        {
            new BarangKeluar().Show();
            this.Close();
        }

        private void tambahBarangKeluarBtn_Click(object sender, RoutedEventArgs e)
        {
            new TambahBarangKeluar().Show();
            this.Close();
        }
    }
}
