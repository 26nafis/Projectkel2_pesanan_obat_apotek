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
           
    }
}