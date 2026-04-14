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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }

        // 🔥 TAMBAHAN: buka form kelola obat
        private void btnKelolaObat_Click(object sender, EventArgs e)
        {
            FormObat f = new FormObat();
            f.Show();
        }

        

       

        

        

        
    }
}