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

namespace Kelompok2Project
{
    public partial class FormTransaksi : Form
    {
        // 🔥 TAMBAHAN: koneksi & id user
        string connStr = "Data Source=NAFIS\\NAFISCOY;Initial Catalog=ApotektokoDB;Integrated Security=True";
        int idAkun;

        public FormTransaksi()
        {
            InitializeComponent();
        }

        // 🔥 TAMBAHAN: constructor dengan id user
        public FormTransaksi(int id)
        {
            InitializeComponent();
            idAkun = id;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormTransaksi_Load(object sender, EventArgs e)
        {

        }

        // 🔥 TAMBAHAN: tombol checkout
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_insert_transaksi", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_akun", idAkun);
            cmd.Parameters.AddWithValue("@total_harga", 0); // sementara
            cmd.Parameters.AddWithValue("@status", "Pending");

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Transaksi berhasil!");
        }

        
}