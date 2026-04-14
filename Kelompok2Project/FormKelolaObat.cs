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
    public partial class FormKelolaObat : Form
    {
        // 🔥 TAMBAHAN: koneksi database
        string connStr = "Data Source=NAFIS\\NAFISCOY;Initial Catalog=ApotektokoDB;Integrated Security=True";

        public FormKelolaObat()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormKelolaObat_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // 🔥 LOAD DATA KE DATAGRID
        private void LoadData()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Obat", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvObat.DataSource = dt;

            conn.Close();
        }

        // 🔥 TAMBAH DATA
        private void btnTambah_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_insert_obat", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nama_obat", txtNamaObat.Text);
            cmd.Parameters.AddWithValue("@kategori", txtKategori.Text);
            cmd.Parameters.AddWithValue("@harga", txtHarga.Text);
            cmd.Parameters.AddWithValue("@stok", txtStok.Text);
            cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Data berhasil ditambahkan!");
            LoadData();
        }

        // 🔥 UPDATE DATA
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_update_obat", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_obat", txtId.Text);
            cmd.Parameters.AddWithValue("@nama_obat", txtNamaObat.Text);
            cmd.Parameters.AddWithValue("@kategori", txtKategori.Text);
            cmd.Parameters.AddWithValue("@harga", txtHarga.Text);
            cmd.Parameters.AddWithValue("@stok", txtStok.Text);
            cmd.Parameters.AddWithValue("@deskripsi", txtDeskripsi.Text);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Data berhasil diupdate!");
            LoadData();
        }

        // 🔥 DELETE DATA
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Yakin ingin menghapus data?",
                "Konfirmasi",
                MessageBoxButtons.YesNo
            );

            if (result == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_delete_obat", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_obat", txtId.Text);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Data berhasil dihapus!");
                LoadData();
            }
        }

        // 🔥 KLIK DATAGRID → MASUK KE TEXTBOX
        private void dgvObat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvObat.Rows[e.RowIndex];

                txtId.Text = row.Cells["id_obat"].Value.ToString();
                txtNamaObat.Text = row.Cells["nama_obat"].Value.ToString();
                txtKategori.Text = row.Cells["kategori"].Value.ToString();
                txtHarga.Text = row.Cells["harga"].Value.ToString();
                txtStok.Text = row.Cells["stok"].Value.ToString();
                txtDeskripsi.Text = row.Cells["deskripsi"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtKembali_Click(object sender, EventArgs e)
        {
            FormAdmin customer = new FormAdmin();
            customer.Show();
        }
    }
}