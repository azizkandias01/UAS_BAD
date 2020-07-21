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
    /// Interaction logic for EditBarangMasuk.xaml
    /// </summary>
    public partial class EditBarangMasuk : Window
    {
        SqlDataAdapter dadapter;
        DataSet dset;
        static string connstring = @"Data Source = (localdb)\MSSQLLOCALDB.; Initial Catalog = DBGudang; Integrated Security = True; Pooling = False";
        SqlConnection con = new SqlConnection(connstring);
        public EditBarangMasuk(int id)
        {
            con.Open();
            InitializeComponent();
            string query = "select * from BarangMasuk WHERE idBarang="+id;
            SqlCommand cmd = new SqlCommand(query, con);
            var Reader = cmd.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                   idBarangEdit.Text = Reader.GetInt32(1).ToString();
                   namaBarangEdit.Text = Reader.GetString(2);
                   jumlahBarangEdit.Text = Reader.GetInt32(3).ToString();
                   hargaBarangEdit.Text = Reader.GetInt32(4).ToString();
                   supplierBarangEdit.Text = Reader.GetString(5).ToString();
                    tanggalBarangEdit.SelectedDate = Reader.GetDateTime(6);
                }
            }
        }
    }
}
