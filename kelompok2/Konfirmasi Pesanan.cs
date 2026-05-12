using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Tambahkan ini untuk akses SQL Server

namespace kelompok2
{
    public partial class Konfirmasi_Pesanan : Form
    {
        // Deklarasi koneksi (Sesuaikan NAMA_PC dengan server SQL kamu)
        SqlConnection conn = new SqlConnection(@"Data Source=NAFIS\NAFISCOY;Initial Catalog=ApotekDB;Integrated Security=True");

        public Konfirmasi_Pesanan()
        {
            InitializeComponent();
        }

        private void Konfirmasi_Pesanan_Load(object sender, EventArgs e)
        {
            // Memanggil data saat form pertama kali dimuat
            TampilTransaksi();
        }

        // --- 7. Fungsi Menampilkan Data Transaksi (Konfirmasi Pesanan) ---
        void TampilTransaksi()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Query menggabungkan tabel Transaksi, Akun, dan Obat agar informasinya lengkap
                string query = @"SELECT T.id_transaksi, A.nama AS Nama_Customer, O.nama_obat, 
                                 T.jumlah_pesanan, T.total_harga, T.status_pesanan 
                                 FROM Transaksi T 
                                 JOIN Akun A ON T.id_akun = A.id_akun 
                                 JOIN Obat O ON T.id_obat = O.id_obat 
                                 WHERE T.status_pesanan = 'Menunggu'";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Pastikan nama DataGridView di Designer adalah dgvTransaksiAdmin
                dgvTransaksiAdmin.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat transaksi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // Jika kode ini berada dalam TabControl di Form Admin
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Jika Tab yang dipilih adalah Tab Konfirmasi Pesanan (misal indeks 1)
            if (tabControl1.SelectedIndex == 1)
            {
                TampilTransaksi();
            }
        }

        // --- 8. Fungsi Konfirmasi Pesanan (Update Status) ---
        private void btnKonfirmasi_Click_1(object sender, EventArgs e)
        {
            // Cek apakah ada baris yang dipilih di DataGridView
            if (dgvTransaksiAdmin.CurrentRow != null)
            {
                try
                {
                    conn.Open();
                    // Ambil ID Transaksi dari kolom "id_transaksi" pada baris yang dipilih
                    string idTrans = dgvTransaksiAdmin.CurrentRow.Cells["id_transaksi"].Value.ToString();

                    // Update status menjadi Dikonfirmasi sesuai Use Case
                    string sql = "UPDATE Transaksi SET status_pesanan = 'Dikonfirmasi' WHERE id_transaksi = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", idTrans);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Pesanan Berhasil Dikonfirmasi!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TampilTransaksi(); // Refresh tabel setelah konfirmasi
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal konfirmasi: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Pilih pesanan yang akan dikonfirmasi terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dgvTransaksiAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_admin menu = new Form_admin();
            menu.Show();

            this.Hide();
        }

        
    }
}