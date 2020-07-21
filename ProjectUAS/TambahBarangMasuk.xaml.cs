using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for TambahBarangMasuk.xaml
    /// </summary>
    public partial class TambahBarangMasuk : Window
    {
        BarangMasuk barangMasuk = new BarangMasuk();
        SqlDataAdapter dadapter;
        DataSet dset;
        static string connstring = @"Data Source = (localdb)\MSSQLLOCALDB.; Initial Catalog = DBGudang; Integrated Security = True; Pooling = False";
        SqlConnection con = new SqlConnection(connstring);
        Boolean isSelected = false;
        public TambahBarangMasuk()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            dadapter = new SqlDataAdapter("select * from Barang", connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            dataBarangTambah.ItemsSource = dset.Tables[0].AsDataView();
        }
        private void refreshTable()
        {
            dadapter = new SqlDataAdapter("select * from Barang where nama_barang LIKE '%" + searchInputTambah.Text + "%'", connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            dataBarangTambah.ItemsSource = dset.Tables[0].AsDataView();
        }

        private void searchInputTambah_TextChanged(object sender, TextChangedEventArgs e)
        {
            refreshTable();
        }

        private void dataBarangTambah_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isSelected = true;
            try
            {
                DataRowView row = (DataRowView)dataBarangTambah.SelectedItems[0];
                string nama = row["nama_barang"].ToString();
                int id = Convert.ToInt32(row["id_barang"]);
                int jumlah = Convert.ToInt32(row["jumlah_barang"]);
                int harga = Convert.ToInt32(row["harga_barang"]);

                namaBarang.Text = nama;
                idBarang.Text = id.ToString();
                hargaBarang.Text = harga.ToString();



            }
            catch (Exception ex)
            {

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
                if (jumlahBarang.Text!=null&&supplierBarang.Text!=null&&tanggalBarang.SelectedDate!=null)
                {
                    if (MessageBox.Show("Yakin Tambahkan Data?", "Tambah Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {

                    }
                    else
                    {
                        tambahDataBarangMasuk();
                        updateJumlah();
                        MessageBox.Show("Berhasil Memasukan Data Barang!");

                        this.Close();
                        barangMasuk.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Ada field yang belum diisi!");
                }
                
            }
        }
        private void tambahDataBarangMasuk()
        {
            try
            {
                String tanggal = tanggalBarang.SelectedDate.Value.Date.ToShortDateString().ToString();
                string nama = namaBarang.Text;
                int jumlah = Convert.ToInt32(jumlahBarang.Text);
                int harga = Convert.ToInt32(hargaBarang.Text);
                int id = Convert.ToInt32(idBarang.Text);
                string supplier = supplierBarang.Text;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    string query = "INSERT INTO BarangMasuk values(@idbarang,@nama_barang,@jumlah_barang,@harga_barang,@supplier,@tanggal)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idbarang", id);
                    cmd.Parameters.AddWithValue("@nama_barang", nama);
                    cmd.Parameters.AddWithValue("@jumlah_barang", jumlah);
                    cmd.Parameters.AddWithValue("@harga_barang", harga);
                    cmd.Parameters.AddWithValue("@supplier", supplier);
                    cmd.Parameters.AddWithValue("@tanggal", tanggal);
                    cmd.ExecuteNonQuery();
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
        private void updateJumlah()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    int jumlah = Convert.ToInt32(jumlahBarang.Text);
                    int id = Convert.ToInt32(idBarang.Text);
                    con.Open();
                    string query = "UPDATE Barang SET jumlah_barang+=@jumlah_barang WHERE id_barang=@idbarang";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idbarang", id);
                    cmd.Parameters.AddWithValue("@jumlah_barang", jumlah);
                    cmd.ExecuteNonQuery();
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

        private void batalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Batalkan Tambah Data?", "Batalkan?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
               
            }
            else
            {
                barangMasuk.Show();
                this.Close();
            }
        }
        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            barangMasuk.Show();
        }
    }
}
