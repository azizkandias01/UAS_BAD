using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
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
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            totalBarang_txt.Content = SumBarang();
            totalItem_txt.Content = CountBarang();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<double> { 10, 50, 39, 50 ,30,40,20}
                }
            };


            Labels = new[] { "Senin", "Selasa", "Rabu", "Kamis", "Jum'at", "Sabtu", "Minggu" };
            Formatter = value => value.ToString("N");
            SeriesCollection2 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "",
                    Values = new ChartValues<double> { 12,34,23,12,12,42,12}
                }
            };


            Labels2 = new[] { "Senin", "Selasa", "Rabu", "Kamis", "Jum'at", "Sabtu", "Minggu" };
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
        }public int CountBarang()
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
            MessageBox.Show("Anda Telah Logout");
            new LoginWindow().Show();
            this.Close();
        }
    }
}
