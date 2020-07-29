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
using System.Windows.Shapes;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data;
using GenericCodes.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using MaterialDesignColors.Recommended;

namespace ProjectUAS
{
    /// <summary>
    /// Interaction logic for Barang.xaml
    /// </summary>
    public partial class Barang : Window
    {
        public string nama { get; set; }
        public string harga { get; set; }
        public string jumlah { get; set; }
        koneksi Data = new koneksi();
        SqlConnection con = koneksi.SqlConnection();
        public Barang()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            dataBarang.ItemsSource = Data.fillTable("select * from Barang").Tables[0].AsDataView();
 
        }
        public void insert()
        {            
            try
            {
                    con.Open();
                    Data.Insert("Barang", namaInput.Text, jumlahInput.Text, hargaInput.Text);
                
            }catch(Exception ex)
            {
                MessageBox.Show("Error : "+ex.Message);
            }
            finally
            {
                refreshTable();
                con.Close();

            }

        }
        public void update()
        {
            try
            {
                con.Open();
                if (MessageBox.Show("Dengan mengupdate semua data pada barang masuk dan keluar akan terupdate?", "Update?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {

                }
                else
                {
                    Data.Update("Barang", "nama_barang", namaInput.Text, "jumlah_barang", jumlahInput.Text, "harga_barang", hargaInput.Text, "id_barang", idInput.Text);
                    Data.Update("BarangMasuk", "NamaBarang", namaInput.Text,"Harga",hargaInput.Text,nama);
                    Data.Update("BarangKeluar", "namaBarang", namaInput.Text,"hargaBarang",hargaInput.Text,nama);
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
        public void delete()
        {
            try
            {
                    con.Open();
                    Data.Delete("Barang", "id_barang", Convert.ToInt32(idInput.Text));
                
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

        private void searchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
           dataBarang.ItemsSource = Data.fillTable("select * from Barang where nama_barang LIKE '%" + searchInput.Text + "%'").Tables[0].AsDataView();
        }
        private void refreshTable()
        {
           dataBarang.ItemsSource =Data.fillTable("select * from Barang").Tables[0].AsDataView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ProjectUAS.DBGudangDataSet dBGudangDataSet = ((ProjectUAS.DBGudangDataSet)(this.FindResource("dBGudangDataSet")));
            // Load data into the table Barang. You can modify this code as needed.
            ProjectUAS.DBGudangDataSetTableAdapters.BarangTableAdapter dBGudangDataSetBarangTableAdapter = new ProjectUAS.DBGudangDataSetTableAdapters.BarangTableAdapter();
            dBGudangDataSetBarangTableAdapter.Fill(dBGudangDataSet.Barang);
            System.Windows.Data.CollectionViewSource barangViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("barangViewSource")));
            barangViewSource.View.MoveCurrentToFirst();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (namaInput.Text == "" || jumlahInput.Text == "" | hargaInput.Text == "")
            {
                MessageBox.Show("field ada yang kosong, harap isi!");

            }
            else
            {
                insert();
                MessageBox.Show("Insert data berhasil");
            }
        }

        private void dataBarang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dataBarang.SelectedItems[0];
                nama = row["nama_barang"].ToString();
                int id = Convert.ToInt32(row["id_barang"]);
                int jumlah = Convert.ToInt32(row["jumlah_barang"]);
                int harga = Convert.ToInt32(row["harga_barang"]);

                namaInput.Text = nama;
                jumlahInput.Text = jumlah.ToString();
                hargaInput.Text = harga.ToString();
                idInput.Text = id.ToString();
            }catch (Exception ex)
            {
               
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            update();
            MessageBox.Show("update data berhasil");
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            delete();
            MessageBox.Show("delete data berhasil");
        }

        private void BarangMasukBtn_Click(object sender, RoutedEventArgs e)
        {
            BarangMasuk barangMasuk = new BarangMasuk();
            barangMasuk.Show();
            this.Close();
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

        private void barangKeluarBtn_Click(object sender, RoutedEventArgs e)
        {
            new BarangKeluar().Show();
            this.Close();
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
