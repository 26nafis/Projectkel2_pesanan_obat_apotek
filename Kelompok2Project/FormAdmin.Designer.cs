namespace Kelompok2Project
{
    partial class FormAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnKelolaObat = new System.Windows.Forms.Button();
            this.btnTransaksi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKelolaObat
            // 
            this.btnKelolaObat.Location = new System.Drawing.Point(189, 168);
            this.btnKelolaObat.Name = "btnKelolaObat";
            this.btnKelolaObat.Size = new System.Drawing.Size(99, 29);
            this.btnKelolaObat.TabIndex = 0;
            this.btnKelolaObat.Text = "KelolaObat";
            this.btnKelolaObat.UseVisualStyleBackColor = true;
            this.btnKelolaObat.Click += new System.EventHandler(this.btnKelolaObat_Click_1);
            // 
            // btnTransaksi
            // 
            this.btnTransaksi.Location = new System.Drawing.Point(420, 168);
            this.btnTransaksi.Name = "btnTransaksi";
            this.btnTransaksi.Size = new System.Drawing.Size(99, 29);
            this.btnTransaksi.TabIndex = 2;
            this.btnTransaksi.Text = "Transaksi";
            this.btnTransaksi.UseVisualStyleBackColor = true;
            this.btnTransaksi.Click += new System.EventHandler(this.btnTransaksi_Click_1);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTransaksi);
            this.Controls.Add(this.btnKelolaObat);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKelolaObat;
        private System.Windows.Forms.Button btnTransaksi;
    }
}