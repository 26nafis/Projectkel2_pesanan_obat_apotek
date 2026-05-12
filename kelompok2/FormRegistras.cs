using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Tambahkan ini

namespace kelompok2
{
    public partial class FormRegistrasi : Form
    {
        // 1. Deklarasi Koneksi (Sesuaikan NAMA_PC kamu)
        SqlConnection conn = new SqlConnection(@"Data Source=NAFIS\NAFISCOY;Initial Catalog=ApotekDB;Integrated Security=True");

        public FormRegistrasi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validasi: Pastikan semua field terisi
            if (string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Nama, Email, dan Password wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // 2. Query Insert ke Tabel Akun
                // Kita set default role sebagai 'Customer'
                string query = @"INSERT INTO Akun (nama, email, password, alamat, tanggal_daftar, nomer_handphone, role) 
                                 VALUES (@nama, @email, @pass, @alamat, @tgl, @hp, 'Customer')";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@tgl", txtTanggal.Text); // Bisa pakai DateTimePicker jika ingin lebih rapi
                cmd.Parameters.AddWithValue("@hp", txtNoHp.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Registrasi Berhasil! Silakan Login.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Kembali ke Form Login (Form1)
                FormAdmin login = new FormAdmin();
                login.Show();
                this.Close(); // Menutup form registrasi
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registrasi Gagal: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void FormRegistrasi_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'apotekDBDataSet.Akun' table. You can move, or remove it, as needed.
            this.akunTableAdapter.Fill(this.apotekDBDataSet.Akun);
            // Set tanggal otomatis hari ini jika txtTanggal adalah TextBox biasa
            txtTanggal.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Kosongkan jika tidak dipakai
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAdmin menu = new FormAdmin();
            menu.Show();

            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
   
}