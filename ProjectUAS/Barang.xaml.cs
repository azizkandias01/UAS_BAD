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
        SqlDataAdapter dadapter;
        DataSet dset;
        static string connstring = @"Data Source = (localdb)\MSSQLLOCALDB.; Initial Catalog = DBGudang; Integrated Security = True; Pooling = False";
        SqlConnection con = new SqlConnection(connstring);
        public Barang()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            dadapter = new SqlDataAdapter("select * from Barang", connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            dataBarang.ItemsSource = dset.Tables[0].AsDataView();
        }
        public void insert()
        {
            
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    string query = "INSERT INTO Barang values(@nama_barang,@jumlah_barang,@harga_barang)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nama_barang", namaInput.Text);
                    cmd.Parameters.AddWithValue("@jumlah_barang", jumlahInput.Text);
                    cmd.Parameters.AddWithValue("@harga_barang", hargaInput.Text);
                    cmd.ExecuteNonQuery();
                }
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
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    string query = "UPDATE BARANG SET nama_barang=@nama_barang,jumlah_barang = @jumlah_barang, harga_barang=@harga_barang WHERE id_barang = @id_barang";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nama_barang", namaInput.Text);
                    cmd.Parameters.AddWithValue("@jumlah_barang", jumlahInput.Text);
                    cmd.Parameters.AddWithValue("@harga_barang", hargaInput.Text);
                    cmd.Parameters.AddWithValue("@id_barang", idInput.Text);
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
        public void delete()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    string query = "DELETE FROM Barang WHERE id_barang=@id_barang";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_barang", idInput.Text);
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

        private void searchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            dadapter = new SqlDataAdapter("select * from Barang where nama_barang LIKE '%" + searchInput.Text + "%'", connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            dataBarang.ItemsSource = dset.Tables[0].AsDataView();
        }
        private void refreshTable()
        {
            dadapter = new SqlDataAdapter("select * from Barang where nama_barang LIKE '%" + searchInput.Text + "%'", connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            dataBarang.ItemsSource = dset.Tables[0].AsDataView();
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
                string nama = row["nama_barang"].ToString();
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
    }
}
