using kas_kelas__2_.Config;
using kas_kelas__2_.Helpers;
using kas_kelas__2_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kas_kelas__2_
{
    public partial class UC_Dashboard : UserControl
    {
        Database db = new Database();
        public UC_Dashboard()
        {
            InitializeComponent();
            // CEK ROLE DAN LOAD DATA SESUAI ROLE
            string role = SessionManager.GetRole();

            if (role == "siswa")
            {
                // Untuk siswa: hanya tampilkan data mereka
                LoadStudentDashboard();
            }
            else
            {
                // Untuk admin: tampilkan semua statistik
                LoadAdminDashboard();
            }
        }
        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            recentTransaction();
            recentExpenditure();
        }
        private void LoadAdminDashboard()
        {
            totalSaldo();
            totalSiswa();
            totalPending();
            totalDebt();
            recentTransaction();
            recentExpenditure();
            DisplayAdminGreeting();
        }

        private void totalSaldo()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string pemasukan = "SELECT ISNULL(SUM(jumlah_pemasukkan),0) FROM pembayaran_kas";
                string pengeluaran = "SELECT ISNULL(SUM(jumlah_pengeluaran),0) FROM pengeluaran_kas";

                SqlCommand cmdMasuk = new SqlCommand(pemasukan, conn);
                SqlCommand cmdKeluar = new SqlCommand(pengeluaran, conn);

                int totalMasuk = Convert.ToInt32(cmdMasuk.ExecuteScalar());
                int totalKeluar = Convert.ToInt32(cmdKeluar.ExecuteScalar());

                int saldo = totalMasuk - totalKeluar;

                lblSaldo.Text = "Rp. " + saldo.ToString("N0");
            }
        }

        private void totalSiswa()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();


                string query = "SELECT COUNT(*) FROM data_students";

                SqlCommand cmd = new SqlCommand(query, conn);

                int total = (int)cmd.ExecuteScalar();

                lblSiswa.Text = total + " SISWA";
            }
        }

        private void totalPending()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM pembayaran_kas";
                SqlCommand cmd = new SqlCommand(query, conn);
                int total = (int)cmd.ExecuteScalar();
                lblPending.Text = total + " RECORD";
            }
        }

        private void totalDebt()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string query = "SELECT ISNULL(SUM(jumlah_pengeluaran),0) FROM pengeluaran_kas";
                SqlCommand cmd = new SqlCommand(query, conn);
                int total = Convert.ToInt32(cmd.ExecuteScalar());
                lblDebt.Text = "Rp. " + total.ToString("N0");
            }
        }

        private void SetupRecentTransactionGrid()
        {
            dgvRecent.AutoGenerateColumns = false;
            dgvRecent.Columns.Clear();

            dgvRecent.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNamaSiswa",
                HeaderText = "Nama Siswa",  // CUSTOM HEADER
                DataPropertyName = "nama_siswa",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvRecent.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colJumlah",
                HeaderText = "Jumlah Pembayaran",  // CUSTOM HEADER
                DataPropertyName = "jumlah_pemasukkan",
                Width = 150
            });

            dgvRecent.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTanggal",
                HeaderText = "Tanggal",  // CUSTOM HEADER
                DataPropertyName = "tanggal_pemasukkan",
                Width = 130
            });
        }

        // DIPERBAIKI: Setup kolom dengan custom headers untuk Recent Expenditure
        private void SetupRecentExpenditureGrid()
        {
            dgvbudget.AutoGenerateColumns = false;
            dgvbudget.Columns.Clear();

            dgvbudget.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTanggal",
                HeaderText = "Tanggal",  // CUSTOM HEADER
                DataPropertyName = "tanggal_pengeluaran",
                Width = 120
            });

            dgvbudget.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colJumlah",
                HeaderText = "Jumlah Pengeluaran",  // CUSTOM HEADER
                DataPropertyName = "jumlah_pengeluaran",
                Width = 150
            });

            dgvbudget.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKeterangan",
                HeaderText = "Keterangan",  // CUSTOM HEADER
                DataPropertyName = "keterangan",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }
        private void recentTransaction()
        {

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    MessageBox.Show("Koneksi berhasil");

                    string query = @"SELECT TOP 5
                data_students.nama_siswa,
                pembayaran_kas.jumlah_pemasukkan,
                pembayaran_kas.tanggal_pemasukkan
                FROM pembayaran_kas
                JOIN data_students
                ON pembayaran_kas.data_student_id = data_students.id
                ORDER BY pembayaran_kas.id DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    MessageBox.Show("Jumlah data: " + dt.Rows.Count); // 🔥 INI KUNCI

                    SetupRecentTransactionGrid();
                    dgvRecent.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void recentExpenditure()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT TOP 5
                    tanggal_pengeluaran,
                    jumlah_pengeluaran,
                    keterangan
                    FROM pengeluaran_kas
                    ORDER BY id DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    SetupRecentExpenditureGrid();
                    dgvbudget.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LoadStudentDashboard()
        {
            // ADDED: Load dashboard untuk siswa
            var student = SessionManager.GetStudent();
            if (student == null) return;

            // Tampilkan nama siswa
            DisplayStudentGreeting(student);

            // Hitung statistik siswa ini saja
            GetStudentBalance(student.id);
            GetStudentPaymentCount(student.id);
            recentStudentTransaction();
        }

        private void DisplayStudentGreeting(StudentsModel student)
        {
            lblAdminSiswa.Text = $"Selamat datang, {student.nama_siswa}!";
        }
        private void DisplayAdminGreeting()
        {
            UsersModel users = new UsersModel();
            lblAdminSiswa.Text = $"Selamat datang, {users.username}!";
        }

        private void GetStudentBalance(int studentId)
        {
            // ADDED: Hitung saldo untuk siswa ini (total pembayaran)
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT ISNULL(SUM(jumlah_pemasukkan), 0) 
                    FROM pembayaran_kas 
                    WHERE data_student_id = @studentId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);

                decimal balance = Convert.ToDecimal(cmd.ExecuteScalar());

                if (lblSaldo != null)
                    lblSaldo.Text = "Rp. " + balance.ToString("N0");
            }
        }

        private void GetStudentPaymentCount(int studentId)
        {
            // ADDED: Hitung jumlah pembayaran untuk siswa ini
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT COUNT(*) 
                    FROM pembayaran_kas 
                    WHERE data_student_id = @studentId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (lblPending != null)
                    lblPending.Text = count + " PEMBAYARAN";
            }
        }

        private void recentStudentTransaction()
        {
            // ADDED: Tampilkan riwayat pembayaran siswa ini
            var student = SessionManager.GetStudent();
            if (student == null) return;

            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT TOP 10
                            jumlah_pemasukkan,
                            tanggal_pemasukkan
                        FROM pembayaran_kas
                        WHERE data_student_id = @studentId
                        ORDER BY tanggal_pemasukkan DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@studentId", student.id);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Setup grid
                    dgvRecent.AutoGenerateColumns = false;
                    dgvRecent.Columns.Clear();

                    dgvRecent.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "colJumlah",
                        HeaderText = "Jumlah",
                        DataPropertyName = "jumlah_pemasukkan",
                        Width = 150
                    });

                    dgvRecent.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "colTanggal",
                        HeaderText = "Tanggal Pembayaran",
                        DataPropertyName = "tanggal_pemasukkan",
                        Width = 200
                    });

                    dgvRecent.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
