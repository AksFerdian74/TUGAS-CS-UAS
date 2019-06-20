using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace App_Manajemen_Lapas
{
    /* ini class untuk menghubungkan ke database 
     */
    class clsDB
    {
        #region Class dan variabel

        private SqlConnection connection = new SqlConnection(@"Server = .\SQLEXPRESS;Database=TugasPCS-Lapas-db; Integrated Security = SSPI");
        private DataTable dtTable;
        private SqlDataAdapter dtAdapter;
        private SqlCommand command;
        private string query;

        #endregion


        // metod untuk membuat koneksi ke database
        public SqlConnection getConnection()
        {
            return connection;
        }

        // metod untuk membuka koneksi
        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // metod untuk menutup koneksi
        public void closeConnection()
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void InsialisasiTable()
        {
            dtTable = new DataTable();
            dtAdapter = new SqlDataAdapter();
            command = new SqlCommand();
        }

        public DataTable Login(int bagAdmin,string username,string password)
        {
            InsialisasiTable();
            
            if (bagAdmin == 0)
                query = string.Concat("SELECT * FROM tbMainAdmin WHERE Username =@usn AND Password =@pass");
            else
                query = string.Concat("SELECT * FROM tbAdmin WHERE Username =@usn AND Password =@pass");

            command.Parameters.Add("@usn", SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", SqlDbType.VarChar).Value = password;

            command.CommandText = query;
            command.Connection = getConnection();

            dtAdapter.SelectCommand = command;
            dtAdapter.Fill(dtTable);

            return dtTable;
        }

        public DataTable DataNarapidana()
        {
            InsialisasiTable();

            query = "SELECT ID_NAPI,JenisNAPI,NamaPLapas,TglMasukNAPI,JenisHukuman,LamaHukuman,TglHabisHukuman FROM " +
               "tbNAPI INNER JOIN tbPenghuniLapas ON tbNAPI.ID_PenghuniLapas = tbPenghuniLapas.ID_PenghuniLapas; ";
            command = new SqlCommand(query, getConnection());
            dtAdapter = new SqlDataAdapter(command);
            dtTable = new DataTable();
            dtAdapter.Fill(dtTable);

            return dtTable;
        }

        public DataTable DataPenghuniLapas(string data = "")
        {
            InsialisasiTable();
            if (data.ToLower() == "narataha")
                query = "SELECT * FROM tbPenghuniLapas where Status != 'Sipir' or Status = ''";
            else if (data.ToLower() == "narapidana")
                query = "SELECT * FROM tbPenghuniLapas where Status = 'Narapidana' or Status = ''";
            else if (data.ToLower() == "tahanan")
                query = "SELECT * FROM tbPenghuniLapas where Status = 'Tahanan' or Status = ''";
            else if (data.ToLower() == "sipir")
                query = "SELECT * FROM tbPenghuniLapas where Status = 'Sipir' or Status = ''";
            else if (data.ToLower() == "all")
                query = "SELECT * FROM tbPenghuniLapas";
            else
                query = "SELECT * FROM tbPenghuniLapas Where Status = ''";

            command = new SqlCommand(query, getConnection());
            dtAdapter = new SqlDataAdapter(command);
            dtTable = new DataTable();
            dtAdapter.Fill(dtTable);

            return dtTable;
        }

        public DataTable DataBarangSimpanan()
        {
            dtTable = new DataTable();
            query = "SELECT ID_Barang,NamaBarang,NamaPLapas,JenisNAPI,tglMasukBarang,jumlah,TglSuratTndTerima FROM tbPenghuniLapas" +
                    " INNER JOIN tbBarang ON tbPenghuniLapas.ID_PenghuniLapas = tbBarang.ID_PenghuniLapas" +
                    " INNER JOIN tbNAPI ON tbPenghuniLapas.ID_PenghuniLapas = tbNAPI.ID_PenghuniLapas; ";
            dtAdapter = new SqlDataAdapter(query, getConnection());

            dtAdapter.Fill(dtTable);

            return dtTable;
        }

        public DataTable DaftarUserAdmin()
        {
            dtTable = new DataTable();
            query = "select tbAdmin.ID_PenghuniLapas,ID_Admin,NamaPLapas,UserName,BagAdmin from tbAdmin " +
                    "INNER JOIN tbPenghuniLapas ON tbPenghuniLapas.ID_PenghuniLapas = tbAdmin.ID_PenghuniLapas ;";
            dtAdapter = new SqlDataAdapter(query, getConnection());

            dtAdapter.Fill(dtTable);

            return dtTable;
        }

        public string UserUtama()
        {
            query = "SELECT Password FROM tbMainAdmin";

            command = new SqlCommand(query, getConnection());
            openConnection();
            string data = command.ExecuteScalar().ToString();
            closeConnection();
            return data;
        }
    }
}
