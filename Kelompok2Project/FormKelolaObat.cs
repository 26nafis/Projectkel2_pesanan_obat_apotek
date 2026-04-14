using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

using System;

=using System;
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

       


        

        
    }
}