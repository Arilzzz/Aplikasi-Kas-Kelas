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
using kas_kelas__2_.Config;
using kas_kelas__2_.Models;
using kas_kelas__2_.Controllers;
using kas_kelas__2_.Helpers;

namespace kas_kelas__2_
{
    public partial class UCTransaction : UserControl
    {
        Database db = new Database();
        TransactionController tr = new TransactionController();

        int paymentId = 0;

        // UI controls for week filtering
        private ComboBox cbMonthFilter;
        private ComboBox cbYearFilter;
        private ComboBox cbWeekFilter;
        private Label lblWeeklyTotal;

        public UCTransaction()
        {
            InitializeComponent();
            // CEK ROLE SAAT INIT
            string role = SessionManager.GetRole();

            if (role == "siswa")
            {
                // Untuk siswa: hanya tampilkan filter + data mereka
                SetupStudentView();
            }
            else
            {
                // Untuk admin: tampilkan semua fitur
                SetupAdminView();
            }
        }

        private void SetupStudentView()
        {
            // ADDED: Setup untuk view siswa (read-only, hanya filter)
            totalSaldo();
            loadStudentPayments(); // Load hanya pembayaran siswa ini

            // Sembunyikan input form untuk tambah/edit/delete
            if (this.Controls.Find("btnSubmit", true).Length > 0)
                this.Controls.Find("btnSubmit", true)[0].Visible = false;
            if (this.Controls.Find("btnEdit", true).Length > 0)
                this.Controls.Find("btnEdit", true)[0].Visible = false;
            if (this.Controls.Find("btnDelete", true).Length > 0)
                this.Controls.Find("btnDelete", true)[0].Visible = false;
            if (this.Controls.Find("cbSiswa", true).Length > 0)
                this.Controls.Find("cbSiswa", true)[0].Visible = false;
            if (this.Controls.Find("txtJumlah", true).Length > 0)
                this.Controls.Find("txtJumlah", true)[0].Visible = false;

            // Tampilkan filter minggu saja
            this.VisibleChanged += UCTransaction_VisibleChanged;
            CreateWeekFilterControls();
            InitializeMonthYearWeekDefaults();
        }
        private void SetupAdminView()
        {
            // ADDED: Setup untuk view admin (full CRUD + filter)
            totalSaldo();
            loadStudent();
            loadPayment();
            this.VisibleChanged += UCTransaction_VisibleChanged;

            var tb = this.Controls.Find("txtSearch", true).FirstOrDefault() as TextBox;
            if (tb != null) tb.TextChanged += txtsearch_TextChanged;

            SetActionButtonsEnabled(false);
            CreateWeekFilterControls();
            InitializeMonthYearWeekDefaults();
        }
        private void loadStudentPayments()
        {
            // ADDED: Load pembayaran untuk siswa yang sedang login
            var student = SessionManager.GetStudent();
            if (student == null) return;

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                // Query pembayaran hanya untuk siswa ini
                string query = @"
                SELECT p.id, s.nama_siswa, s.nis, p.jumlah_pemasukkan, p.tanggal_pemasukkan
                FROM pembayaran_kas p
                JOIN data_students s ON p.data_student_id = s.id
                WHERE p.data_student_id = @studentId
                ORDER BY p.tanggal_pemasukkan DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", student.id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Setup grid dengan custom headers
                dgvPembayaran.AutoGenerateColumns = false;
                dgvPembayaran.Columns.Clear();

                dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colId",
                    DataPropertyName = "id",
                    Visible = false
                });

                dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colNamaSiswa",
                    HeaderText = "Nama Siswa",
                    DataPropertyName = "nama_siswa",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colNIS",
                    HeaderText = "NIS",
                    DataPropertyName = "nis",
                    Width = 100
                });

                dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colJumlah",
                    HeaderText = "Jumlah",
                    DataPropertyName = "jumlah_pemasukkan",
                    Width = 120
                });

                dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colTanggal",
                    HeaderText = "Tanggal Pembayaran",
                    DataPropertyName = "tanggal_pemasukkan",
                    Width = 150
                });

                dgvPembayaran.DataSource = dt;
                dgvPembayaran.ClearSelection();
            }
        }
        private void loadPayment()
        {
            string q = null;
            DataTable dt = getPayment(q);

            dgvPembayaran.DataSource = null;
            dgvPembayaran.Columns.Clear();
            dgvPembayaran.AutoGenerateColumns = false;  // CHANGED

            // Setup kolom secara manual dengan custom headers
            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                DataPropertyName = "id",
                Visible = false
            });

            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNamaSiswa",
                HeaderText = "Nama Siswa",  // CUSTOM HEADER
                DataPropertyName = "nama_siswa",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNIS",
                HeaderText = "NIS",  // CUSTOM HEADER
                DataPropertyName = "nis",
                Width = 100
            });

            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colJumlah",
                HeaderText = "Jumlah",  // CUSTOM HEADER
                DataPropertyName = "jumlah_pemasukkan",
                Width = 120
            });

            dgvPembayaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTanggal",
                HeaderText = "Tanggal Pembayaran",  // CUSTOM HEADER
                DataPropertyName = "tanggal_pemasukkan",
                Width = 150
            });

            dgvPembayaran.DataSource = dt;
            dgvPembayaran.ClearSelection();
            SetActionButtonsEnabled(false);
        }

        private void loadStudent()
        {
            try
            {
                var sc = new StudentController();
                DataTable dt = sc.GetAll();

                // ensure cbSiswa exists in the designer
                var cb = this.Controls.Find("cbSiswa", true).FirstOrDefault() as ComboBox;
                if (cb == null)
                {
                    // fallback to field if designer has it as a field
                    cb = this.cbSiswa;
                }

                if (cb == null)
                {
                    MessageBox.Show("ComboBox cbSiswa not found in UCTransaction.");
                    return;
                }

                cb.DataSource = dt;
                cb.DisplayMember = "nama_siswa";
                cb.ValueMember = "id";
                cb.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student list: " + ex.Message);
            }
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
        private void CreateWeekFilterControls()
        {
            // DIPINDAHKAN: Sekarang filter ditempatkan di atas form (lebih tinggi)
            // Anda bisa menyesuaikan posisi Y sesuai dengan garis kuning di desain Anda

            var baseCtrl = this.Controls.Find("dtTanggal", true).FirstOrDefault();
            var baseX = baseCtrl != null ? baseCtrl.Location.X : 8;
            var baseY = 95; // CHANGED: Ubah Y position untuk ditempatkan di garis kuning
                            // Jika garis kuning lebih atas, kurangi nilai ini. Jika lebih bawah, tambahkan.

            cbMonthFilter = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 110 };
            cbYearFilter = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 70 };
            cbWeekFilter = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200 };
            lblWeeklyTotal = new Label { AutoSize = true };

            // CHANGED: Posisi X dan Y diatur untuk garis kuning
            cbMonthFilter.Location = new Point(baseX, baseY);
            cbYearFilter.Location = new Point(baseX + 130, baseY);
            cbWeekFilter.Location = new Point(baseX + 220, baseY);
            lblWeeklyTotal.Location = new Point(baseX + 440, baseY + 4);

            cbMonthFilter.SelectedIndexChanged += Filter_SelectionChanged;
            cbYearFilter.SelectedIndexChanged += Filter_SelectionChanged;
            cbWeekFilter.SelectedIndexChanged += Filter_SelectionChanged;

            this.Controls.Add(cbMonthFilter);
            this.Controls.Add(cbYearFilter);
            this.Controls.Add(cbWeekFilter);
            this.Controls.Add(lblWeeklyTotal);
        }

        private void InitializeMonthYearWeekDefaults()
        {
            // months
            var months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                .Where(m => !string.IsNullOrEmpty(m))
                .Select((name, idx) => new { Name = name, Value = idx + 1 })
                .ToList();
            cbMonthFilter.DataSource = months;
            cbMonthFilter.DisplayMember = "Name";
            cbMonthFilter.ValueMember = "Value";

            // years (from 2020 to next year)
            int startYear = 2020;
            int endYear = DateTime.Now.Year + 1;
            var years = Enumerable.Range(startYear, endYear - startYear + 1).ToList();
            cbYearFilter.DataSource = years;

            // set defaults to selected date or today
            var selected = GetSelectedDateOrDefault();
            cbMonthFilter.SelectedValue = selected.Month;
            cbYearFilter.SelectedItem = selected.Year;

            PopulateWeeksFromSelection();
        }

        private void PopulateWeeksFromSelection()
        {
            if (cbMonthFilter.SelectedValue == null || cbYearFilter.SelectedItem == null) return;

            int month = Convert.ToInt32(cbMonthFilter.SelectedValue);
            int year = Convert.ToInt32(cbYearFilter.SelectedItem);

            var weeks = WeekHelper.GetWeeksInMonth(year, month);
            var weekItems = weeks.Select(w => new { Display = w.DisplayName, Value = w.WeekNumber }).ToList();

            cbWeekFilter.DataSource = weekItems;
            cbWeekFilter.DisplayMember = "Display";
            cbWeekFilter.ValueMember = "Value";

            // try to select week containing selected date
            var sDate = GetSelectedDateOrDefault();
            var currentWeek = WeekHelper.GetWeekInfo(sDate);
            if (currentWeek != null && currentWeek.StartDate.Month == month && currentWeek.StartDate.Year == year)
                cbWeekFilter.SelectedValue = currentWeek.WeekNumber;
            else if (weekItems.Count > 0)
                cbWeekFilter.SelectedIndex = 0;

            // update grid for selected week
            LoadPaymentsBySelectedWeek();
        }

        private void Filter_SelectionChanged(object sender, EventArgs e)
        {
            // if month/year changed, repopulate weeks
            if (sender == cbMonthFilter || sender == cbYearFilter)
            {
                PopulateWeeksFromSelection();
                return;
            }

            // otherwise, week changed -> reload
            if (sender == cbWeekFilter)
            {
                LoadPaymentsBySelectedWeek();
            }
        }

        private DataTable getPayment(string q = null)
        {
            return tr.GetPayments(q);
        }
        private void UCTransaction_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // Refresh semua data saat tab ditampilkan
                totalSaldo();  // Update saldo
                loadPayment();  // Refresh grid pembayaran
                loadStudent();  // Refresh student list
            }
        }

        private void loadPayment(string q = null)
        {
            DataTable dt = getPayment(q);

            dgvPembayaran.DataSource = null;
            dgvPembayaran.Columns.Clear();
            dgvPembayaran.AutoGenerateColumns = true;
            dgvPembayaran.DataSource = dt;

            // hide id column if present
            if (dgvPembayaran.Columns.Count > 0)
            {
                if (dgvPembayaran.Columns.Contains("id"))
                    dgvPembayaran.Columns["id"].Visible = false;
                else
                    dgvPembayaran.Columns[0].Visible = false;
            }

            // customize headers (friendly names)
            if (dgvPembayaran.Columns.Contains("nama_siswa"))
                dgvPembayaran.Columns["nama_siswa"].HeaderText = "Nama Siswa";
            if (dgvPembayaran.Columns.Contains("nis"))
                dgvPembayaran.Columns["nis"].HeaderText = "NIS";
            if (dgvPembayaran.Columns.Contains("jumlah_pemasukkan"))
                dgvPembayaran.Columns["jumlah_pemasukkan"].HeaderText = "Jumlah";
            if (dgvPembayaran.Columns.Contains("tanggal_pemasukkan"))
                dgvPembayaran.Columns["tanggal_pemasukkan"].HeaderText = "Tanggal";

            dgvPembayaran.ClearSelection();
            SetActionButtonsEnabled(false);
        }

        private void LoadPaymentsBySelectedWeek()
        {
            if (cbWeekFilter == null || cbMonthFilter == null || cbYearFilter == null) return;
            if (cbWeekFilter.SelectedValue == null) return;

            // safe parsing of SelectedValue / SelectedItem
            int weekNumber = 0;
            var wkVal = cbWeekFilter.SelectedValue;
            if (wkVal is int) weekNumber = (int)wkVal;
            else if (wkVal != null && int.TryParse(wkVal.ToString(), out var w)) weekNumber = w;

            int month = 0;
            var mVal = cbMonthFilter.SelectedValue ?? cbMonthFilter.SelectedItem;
            if (mVal is int) month = (int)mVal;
            else if (mVal != null && int.TryParse(mVal.ToString(), out var mm)) month = mm;

            int year = 0;
            var yVal = cbYearFilter.SelectedValue ?? cbYearFilter.SelectedItem;
            if (yVal is int) year = (int)yVal;
            else if (yVal != null && int.TryParse(yVal.ToString(), out var yy)) year = yy;

            var dt = tr.GetPaymentsByWeek(year, month, weekNumber);

            dgvPembayaran.DataSource = dt;

            if (dgvPembayaran.Columns.Count > 0)
            {
                if (dgvPembayaran.Columns.Contains("id"))
                    dgvPembayaran.Columns["id"].Visible = false;
                else
                    dgvPembayaran.Columns[0].Visible = false;
            }

            if (dgvPembayaran.Columns.Contains("nama_siswa"))
                dgvPembayaran.Columns["nama_siswa"].HeaderText = "Nama Siswa";
            if (dgvPembayaran.Columns.Contains("nis"))
                dgvPembayaran.Columns["nis"].HeaderText = "NIS";
            if (dgvPembayaran.Columns.Contains("jumlah_pemasukkan"))
                dgvPembayaran.Columns["jumlah_pemasukkan"].HeaderText = "Jumlah";
            if (dgvPembayaran.Columns.Contains("tanggal_pemasukkan"))
                dgvPembayaran.Columns["tanggal_pemasukkan"].HeaderText = "Tanggal";

            dgvPembayaran.ClearSelection();
            SetActionButtonsEnabled(false);

            decimal total = tr.GetWeeklyPaymentTotal(year, month, weekNumber);
            if (lblWeeklyTotal != null)
                lblWeeklyTotal.Text = $"Total minggu: {total:N0}";
        }

        private DateTime GetSelectedDateOrDefault()
        {

            var dtCtrl = this.Controls.Find("dtTanggal", true).FirstOrDefault() as DateTimePicker;
            return dtCtrl?.Value ?? DateTime.Now;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (cbSiswa.SelectedValue == null)
            {
                MessageBox.Show("Pilih siswa terlebih dahulu!");
                return;
            }

            if (!int.TryParse(txtJumlah.Text, out int jumlah))
            {
                MessageBox.Show("Jumlah tidak valid!");
                return;
            }

            int studentId = 0;

            if (cbSiswa.SelectedItem is DataRowView row)
            {
                studentId = Convert.ToInt32(row["id"]);
            }
            else
            {
                MessageBox.Show("Gagal ambil ID siswa!");
                return;
            }

            if (!tr.IsStudentExists(studentId))
            {
                MessageBox.Show("Student tidak ada di database!");
                return;
            }
            //MessageBox.Show("Student ID yang dikirim: " + studentId);

            PaymentModel p = new PaymentModel()
            {
                data_student_id = studentId,
                jumlah_pemasukkan = jumlah,
                tanggal_pemasukkan = GetSelectedDateOrDefault()
            };

            try
            {
                tr.insertPayment(p);

                MessageBox.Show("Data berhasil ditambahkan!");

                clearForm();
                loadPayment();
                PopulateWeeksFromSelection(); // refresh weeks/totals with current selection
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menambahkan data: " + ex.Message);
            }
        }

        private void dgvPembayaran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // ensure clicked row is valid
            var row = dgvPembayaran.Rows[e.RowIndex];
            if (row == null || row.Cells.Count < 3) return;

            // read values safely by column name if available, fallback to index
            try
            {
                paymentId = Convert.ToInt32(row.Cells["id"].Value ?? row.Cells[0].Value);
            }
            catch
            {
                paymentId = Convert.ToInt32(row.Cells[0].Value);
            }

            try
            {
                cbSiswa.Text = row.Cells["nama_siswa"].Value?.ToString() ?? row.Cells[1].Value?.ToString();
            }
            catch
            {
                cbSiswa.Text = row.Cells[1].Value?.ToString();
            }

            txtJumlah.Text = row.Cells["jumlah_pemasukkan"].Value?.ToString() ?? row.Cells[2].Value?.ToString();

            // enable edit/delete buttons now that a row is selected
            SetActionButtonsEnabled(true);
        }

        private void clearForm()
        {
            cbSiswa.SelectedIndex = -1;
            txtJumlah.Clear();
            paymentId = 0;

            SetActionButtonsEnabled(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (paymentId == 0)
            {
                MessageBox.Show("Pilih data yang ingin diupdate!");
                return;
            }

            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah tidak valid!");
                return;
            }

            if (cbSiswa.SelectedValue == null)
            {
                MessageBox.Show("Pilih siswa terlebih dahulu!");
                return;
            }

            int studentId = Convert.ToInt32(cbSiswa.SelectedValue);

            PaymentModel p = new PaymentModel()
            {
                id = paymentId,
                data_student_id = studentId,
                jumlah_pemasukkan = jumlah,
                tanggal_pemasukkan = GetSelectedDateOrDefault()
            };

            try
            {
                tr.UpdatePayment(p);
                MessageBox.Show("Data berhasil diupdate!");
                clearForm();
                loadPayment();
                PopulateWeeksFromSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat update: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (paymentId == 0)
            {
                MessageBox.Show("Pilih data yang ingin dihapus!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Yakin ingin menghapus data?",
                "Konfirmasi",
                MessageBoxButtons.YesNo
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    tr.DeletePayment(paymentId);
                    MessageBox.Show("Data berhasil dihapus!");
                    clearForm();
                    loadPayment();
                    PopulateWeeksFromSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat delete: " + ex.Message);
                }
            }
        }
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            var q = (sender as TextBox)?.Text?.Trim();
            if (string.IsNullOrWhiteSpace(q))
                loadPayment(null);
            else
                loadPayment(q);
        }

        private void SetActionButtonsEnabled(bool enabled)
        {
            // assumes you have buttons named btnEdit and btnDelete in the designer
            if (this.Controls.Find("btnEdit", true).FirstOrDefault() is Button btnE)
                btnE.Enabled = enabled;
            if (this.Controls.Find("btnDelete", true).FirstOrDefault() is Button btnD)
                btnD.Enabled = enabled;
        }

        private void lblSaldo_Click(object sender, EventArgs e)
        {

        }
    }
}
