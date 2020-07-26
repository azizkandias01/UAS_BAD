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
    /// Interaction logic for EditBarangKeluar.xaml
    /// </summary>
    public partial class EditBarangKeluar : Window
    {
        
        SqlDataAdapter dadapter;
        DataSet dset;
        static string connstring = @"Data Source = (localdb)\MSSQLLOCALDB.; Initial Catalog = DBGudang; Integrated Security = True; Pooling = False";
        SqlConnection con = new SqlConnection(connstring);
        int idbarang = 0;
        int jumlahLama = 0;
        public EditBarangKeluar(int id)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            con.Open();
            InitializeComponent();
            string query = "select * from BarangKeluar WHERE idBarang=" + id;
            SqlCommand cmd = new SqlCommand(query, con);
            var Reader = cmd.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    idbarang = Reader.GetInt32(0);
                    idBarangEdit.Text = Reader.GetInt32(1).ToString();
                    namaBarangEdit.Text = Reader.GetString(2);
                    jumlahBarangEdit.Text = Reader.GetInt32(3).ToString();
                    jumlahLama = Reader.GetInt32(3);
                    hargaBarangEdit.Text = Reader.GetInt32(4).ToString();
                    supplierBarangEdit.Text = Reader.GetString(5).ToString();
                    tanggalBarangEdit.SelectedDate = Reader.GetDateTime(6);
                }
            }
            con.Close();
        }
        private void update()
        {
            con.Open();
            int jumlah = Convert.ToInt32(jumlahBarangEdit.Text);
            string supplier = supplierBarangEdit.Text;
            string tanggal = tanggalBarangEdit.SelectedDate.Value.Date.ToShortDateString().ToString();
            string query = "UPDATE BarangKeluar SET jumlahBarang=" + jumlah + ", customer='" + supplier + "', tanggalKeluar='" + tanggal + "'WHERE idBarangKeluar=" + idbarang;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            MessageBox.Show(query);
            con.Close();
        }
        private void updateJumlah()
        {
            con.Open();
            int jumlah = Convert.ToInt32(jumlahBarangEdit.Text);
            string supplier = supplierBarangEdit.Text;
            string tanggal = tanggalBarangEdit.SelectedDate.Value.Date.ToShortDateString().ToString();
            string query = "UPDATE Barang SET jumlah_barang-=" + jumlahLama + " WHERE id_barang=" + idBarangEdit.Text;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            MessageBox.Show(query);
            con.Close();
        }
        private void updateJumlahBaru()
        {
            con.Open();
            int jumlah = Convert.ToInt32(jumlahBarangEdit.Text);
            string query = "UPDATE Barang SET jumlah_barang+=" + jumlah + " WHERE id_barang=" + idBarangEdit.Text;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            MessageBox.Show(query);
            con.Close();
        }

        private void tambahBarangMasukBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Update Data?", "Update?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {

            }
            else
            {
                update();
                updateJumlah();
                updateJumlahBaru();
                new BarangMasuk().Show();
                this.Close();
            }
        }

        private void batalBtn_Click(object sender, RoutedEventArgs e)
        {
            new BarangKeluar().Show();
            this.Close();
        }
    }
}
