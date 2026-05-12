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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kelompok2
{
    public partial class Form_admin : Form
    {
        // Ganti NAMA_PC dengan Server Name SQL Server kamu
        SqlConnection conn = new SqlConnection(@"Data Source=NAFIS\NAFISCOY;Initial Catalog=ApotekDB;Integrated Security=True");

        public Form_admin()
        {
            InitializeComponent();
        }

        // --- 1. Fungsi Menampilkan Data ke GridView ---
        void TampilDataObat()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Obat", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvObat.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // --- 2. Fungsi Reset Form ---
        void BersihkanForm()
        {
            txtNamaObat.Clear();
            txtHarga.Clear();
            txtStok.Clear();
            rtbDeskripsi.Clear();
        }

        private BindingSource BindingSource = new BindingSource();
        private DataTable dtobat = new DataTable();

        private void Form_admin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'apotekDBDataSet.Obat' table. You can move, or remove it, as needed.
            this.obatTableAdapter.Fill(this.apotekDBDataSet.Obat);
            TampilDataObat();
        }

        // --- 3. CREATE (Tambah Data) ---
        private void btntambah_Click_1(object sender, EventArgs e)
        {
            if (txtNamaObat.Text == "" || txtHarga.Text == "" || txtStok.Text == "")
            {
                MessageBox.Show("Data tidak boleh kosong!");
            }
            else
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO Obat (nama_obat, harga, stok, deskripsi) VALUES (@nama, @harga, @stok, @desc)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@nama", txtNamaObat.Text);
                    cmd.Parameters.AddWithValue("@harga", txtHarga.Text);
                    cmd.Parameters.AddWithValue("@stok", txtStok.Text);
                    cmd.Parameters.AddWithValue("@desc", rtbDeskripsi.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Obat Berhasil Disimpan!");
                    TampilDataObat();
                    BersihkanForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally { conn.Close(); }
            }
        }

        // --- 4. UPDATE (Edit Data) ---
        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                // Mengambil ID dari baris yang dipilih di GridView
                string id = dgvObat.CurrentRow.Cells["id_obat"].Value.ToString();
                string sql = "UPDATE Obat SET nama_obat=@nama, harga=@harga, stok=@stok, deskripsi=@desc WHERE id_obat=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nama", txtNamaObat.Text);
                cmd.Parameters.AddWithValue("@harga", txtHarga.Text);
                cmd.Parameters.AddWithValue("@stok", txtStok.Text);
                cmd.Parameters.AddWithValue("@desc", rtbDeskripsi.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Obat Berhasil Diupdate!");
                TampilDataObat();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { conn.Close(); }
        }

        // --- 5. DELETE (Hapus Data) ---
        private void btnhapus_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hapus obat ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string id = dgvObat.CurrentRow.Cells["id_obat"].Value.ToString();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Obat WHERE id_obat='" + id + "'", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Dihapus");
                    TampilDataObat();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                finally { conn.Close(); }
            }
        }

        // --- 6. Mengambil Data dari Grid ke TextBox saat baris diklik ---
        private void dgvObat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvObat.Rows[e.RowIndex];
                txtNamaObat.Text = row.Cells["nama_obat"].Value.ToString();
                txtHarga.Text = row.Cells["harga"].Value.ToString();
                txtStok.Text = row.Cells["stok"].Value.ToString();
                rtbDeskripsi.Text = row.Cells["deskripsi"].Value.ToString();
            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Tidak perlu diisi kecuali ada fungsi spesifik
        }

        private void btnTransaksi_Click(object sender, EventArgs e)
        {
            Konfirmasi_Pesanan ftran = new Konfirmasi_Pesanan();
            ftran.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAdmin menu = new FormAdmin();
            menu.Show();

            this.Hide();
        }
    }
}