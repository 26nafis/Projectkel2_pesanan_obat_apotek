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

namespace kelompok2
{
    public partial class Form_Customer__Status___Riwayat_ : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=NAFIS\NAFISCOY;Initial Catalog=ApotekDB;Integrated Security=True");

        public Form_Customer__Status___Riwayat_()
        {
            InitializeComponent();
        }

        void TampilRiwayatPesanan()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = @"SELECT T.id_transaksi, O.nama_obat, T.jumlah_pesanan, 
                                 T.total_harga, T.status_pesanan, T.tanggal
                                 FROM Transaksi T
                                 JOIN Obat O ON T.id_obat = O.id_obat
                                 WHERE T.id_akun = @uid
                                 ORDER BY T.tanggal DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@uid", FormAdmin.IdAkunLogin);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRiwayat.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat riwayat: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Form_Customer__Status___Riwayat__Load(object sender, EventArgs e)
        {
            TampilRiwayatPesanan();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            TampilRiwayatPesanan();
            MessageBox.Show("Status pesanan telah diperbarui.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormCustomer menu = new FormCustomer();
            menu.Show();
            this.Hide();
        }
    }
}