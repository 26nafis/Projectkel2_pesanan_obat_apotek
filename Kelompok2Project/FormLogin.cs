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
    public partial class FormLogin : Form
    {
        // 🔹 TAMBAHAN KONEKSI
        string connStr = "Data Source=NAFIS\\NAFISCOY;Initial Catalog=ApotektokoDB;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // 🔥 TAMBAHAN BUTTON LOGIN
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // validasi input
            if (txtEmail.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Harap isi email dan password!");
                return;
            }

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Akun WHERE email=@email AND password=@password",
                conn
            );

            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string role = dr["role"].ToString();
                int idAkun = Convert.ToInt32(dr["id_akun"]);

                MessageBox.Show("Login berhasil!");

                if (role == "Admin")
                {
                    FormAdmin admin = new FormAdmin();
                    admin.Show();
                }
                else
                {
                    FormCustomer customer = new FormCustomer(idAkun);
                    customer.Show();
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show("Email atau password salah!");
            }

            conn.Close();
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister f = new FormRegister();
            f.Show();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            {
                // validasi input
                if (txtEmail.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Harap isi email dan password!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT id_akun, role FROM Akun WHERE email=@email AND password=@password",
                        conn
                    );

                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        // 🔥 ambil data dari DB
                        string role = dr["role"].ToString().Trim();
                        int idAkun = Convert.ToInt32(dr["id_akun"]);

                        MessageBox.Show("Login berhasil!");

                        // 🔥 LOGIN 1 BUTTON (ADMIN & CUSTOMER)
                        if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            FormAdmin admin = new FormAdmin();
                            admin.Show();
                        }
                        else if (role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                        {
                            FormCustomer customer = new FormCustomer(idAkun);
                            customer.Show();
                        }
                        else
                        {
                            MessageBox.Show("Role tidak dikenali!");
                        }

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Email atau password salah!");
                    }
                }
            }
        }
    }
}