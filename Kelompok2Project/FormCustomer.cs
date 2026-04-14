using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kelompok2Project
{
    public partial class FormCustomer : Form
    {
        // 🔥 TAMBAHAN: simpan id user
        int idAkun;

        public FormCustomer()
        {
            InitializeComponent();
        }

        // 🔥 TAMBAHAN: constructor dengan parameter
        public FormCustomer(int id)
        {
            InitializeComponent();
            idAkun = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            // 🔥 TAMBAHAN: tampilkan info user (opsional)
            // lblWelcome.Text = "Selamat datang user ID: " + idAkun;
        }

        // 🔥 TAMBAHAN: buka daftar obat
        private void btnLihatObat_Click(object sender, EventArgs e)
        {
            FormObat f = new FormObat();
            f.Show();
        }

        // 🔥 TAMBAHAN: buka riwayat transaksi
        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            FormTransaksi f = new FormTransaksi(idAkun);
            f.Show();
        }

        // 🔥 TAMBAHAN: logout
        private void btnLogout_Click(object sender, EventArgs e)
        {
            FormLogin f = new FormLogin();
            f.Show();
            this.Close();
        }
    }
}