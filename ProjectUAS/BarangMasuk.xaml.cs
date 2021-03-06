﻿using System;
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
    /// Interaction logic for BarangMasuk.xaml
    /// </summary>
    public partial class BarangMasuk : Window
    {
        SqlConnection con = koneksi.SqlConnection();
        Boolean isSelected = false;
        int id = 0;
        int idBarangMasuk = 0;
        int jumlah = 0;
        koneksi Data = new koneksi();
        public BarangMasuk()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            refreshTableBarangMasuk();
        }

        private void barangMasukBtn_Click(object sender, RoutedEventArgs e)
        {
            TambahBarangMasuk barangMasuk = new TambahBarangMasuk();
            barangMasuk.Show();
            this.Close();
        }

        private void dataBarangMasuk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                isSelected = true;
                DataRowView row = (DataRowView)dataBarangMasuk.SelectedItems[0];
                id = Convert.ToInt32(row["idBarang"]);
                idBarangMasuk = Convert.ToInt32(row["idBarangMasuk"]);
                jumlah = Convert.ToInt32(row["jumlah"]);
            }catch(Exception ex)
            {

            }

        }
        public void refreshTableBarangMasuk()
        {
            dataBarangMasuk.ItemsSource = Data.fillTable("select * from BarangMasuk").Tables[0].AsDataView();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected)
            {
                              
                EditBarangMasuk editBarangMasuk = new EditBarangMasuk(id);
                editBarangMasuk.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Pilih Dulu Transaksi yang ingin diedit!");
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected)
            {
                if (MessageBox.Show("Yakin Menghapus Data?", "Hapus Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    con.Open();
                    Data.Delete("BarangMasuk","idBarangMasuk",idBarangMasuk);
                    refreshTableBarangMasuk();
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
            Data.UpdateMasuk("Barang", "jumlah_barang", jumlah, "id_barang", id);
        }

        private void daftarBarangBtn_Click(object sender, RoutedEventArgs e)
        {
            Barang barang = new Barang();
            barang.Show();
            this.Close();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void searchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataBarangMasuk.ItemsSource = Data.fillTable("select * from BarangMasuk where namaBarang LIKE '%" + searchInput.Text + "%'").Tables[0].AsDataView();
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
