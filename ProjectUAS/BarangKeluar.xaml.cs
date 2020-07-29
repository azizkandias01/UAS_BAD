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
        koneksi Data = new koneksi();
        SqlConnection con = koneksi.SqlConnection();
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
            dataBarangKeluar.ItemsSource = Data.fillTable("select * from BarangKeluar").Tables[0].AsDataView();
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
                    Data.Delete("BarangKeluar", "idBarangKeluar", idBarangKeluar);
                    updateJumlah();
                    fillTable();
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
            Data.Update("Barang", "jumlah_barang", jumlah, "id_barang", id);
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
            dataBarangKeluar.ItemsSource = Data.fillTable("select * from BarangKeluar where namaBarang LIKE '%" + searchInput.Text + "%'").Tables[0].AsDataView();
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
