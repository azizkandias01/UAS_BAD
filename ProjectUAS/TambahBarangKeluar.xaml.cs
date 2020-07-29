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
    /// Interaction logic for TambahBarangKeluar.xaml
    /// </summary>
    public partial class TambahBarangKeluar : Window
    {
        BarangMasuk barangMasuk = new BarangMasuk();
        koneksi Data = new koneksi();
        SqlConnection con = koneksi.SqlConnection();
        Boolean isSelected = false;
        int jumlahLama = 0;
        public TambahBarangKeluar()
        {
            InitializeComponent();
            refreshTable();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        private void refreshTable()
        {
            dataBarangTambah.ItemsSource = Data.fillTable("select * from Barang where nama_barang LIKE '%" + searchInputTambah.Text + "%'").Tables[0].AsDataView();
        }

        private void dataBarangTambah_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isSelected = true;
            try
            {
                DataRowView row = (DataRowView)dataBarangTambah.SelectedItems[0];
                string nama = row["nama_barang"].ToString();
                int id = Convert.ToInt32(row["id_barang"]);
                jumlahLama = Convert.ToInt32(row["jumlah_barang"]);
                int harga = Convert.ToInt32(row["harga_barang"]);

                namaBarang.Text = nama;
                idBarang.Text = id.ToString();
                hargaBarang.Text = harga.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void searchInputTambah_TextChanged(object sender, TextChangedEventArgs e)
        {
            refreshTable();
        }
        private void tambahDataBarangMasuk()
        {
            try
            {
                    con.Open();
                    String tanggal = tanggalBarang.SelectedDate.Value.Date.ToShortDateString().ToString();
                    string nama = namaBarang.Text;
                    int harga = Convert.ToInt32(hargaBarang.Text);
                    int id = Convert.ToInt32(idBarang.Text);
                    string supplier = supplierBarang.Text;
                    int jumlah = Convert.ToInt32(jumlahBarang.Text);
                    Data.Insert("BarangKeluar", id, nama, jumlah, harga, supplier, tanggal);
                                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            finally
            {
                refreshTable();
                con.Close();

            }
        }
        private void updateJumlah()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    int jumlah = Convert.ToInt32(jumlahBarang.Text);
                    int id = Convert.ToInt32(idBarang.Text);
                    con.Open();
                    Data.UpdateMasuk("Barang", "jumlah_barang", jumlah, "id_barang", id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            finally
            {
                refreshTable();
                con.Close();
            }
        }

        private void tambahBarangMasukBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected == false)
            {
                MessageBox.Show("Anda Belum Memilih Item");
            }
            else
            {
                int jumlah = 0;
                jumlah = Convert.ToInt32(jumlahBarang.Text);
                if (jumlahBarang.Text != null && supplierBarang.Text != null && tanggalBarang.SelectedDate != null)
                {
                    if (MessageBox.Show("Yakin Tambahkan Data?", "Tambah Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {

                    }
                    else
                    {
                        
                        if (jumlah > jumlahLama)
                        {
                            MessageBox.Show("Barang yang dikeluarkan melebihi jumlah stok barang!");
                        }
                        else
                        {
                            tambahDataBarangMasuk();
                            updateJumlah();
                            MessageBox.Show("Berhasil Memasukan Data Barang!");

                            this.Close();
                            new BarangKeluar().Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ada field yang belum diisi!");
                }

            }
        }

        private void batalBtn_Click(object sender, RoutedEventArgs e)
        {
            new BarangKeluar().Show();
            this.Close();
        }
    }
}
