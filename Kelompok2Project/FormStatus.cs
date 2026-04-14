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
    public partial class FormStatus : Form
    {
        // 🔥 TAMBAHAN: koneksi & id user
        string connStr = "Data Source=NAFIS\\NAFISCOY;Initial Catalog=ApotektokoDB;Integrated Security=True";
        int idAkun;

        public FormStatus()
        {
            InitializeComponent();
        }

        // 🔥 TAMBAHAN: constructor dengan id user
        public FormStatus(int id)
        {
            InitializeComponent();
            idAkun = id;
        }

        private void FormStatus_Load(object sender, EventArgs e)
        {
            LoadStatus();
        }

        // 🔥 TAMBAHAN: load data status pesanan
        private void LoadStatus()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    id_transaksi,
                    tanggal_transaksi,
                    total_harga,
                    status
                FROM Transaksi
                WHERE id_akun = @id_akun
                ORDER BY tanggal_transaksi DESC
            ", conn);

            cmd.Parameters.AddWithValue("@id_akun", idAkun);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvStatus.DataSource = dt;

            conn.Close();
        }

        // 🔥 TAMBAHAN: tombol refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStatus();
        }

       
}