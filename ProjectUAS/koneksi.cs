using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectUAS
{
    class koneksi
    {
        private SqlDataAdapter dadapter;
        private DataSet dset;
        private static string connstring = @"Data Source = (localdb)\MSSQLLOCALDB.; Initial Catalog = DBGudang; Integrated Security = True; Pooling = False";
        static SqlConnection con = new SqlConnection(connstring);

        public static SqlConnection SqlConnection()
        {
           
            return con;
        }
        public DataSet fillTable(String query)
        {
            dadapter = new SqlDataAdapter(query, connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            return dset;
        }
        public void Delete(String table, String param1, int data1)
        {
            try
            {
                    string query = "DELETE FROM "+table+" WHERE "+param1+"="+data1;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        public void Update(String table, String param1, int data1, String param2, int data2)
        {
            try
            {
                string query = "UPDATE "+table+" SET "+param1+"+="+data1+" WHERE "+param2+" = "+data2;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        public void UpdateMasuk(String table, String param1, int data1, String param2, int data2)
        {
            try
            {
                string query = "UPDATE " + table + " SET " + param1 + "-=" + data1 + " WHERE " + param2 + " = " + data2;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
        public void update(string table)
        {

        }
        public void Update(String table, string param1, string data1, string param2, string data2, string param3, string data3, string param4, string data4)
        {
            string query = "UPDATE "+table+" SET "+param1+"='"+data1+"',"+param2+" = "+data2+", "+param3+"="+data3+" WHERE "+param4+" ="+data4;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        public void Update(String table, string param1, string data1, string param2, string data2,string data3)
        {
            string query = "UPDATE " + table + " SET " + param1 + "='" + data1 + "'," + param2 + " = " + data2 +" WHERE " + param1 + " ='" + data3+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }

        public void Insert(String table, string data1, string data2, string data3)
        {
            string query = "INSERT INTO "+table+" values('"+data1+"',"+data2+","+data3+")";
            MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        public void Insert(string table,int id, string nama_barang, int jumlah_barang, int harga_barang, string supplier, string tanggal)
        {
            string query = "INSERT INTO "+table+" values("+id+",'"+nama_barang+"',"+jumlah_barang+","+harga_barang+",'"+supplier+"','"+tanggal+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
    }
}
