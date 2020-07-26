using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using LiveCharts;
using LiveCharts.Wpf;

namespace ProjectUAS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLOCALDB.;Initial Catalog=DBGudang;Integrated Security=True;Pooling=False");
            DateTime dateTime = DateTime.UtcNow.Date;
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            totalBarang_txt.Content = SumBarang();
            totalItem_txt.Content = CountBarang();
            totalBarangKeluar_txt.Content = CountBarangKeluar();
            totalBarangMasuk_txt.Content = CountBarangMasuk();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<double>  (getCartMasuk())
                }
            };


            Labels = hariMasuk();
            Formatter = value => value.ToString("N");
            SeriesCollection2 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<double> ( getCartKeluar())
                }
            };


            Labels2 = hari();
            Formatter2 = value => value.ToString("N");

            DataContext = this;
        }
        public int SumBarang()
        {
            int count = 0;
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "Select sum(jumlah_barang) from Barang";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
                else
                {
                    return 0;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public int CountBarang()
        {
            int count = 0;
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "Select count(*) from Barang";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
                else
                {
                    return 0;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public int CountBarangMasuk()
        {
            int count = 0;
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "Select count(*) from BarangMasuk";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
                else
                {
                    return 0;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public int CountBarangKeluar()
        {
            int count = 0;
            try
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "Select count(*) from BarangKeluar";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
                else
                {
                    return 0;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels2 { get; set; }
        public Func<double, string> Formatter2 { get; set; }


        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void daftarBarangBtn_Click(object sender, RoutedEventArgs e)
        {
            new Barang().Show();
            this.Close();
        }

        private void homeBtn_Copy1_Click(object sender, RoutedEventArgs e)
        {
            new BarangMasuk().Show();
            this.Close();
        }

        private void barangKeluarBtn_Click(object sender, RoutedEventArgs e)
        {
            new BarangKeluar().Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
        private List<double> getCartMasuk()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                List<double> allValues = new List<double>();
                string query = "select sum(jumlah) from BarangMasuk where TanggalMasuk BETWEEN '" + dateTime.AddDays(-7).ToString("MM-dd-yyyy") + "' and '" + dateTime.ToString("MM-dd-yyyy") + "' GROUP BY TanggalMasuk";
                SqlCommand cmd = new SqlCommand(query, conn);
                var Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    allValues.Add(Convert.ToDouble(Reader[0]));
                }
                conn.Close();
                return allValues;
            }
            else
            {
                return null;
            }
        }
        private List<double> getCartKeluar()
        {
            conn.Open();
            List<double> allValues = new List<double>();
            string query = "select sum(jumlahBarang) from BarangKeluar where TanggalKeluar BETWEEN '" + dateTime.AddDays(-6).ToString("MM-dd-yyyy") + "' and '" + dateTime.ToString("MM-dd-yyyy") + "' GROUP BY TanggalKeluar";
            SqlCommand cmd = new SqlCommand(query, conn);
            var Reader = cmd.ExecuteReader();
            while (Reader.Read())
            {
                allValues.Add(Convert.ToDouble(Reader[0]));
            }
            conn.Close();
            return allValues;
        
        }

        private String[] hari()
        {
            string[] hari = new string[7];
            conn.Open();
            string query = "select tanggalKeluar from BarangKeluar where TanggalKeluar BETWEEN '" + dateTime.AddDays(-7).ToString("MM-dd-yyyy") + "' and '" + dateTime.ToString("MM-dd-yyyy") + "' GROUP BY TanggalKeluar";
            SqlCommand cmd = new SqlCommand(query, conn);
            var Reader = cmd.ExecuteReader();
            var i = 0;
            while (Reader.Read())
            {
                hari[i] = Reader[0].ToString().Substring(0,10);
                i++;
            }
            conn.Close();
            return hari;
        }
        private String[] hariMasuk()
        {
            string[] hari = new string[7];
            conn.Open();
            string query = "select tanggalMasuk from BarangMasuk where TanggalMasuk BETWEEN '" + dateTime.AddDays(-7).ToString("MM-dd-yyyy") + "' and '" + dateTime.ToString("MM-dd-yyyy") + "' GROUP BY TanggalMasuk";
            SqlCommand cmd = new SqlCommand(query, conn);
            var Reader = cmd.ExecuteReader();
            var i = 0;
            while (Reader.Read())
            {
                hari[i] = Reader[0].ToString().Substring(0,10);
                i++;
            }
            conn.Close();
            return hari;
        }
    }
}
