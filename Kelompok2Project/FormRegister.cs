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
    public partial class FormRegister : Form
    {
        // 🔥 TAMBAHAN: koneksi database
        string connStr = "Data Source=NAFIS\\NAFISCOY;Initial Catalog=ApotektokoDB;Integrated Security=True";

        public FormRegister()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 🔥 TAMBAHAN: biar tidak dobel
            if (cmbRole.Items.Count == 0)
            {
                cmbRole.Items.Add("Customer");
                cmbRole.Items.Add("Admin");
            }
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            // 🔥 TAMBAHAN: isi combobox saat form load
            cmbRole.Items.Add("Customer");
            cmbRole.Items.Add("Admin");
        }

        // 🔥 TAMBAHAN: tombol daftar
        private void btnDaftar_Click(object sender, EventArgs e)
        {
            if (txtNama.Text == "" || txtEmail.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Semua field wajib diisi!");
                return;
            }

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_insert_akun", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nama", txtNama.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@role", cmbRole.Text);
            cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
            cmd.Parameters.AddWithValue("@no_hp", txtNoHp.Text);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Registrasi berhasil!");

            // kembali ke login
            FormLogin f = new FormLogin();
            f.Show();
            this.Close();
        }

        private void btnDaftar_Click_1(object sender, EventArgs e)
        {
            if (txtNama.Text == "" || txtEmail.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Semua field wajib diisi!");
                return;
            }

            // 🔥 VALIDASI NOMOR HP
            if (txtNoHp.Text == "")
            {
                MessageBox.Show("Nomor HP wajib diisi!");
                return;
            }

            // harus angka semua
            if (!txtNoHp.Text.All(char.IsDigit))
            {
                MessageBox.Show("Nomor HP harus berupa angka saja!");
                return;
            }

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_insert_akun", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nama", txtNama.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@role", cmbRole.Text);
            cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text);
            cmd.Parameters.AddWithValue("@no_hp", txtNoHp.Text);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Registrasi berhasil!");

            FormLogin f = new FormLogin();
            f.Show();
            this.Close();
        }
    }
}