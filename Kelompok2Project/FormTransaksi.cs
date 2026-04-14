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
    public partial class FormTransaksi : Form
    {
        // 🔥 TAMBAHAN: koneksi & id user
        string connStr = "Data Source=NAFIS\\NAFISCOY;Initial Catalog=ApotektokoDB;Integrated Security=True";
        int idAkun;

        public FormTransaksi()
        {
            InitializeComponent();
        }

        
}