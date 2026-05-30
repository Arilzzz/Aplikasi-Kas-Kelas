namespace kas_kelas__2_
{
    partial class dashboard
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btnStudent = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnBudgets = new System.Windows.Forms.Button();
            this.btnTransaction = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelIsi = new System.Windows.Forms.Panel();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelIsi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.panel1.Controls.Add(this.pictureBox5);
            this.panel1.Controls.Add(this.btnStudent);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.btnBudgets);
            this.panel1.Controls.Add(this.btnTransaction);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnDashboard);
            this.panel1.Location = new System.Drawing.Point(-5, -6);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 549);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::kas_kelas__2_.Properties.Resources.usd_circle;
            this.pictureBox5.Location = new System.Drawing.Point(8, 116);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(30, 22);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 9;
            this.pictureBox5.TabStop = false;
            // 
            // btnStudent
            // 
            this.btnStudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudent.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStudent.ForeColor = System.Drawing.Color.White;
            this.btnStudent.Location = new System.Drawing.Point(43, 205);
            this.btnStudent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStudent.Name = "btnStudent";
            this.btnStudent.Size = new System.Drawing.Size(105, 32);
            this.btnStudent.TabIndex = 8;
            this.btnStudent.Text = "DAFTAR SISWA";
            this.btnStudent.UseVisualStyleBackColor = false;
            this.btnStudent.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::kas_kelas__2_.Properties.Resources.users;
            this.pictureBox3.Location = new System.Drawing.Point(8, 215);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 22);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Red;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(22, 489);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(116, 41);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(153, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(630, 539);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::kas_kelas__2_.Properties.Resources.wallet;
            this.pictureBox4.Location = new System.Drawing.Point(9, 166);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(30, 22);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // btnBudgets
            // 
            this.btnBudgets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnBudgets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBudgets.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBudgets.ForeColor = System.Drawing.Color.White;
            this.btnBudgets.Location = new System.Drawing.Point(44, 161);
            this.btnBudgets.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBudgets.Name = "btnBudgets";
            this.btnBudgets.Size = new System.Drawing.Size(105, 32);
            this.btnBudgets.TabIndex = 5;
            this.btnBudgets.Text = "BUDGETS";
            this.btnBudgets.UseVisualStyleBackColor = false;
            this.btnBudgets.Click += new System.EventHandler(this.btnBudgets_Click);
            // 
            // btnTransaction
            // 
            this.btnTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransaction.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransaction.ForeColor = System.Drawing.Color.White;
            this.btnTransaction.Location = new System.Drawing.Point(44, 112);
            this.btnTransaction.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTransaction.Name = "btnTransaction";
            this.btnTransaction.Size = new System.Drawing.Size(105, 32);
            this.btnTransaction.TabIndex = 3;
            this.btnTransaction.Text = "TRANSAKSI";
            this.btnTransaction.UseVisualStyleBackColor = false;
            this.btnTransaction.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(49, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "KAS KELAS";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::kas_kelas__2_.Properties.Resources.ChatGPT_Image_Apr_15__2026__01_44_58_PM;
            this.pictureBox2.Location = new System.Drawing.Point(9, 8);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::kas_kelas__2_.Properties.Resources.apps__1_;
            this.pictureBox1.Location = new System.Drawing.Point(8, 67);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Cascadia Code", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(44, 63);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(105, 32);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "DASHBOARD";
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // panelIsi
            // 
            this.panelIsi.Controls.Add(this.pictureBox9);
            this.panelIsi.Controls.Add(this.txtSearch);
            this.panelIsi.Location = new System.Drawing.Point(158, 2);
            this.panelIsi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelIsi.Name = "panelIsi";
            this.panelIsi.Size = new System.Drawing.Size(788, 536);
            this.panelIsi.TabIndex = 1;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::kas_kelas__2_.Properties.Resources.search__1_;
            this.pictureBox9.Location = new System.Drawing.Point(180, 15);
            this.pictureBox9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(16, 17);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 41;
            this.pictureBox9.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(20, 15);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(151, 20);
            this.txtSearch.TabIndex = 40;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 531);
            this.Controls.Add(this.panelIsi);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "dashboard";
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.dashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelIsi.ResumeLayout(false);
            this.panelIsi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnTransaction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnBudgets;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelIsi;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnStudent;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.TextBox txtSearch;
    }
}