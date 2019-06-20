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
    class clsCRUD
    {

        #region CRUD Class dan Variabel

        clsDB DBCSource = new clsDB();
        string query;
        DataTable dtTable;
        SqlDataAdapter dtAdapter;
        SqlDataReader dtReader;
        SqlCommand command;

        #endregion


        public void TambahNAPI(int idpenghunilapas, string jenisnapi, DateTime tglmasuk, string jenishukuman, string lamahukuman)
        {
            DateTime tglkeluar =  tglmasuk.AddDays(Convert.ToDouble(lamahukuman));
            
            query = string.Concat("INSERT INTO tbNAPI VALUES (@idpenghunilapas,@jenisnapi,@tglmasuk,@jenishukuman,@lamahukuman,@tglHabisHukuman)");

            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@idpenghunilapas", idpenghunilapas);
            command.Parameters.AddWithValue("@jenisnapi", jenisnapi);
            command.Parameters.AddWithValue("@tglmasuk", tglmasuk);
            command.Parameters.AddWithValue("@jenishukuman", jenishukuman);
            command.Parameters.AddWithValue("@lamahukuman", lamahukuman);
            command.Parameters.AddWithValue("@tglHabisHukuman", tglkeluar);
            ExQuery(command);
    
        }

        public void HapusNAPI(int IDNAPI)
        {
            query = string.Concat("DELETE FROM tbNAPI WHERE ID_NAPI=@ID;");
            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@ID", IDNAPI);

            ExQuery(command);
        }

        public void UbahNAPI(string gol,string jenisnapi,string lamahuk,int idNAP)
        {
            query = string.Concat("UPDATE tbNAPI SET JenisNAPI=@gol, JenisHukuman=@jhukuman, LamaHukuman=@lhukuman WHERE ID_NAPI=@idnapi;");
            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@gol", gol);
            command.Parameters.AddWithValue("@jhukuman", jenisnapi);
            command.Parameters.AddWithValue("@lhukuman", lamahuk);
            command.Parameters.AddWithValue("@idnapi", idNAP);

            ExQuery(command);
        }

        public void TambahBarang(int idPenghuniLapas, string NamaBarang, DateTime tglMasuk, int Jumlah, DateTime TglSuratMasuk)
        {
            query = String.Concat("INSERT INTO tbBarang VALUES(@idpenghuniLapas, @namabarang, @tglmasuk, @jumlah, @tglsuratmasuk)");
            
            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@idpenghunilapas", idPenghuniLapas);
            command.Parameters.AddWithValue("@namabarang", NamaBarang);
            command.Parameters.AddWithValue("@tglmasuk", tglMasuk);
            command.Parameters.AddWithValue("@jumlah", Jumlah);
            command.Parameters.AddWithValue("@tglsuratmasuk", TglSuratMasuk);
            ExQuery(command);
        }

        public void HapusSimpananBarang(int idBarang)
        {
            query = "DELETE FROM tbBarang WHERE ID_Barang = @ID";
            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@ID", idBarang);

            ExQuery(command);
        }

        public void TambahPenghuniLapas(string nama, DateTime tglahir, string tmptlahir, string jeniskelamin, 
            string agama, string pekerjaan, string pendidikan, string alamat, string keterangan)
        {
            query = string.Concat("INSERT INTO tbPenghuniLapas VALUES " +
                "(@nama,@tgllahir,@tmptlahir,@jenisK,@Agama,@Pekerjaan,@Pendidikan,@Alamat,'',@Ket)");

            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@nama", nama);
            command.Parameters.AddWithValue("@tgllahir", tglahir);
            command.Parameters.AddWithValue("@tmptlahir", tmptlahir);
            command.Parameters.AddWithValue("@jenisK", jeniskelamin);
            command.Parameters.AddWithValue("@Agama", agama);
            command.Parameters.AddWithValue("@Pekerjaan", pekerjaan);
            command.Parameters.AddWithValue("@Pendidikan", pendidikan);
            command.Parameters.AddWithValue("@Alamat", alamat);
            command.Parameters.AddWithValue("@Ket", keterangan);

            ExQuery(command);
        }

        public void TambahAdmin(int id_Peng, string username,string password,string bagian)
        {
           // if (bagian == "Admin Bag. Narapidana")
            //    query = "INSERT INTO tbMainAdmin VALUES (@ID,@UN,@PASS);";
            //else
                query = "INSERT INTO tbAdmin VALUES (@ID,@UN,@PASS,@BAG);";

            query = String.Concat(query,"UPDATE tbPenghuniLapas SET Status='Sipir' Where ID_PenghuniLapas = @ID;");

            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@ID", id_Peng);
            command.Parameters.AddWithValue("@UN", username);
            command.Parameters.AddWithValue("@PASS", password);
            command.Parameters.AddWithValue("@BAG", bagian);

            ExQuery(command);

        }

        public void HapusAdmin(int id_Adm,int id_PLapas)
        {
           // MessageBox.Show(id_PLapas.ToString());
            query = "DELETE FROM tbAdmin WHERE ID_Admin = @id";
            query = String.Concat(query," UPDATE tbPenghuniLapas SET Status='' Where ID_PenghuniLapas = @idl;");

            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@id", id_Adm);
            command.Parameters.AddWithValue("@idl", id_PLapas);

            ExQuery(command);
        }
        public void GantiPassword(string un, string newPass,string tb = "tbAdmin")
        {
            query = "UPDATE tbMainAdmin SET Password=@newpass WHERE Username=@username";

            command = new SqlCommand(query, DBCSource.getConnection());
            command.Parameters.AddWithValue("@newpass", newPass);
            command.Parameters.AddWithValue("@username", un);
           // command.Parameters.AddWithValue("@table", tb);

            ExQuery(command);
        }
        public void ExQuery(SqlCommand sqlc)
        {
            try
            {
                DBCSource.openConnection();
                sqlc.ExecuteNonQuery();
                DBCSource.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DBCSource.closeConnection();
            }
        }
    }
}
