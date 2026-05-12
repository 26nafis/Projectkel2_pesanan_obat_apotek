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
    public partial class FormCustomer : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=NAFIS\NAFISCOY;Initial Catalog=ApotekDB;Integrated Security=True");

        decimal hargaSatuan = 0;

        public FormCustomer()
        {
            InitializeComponent();
        }

        void TampilKatalog()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "SELECT id_obat, nama_obat, harga, stok, deskripsi FROM Obat WHERE stok > 0";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvKatalog.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat katalog: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'apotekDBDataSet.Transaksi' table. You can move, or remove it, as needed.
            this.transaksiTableAdapter.Fill(this.apotekDBDataSet.Transaksi);
            // TODO: This line of code loads data into the 'apotekDBDataSet.Obat' table. You can move, or remove it, as needed.
            this.obatTableAdapter.Fill(this.apotekDBDataSet.Obat);
            TampilKatalog();
            textBox1.Text = "1";

            if (string.IsNullOrEmpty(FormAdmin.IdAkunLogin))
            {
                MessageBox.Show("Peringatan: Sesi Login tidak terdeteksi. Silakan Login kembali.", "Sesi Habis");
            }
        }

        private void dgvKatalog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvKatalog.Rows[e.RowIndex];
                    txtNamaObat.Text = row.Cells["nama_obat"].Value.ToString();
                    hargaSatuan = Convert.ToDecimal(row.Cells["harga"].Value);
                    textBox1.Text = "1";
                    HitungTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memilih obat: " + ex.Message);
                }
            }
        }

        private void HitungTotal()
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(conn.ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT @harga * @jumlah";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        int jumlahBeli = 0;
                        if (!int.TryParse(textBox1.Text, out jumlahBeli)) return;

                        cmd.Parameters.AddWithValue("@harga", hargaSatuan);
                        cmd.Parameters.AddWithValue("@jumlah", jumlahBeli);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            decimal totalHarga = Convert.ToDecimal(result);
                            label1.Text = "Rp " + totalHarga.ToString("N0");
                            label1.ForeColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghitung total: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            HitungTotal();
        }

        private void btnPesan_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNamaObat.Text))
            {
                MessageBox.Show("Pilih obat dari tabel terlebih dahulu!");
                return;
            }

            int jumlahPesan;
            if (!int.TryParse(textBox1.Text, out jumlahPesan) || jumlahPesan <= 0)
            {
                MessageBox.Show("Masukkan jumlah obat yang valid!");
                return;
            }

            int stokTersedia = Convert.ToInt32(dgvKatalog.CurrentRow.Cells["stok"].Value);
            if (jumlahPesan > stokTersedia)
            {
                MessageBox.Show("Maaf, stok tidak mencukupi! Sisa stok: " + stokTersedia);
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string idObat = dgvKatalog.CurrentRow.Cells["id_obat"].Value.ToString();
                string cekSql = @"SELECT COUNT(*) FROM Transaksi 
                                  WHERE id_akun = @uid 
                                  AND id_obat = @oid 
                                  AND status_pesanan = 'Menunggu'";

                SqlCommand cekCmd = new SqlCommand(cekSql, conn);
                cekCmd.Parameters.AddWithValue("@uid", FormAdmin.IdAkunLogin);
                cekCmd.Parameters.AddWithValue("@oid", idObat);
                int sudahPesan = Convert.ToInt32(cekCmd.ExecuteScalar());

                if (sudahPesan > 0)
                {
                    MessageBox.Show("Kamu sudah memesan obat ini dan masih menunggu konfirmasi Admin!",
                                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = @"INSERT INTO Transaksi (id_akun, id_obat, jumlah_pesanan, total_harga, status_pesanan, tanggal) 
                               VALUES (@uid, @oid, @qty, @total, 'Menunggu', GETDATE())";

                decimal totalBayar = hargaSatuan * jumlahPesan;

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@uid", FormAdmin.IdAkunLogin);
                cmd.Parameters.AddWithValue("@oid", idObat);
                cmd.Parameters.AddWithValue("@qty", jumlahPesan);
                cmd.Parameters.AddWithValue("@total", totalBayar);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Pesanan berhasil dikirim ke Admin!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Setelah pesan berhasil, langsung ke Form Status & Riwayat
                Form_Customer__Status___Riwayat_ menu = new Form_Customer__Status___Riwayat_();
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan pesanan: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            HitungTotal();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormAdmin menu = new FormAdmin();
            menu.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_Customer__Status___Riwayat_ menu = new Form_Customer__Status___Riwayat_();
            menu.Show();
            this.Hide();
        }

        private void btntest_Click(object sender, EventArgs e)
        {

        }
    }
}