using kas_kelas__2_.Config;
using kas_kelas__2_.Controllers;
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

namespace kas_kelas__2_
{
    public partial class UC_Student_List : UserControl
    {
        Database db = new Database();
        StudentController sc = new StudentController();
        int id;

        private DataTable tampilData()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM data_students ORDER BY nama_siswa ASC";  // CHANGED
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void SetupGridColumns()
        {
            dgvStudent.AutoGenerateColumns = false;
            dgvStudent.Columns.Clear();

            // Kolom Nama
            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                DataPropertyName = "id",
                Visible = false
            });

            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNamaSiswa",
                HeaderText = "Nama Siswa",  // CUSTOM HEADER
                DataPropertyName = "nama_siswa",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNIS",
                HeaderText = "NIS",  // CUSTOM HEADER
                DataPropertyName = "nis",
                Width = 120
            });
        }

        private void LoadData()
        {
            SetupGridColumns();  // ADDED
            dgvStudent.DataSource = sc.GetAll();
            dgvStudent.ClearSelection();
        }
        public UC_Student_List()
        {

            InitializeComponent();
            dgvStudent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudent.MultiSelect = false;
            dgvStudent.CellClick += dgvStudent_CellClick;

            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            id = Convert.ToInt32(dgvStudent.Rows[e.RowIndex].Cells[0].Value);
            txtNama.Text = dgvStudent.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtNis.Text = dgvStudent.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void UC_Student_List_Load(object sender, EventArgs e)
        {

        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvStudent.Rows[e.RowIndex];
            id = row.DataGridView.Columns.Contains("colId")
                ? Convert.ToInt32(row.Cells["colId"].Value)
                : Convert.ToInt32(row.Cells[0].Value);

            txtNama.Text = row.DataGridView.Columns.Contains("colNamaSiswa")
                ? row.Cells["colNamaSiswa"].Value?.ToString()
                : row.Cells[1].Value?.ToString();

            txtNis.Text = row.DataGridView.Columns.Contains("colNIS")
                ? row.Cells["colNIS"].Value?.ToString()
                : row.Cells[2].Value?.ToString();
        }
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            var model = new StudentsModel { nama_siswa = txtNama.Text?.Trim(), nis = txtNis.Text?.Trim() };
            sc.Insert(model);
            MessageBox.Show("Data siswa berhasil ditambahkan");
            LoadData();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var model = new StudentsModel { id = id, nama_siswa = txtNama.Text?.Trim(), nis = txtNis.Text?.Trim() };
            sc.Update(model);
            MessageBox.Show("Data siswa berhasil diupdate");
            LoadData();
            ClearForm();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            sc.Delete(id);
            MessageBox.Show("Data siswa berhasil dihapus");
            LoadData();
            ClearForm();
        }

        private void ClearForm()
        {
            id = 0;
            txtNama.Clear();
            txtNis.Clear();
            dgvStudent.ClearSelection();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            var q = (sender as TextBox)?.Text?.Trim();
            SetupGridColumns();  // ADDED
            if (string.IsNullOrEmpty(q))
            {
                dgvStudent.DataSource = sc.GetAll();
            }
            else
            {
                dgvStudent.DataSource = sc.Search(q);
            }

            dgvStudent.ClearSelection();
        }
    }
}
