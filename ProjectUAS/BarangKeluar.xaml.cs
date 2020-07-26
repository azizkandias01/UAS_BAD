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
        Boolean isSelected = false;
        int id = 0;
        int idBarangKeluar = 0;
        int jumlah = 0;
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

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected)
            {
                if (MessageBox.Show("Yakin Menghapus Data?", "Hapus Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    con.Open();
                    string query = "DELETE FROM BarangKeluar WHERE idBarangKeluar = @idBarangMasuk";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idbarangMasuk", idBarangKeluar);
                    cmd.ExecuteNonQuery();
                    fillTable();
                    updateJumlah();
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Anda Belum Memilih Transaksi Untuk Dihapus!");
            }

        }
        private void updateJumlah()
        {
            string query = "UPDATE Barang SET jumlah_barang+=@jumlah WHERE id_barang = @idBarang";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@idbarang", id);
            cmd.Parameters.AddWithValue("@jumlah", jumlah);
            cmd.ExecuteNonQuery();
            fillTable();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected)
            {

                new EditBarangKeluar(id).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Pilih Dulu Transaksi yang ingin diedit!");
            }
        }

        private void searchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            dadapter = new SqlDataAdapter("select * from BarangKeluar where namaBarang LIKE '%" + searchInput.Text + "%'", connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            dataBarangKeluar.ItemsSource = dset.Tables[0].AsDataView();
        }

        private void dataBarangKeluar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
                try
                {
                    isSelected = true;
                    DataRowView row = (DataRowView)dataBarangKeluar.SelectedItems[0];
                    id = Convert.ToInt32(row["idBarang"]);
                    idBarangKeluar = Convert.ToInt32(row["idBarangKeluar"]);
                    jumlah = Convert.ToInt32(row["jumlahBarang"]);
                }
                catch (Exception ex)
                {

                }            
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Yakin Akan Keluar?", "Keluar?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {

            }
            else
            {
                new LoginWindow().Show();
                this.Close();
            }
        }
    }
}
