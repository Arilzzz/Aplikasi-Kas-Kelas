using kas_kelas__2_.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace kas_kelas__2_
{
    public partial class dashboard : Form
    {
        string role = "";

        public dashboard()
        {
            InitializeComponent();
            cekRole();
        }

        private void cekRole()
        {
            // AMBIL ROLE DARI SESSION MANAGER
            role = SessionManager.GetRole();

            // JIKA SISWA
            if (role == "siswa")
            {
                btnTransaction.Visible = false;
                btnBudgets.Visible = false;
                btnStudent.Visible = false;
                pictureBox5.Visible = false;
                pictureBox4.Visible = false;
                pictureBox3.Visible = false;

                // Tampilkan info siswa yang login
                DisplayStudentInfo();
            }
            // JIKA ADMIN
            else if (role == "admin")
            {
                btnTransaction.Visible = true;
                btnBudgets.Visible = true;
                btnStudent.Visible = true;
            }
        }

        private void DisplayStudentInfo()
        {
            // ADDED: Tampilkan nama siswa yang login
            var student = SessionManager.GetStudent();
            if (student != null)
            {
                // Jika ada label untuk menampilkan nama siswa, set di sini
                // contoh: lblStudentName.Text = student.nama_siswa;
            }
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            panelIsi.Controls.Clear();

            var UCdasboard = new UC_Dashboard();
            UCdasboard.Dock = DockStyle.Fill;

            panelIsi.Controls.Add(UCdasboard);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            panelIsi.Controls.Clear();

            var UCdasboard = new UC_Dashboard();
            UCdasboard.Dock = DockStyle.Fill;

            panelIsi.Controls.Add(UCdasboard);
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            panelIsi.Controls.Clear();

            var UCTransaction = new UCTransaction();
            UCTransaction.Dock = DockStyle.Fill;

            panelIsi.Controls.Add(UCTransaction);
        }

        private void btnBudgets_Click(object sender, EventArgs e)
        {
            panelIsi.Controls.Clear();

            var UCBudgets = new UC_Budget();
            UCBudgets.Dock = DockStyle.Fill;

            panelIsi.Controls.Add(UCBudgets);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelIsi.Controls.Clear();

            var UCStudentList = new UC_Student_List();
            UCStudentList.Dock = DockStyle.Fill;

            panelIsi.Controls.Add(UCStudentList);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.ClearSession();
            this.Hide();

            // Kembali ke login sesuai role
            if (role == "siswa")
            {
                LoginStudent loginForm = new LoginStudent();
                loginForm.Show();
            }
            else if (role == "admin")
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
            }

            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}