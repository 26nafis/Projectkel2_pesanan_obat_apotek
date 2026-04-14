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
    public partial class FormObat : Form
    {
        // 🔥 TAMBAHAN: koneksi database
        string connStr = "Data Source=NAFIS\\NAFISCOY;Initial Catalog=ApotektokoDB;Integrated Security=True";

        int idAkun;

        public FormObat()
        {
            InitializeComponent();
        }

        // 🔥 TAMBAHAN: constructor dengan id user
        public FormObat(int id)
        {
            InitializeComponent();
            idAkun = id;
        }

        private void FormObat_Load(object sender, EventArgs e)
        {
            LoadObat();
        }

        // 🔥 LOAD DATA OBAT KE DATAGRID
        private void LoadObat()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Obat", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvObat.DataSource = dt;

            conn.Close();
        }

        // 🔥 KLIK DATAGRID → MASUK KE TEXTBOX
        private void dgvObat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvObat.Rows[e.RowIndex];

                txtId.Text = row.Cells["id_obat"].Value.ToString();
                txtNamaObat.Text = row.Cells["nama_obat"].Value.ToString();
                txtHarga.Text = row.Cells["harga"].Value.ToString();
            }
        }

        // 🔥 TAMBAH KE TRANSAKSI
        private void btnBeli_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtJumlah.Text == "")
            {
                MessageBox.Show("Pilih obat dan isi jumlah!");
                return;
            }

            FormTransaksi f = new FormTransaksi(idAkun);
            f.Show();
        }

        // 🔥 KEMBALI
        private void btnKembali_Click(object sender, EventArgs e)
        {
            FormCustomer f = new FormCustomer(idAkun);
            f.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}