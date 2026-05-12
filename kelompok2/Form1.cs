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
    // Nama class ini adalah FormAdmin (tapi fungsinya sebagai Form Login)
    public partial class FormAdmin : Form
    {
        // 1. Koneksi Database
        SqlConnection conn = new SqlConnection(@"Data Source=NAFIS\NAFISCOY;Initial Catalog=ApotekDB;Integrated Security=True");

        // VARIABEL PENTING: Untuk menyimpan ID yang sedang login agar bisa dipanggil di FormCustomer
        public static string IdAkunLogin;

        string query = "SELECT * FROM Akun WHERE email='" + txtEmail.Text + "' AND password='" + txtPassword.Text + "'";

        public FormAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // 2. Query untuk cek Login (Ambil id_akun juga!)
                string query = "SELECT id_akun, nama, role FROM Akun WHERE email=@email AND password=@pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // --- BAGIAN PALING PENTING ---
                    // Simpan id_akun dari database ke variabel static IdAkunLogin
                    IdAkunLogin = reader["id_akun"].ToString();

                    string role = reader["role"].ToString();
                    string namaUser = reader["nama"].ToString();

                    MessageBox.Show("Selamat Datang, " + namaUser, "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Logika Pemisahan Role
                    if (role == "Admin")
                    {
                        Form_admin fAdmin = new Form_admin();
                        fAdmin.Show();
                    }
                    else
                    {
                        // Jika role adalah Customer
                        FormCustomer fCust = new FormCustomer();
                        fCust.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email atau Password salah!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi Kesalahan: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            FormRegistrasi fReg = new FormRegistrasi();
            fReg.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}