using kas_kelas__2_.Config;
using kas_kelas__2_.Controllers;
using kas_kelas__2_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kas_kelas__2_
{
    public partial class UC_Budget : UserControl
    {
        Database db = new Database();
        BudgetController bt = new BudgetController();

        int id = 0;

        byte[] imageData;

        public UC_Budget()
        {
            InitializeComponent();
            dgvPengeluaran.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPengeluaran.MultiSelect = false;
            dgvPengeluaran.CellClick += dgvPengeluaran_CellContentClick_1;

            // ADDED: Auto-load ketika UC visible
            this.VisibleChanged += UC_Budget_VisibleChanged;

            tampilData();
            clearForm();
        }

        private void UC_Budget_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                tampilData();
            }
        }

        private void SetupGridColumns()
        {
            dgvPengeluaran.AutoGenerateColumns = false;
            dgvPengeluaran.Columns.Clear();

            dgvPengeluaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                DataPropertyName = "id",
                Visible = false
            });

            dgvPengeluaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTanggal",
                HeaderText = "Tanggal",  // CUSTOM HEADER
                DataPropertyName = "tanggal_pengeluaran",
                Width = 120
            });

            dgvPengeluaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colJumlah",
                HeaderText = "Jumlah Pengeluaran",  // CUSTOM HEADER
                DataPropertyName = "jumlah_pengeluaran",
                Width = 150
            });

            dgvPengeluaran.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKeterangan",
                HeaderText = "Keterangan",  // CUSTOM HEADER
                DataPropertyName = "keterangan",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }
        private void tampilData()
        {
            SetupGridColumns();  // ADDED
            DataTable dt = bt.GetAll();
            dgvPengeluaran.DataSource = dt;
            dgvPengeluaran.ClearSelection();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah tidak valid! Masukkan angka > 0.");
                return;
            }

            var model = new BudgetModel
            {
                tanggal_pengeluaran = dtTanggal.Value,
                jumlah_pengeluaran = jumlah,
                keterangan = txtKeterangan.Text?.Trim(),
                bukti_foto = imageData
            };

            try
            {
                bt.insertBudget(model);
                MessageBox.Show("Pengeluaran berhasil ditambahkan");
                tampilData();
                clearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menambah pengeluaran: " + ex.Message);
            }
        }
        private void clearForm()
        {
            id = 0;
            dtTanggal.Value = DateTime.Now;
            txtJumlah.Clear();
            txtKeterangan.Clear();
            imageData = null;
            pbUpload.Image = null;
            dgvPengeluaran.ClearSelection();
        }

        private DateTime GetSelectedDateOrDefault()
        {

            var dtCtrl = this.Controls.Find("dtTanggal", true).FirstOrDefault() as DateTimePicker;
            return dtCtrl?.Value ?? DateTime.Now;
        }

        private void btnTambah_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah tidak valid! Masukkan angka > 0.");
                return;
            }

            var model = new BudgetModel
            {
                tanggal_pengeluaran = dtTanggal.Value,
                jumlah_pengeluaran = jumlah,
                keterangan = txtKeterangan.Text?.Trim(),
                bukti_foto = imageData
            };

            try
            {
                bt.insertBudget(model);
                MessageBox.Show("Pengeluaran berhasil ditambahkan");
                tampilData();
                clearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menambah pengeluaran: " + ex.Message);
            }
        }

        private void dgvPengeluaran_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            id = Convert.ToInt32(dgvPengeluaran.Rows[e.RowIndex].Cells[0].Value);
            dtTanggal.Value = Convert.ToDateTime(dgvPengeluaran.Rows[e.RowIndex].Cells[1].Value);
            txtJumlah.Text = dgvPengeluaran.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtKeterangan.Text = dgvPengeluaran.Rows[e.RowIndex].Cells[3].Value.ToString();

            imageData = bt.GetImage(id);
            if (imageData != null)
            {
                using (var ms = new MemoryStream(imageData))
                    pbUpload.Image = Image.FromStream(ms);
            }
            else
            {
                pbUpload.Image = null;
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Pilih data yang ingin diupdate!");
                return;
            }

            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah tidak valid! Masukkan angka > 0.");
                return;
            }

            var model = new BudgetModel
            {
                id = id,
                tanggal_pengeluaran = dtTanggal.Value,
                jumlah_pengeluaran = jumlah,
                keterangan = txtKeterangan.Text?.Trim(),
                bukti_foto = imageData
            };

            try
            {
                bt.UpdateBudget(model);
                MessageBox.Show("Data berhasil diupdate");
                tampilData();
                clearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat update: " + ex.Message);
            }
        }

        private void btnUpload_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    pbUpload.Image = Image.FromFile(op.FileName);
                    imageData = File.ReadAllBytes(op.FileName);
                }
            }
        }

        private void btnHapus_Click_1(object sender, EventArgs e)
        {
            if (id == 0)
            {
                MessageBox.Show("Pilih data yang ingin dihapus!");
                return;
            }

            var hasil = MessageBox.Show("Yakin ingin menghapus data?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (hasil != DialogResult.Yes) return;

            try
            {
                bt.DeleteBudget(id);
                MessageBox.Show("Data berhasil dihapus");
                tampilData();
                clearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menghapus data: " + ex.Message);
            }
        }

        private void dgvPengeluaran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPengeluaran.Rows[e.RowIndex];
            // safe lookups by column name with fallbacks
            id = row.DataGridView.Columns.Contains("id") ? Convert.ToInt32(row.Cells["id"].Value) : Convert.ToInt32(row.Cells[0].Value);
            dtTanggal.Value = row.DataGridView.Columns.Contains("tanggal_pengeluaran")
                ? Convert.ToDateTime(row.Cells["tanggal_pengeluaran"].Value)
                : Convert.ToDateTime(row.Cells[1].Value);
            txtJumlah.Text = row.DataGridView.Columns.Contains("jumlah_pengeluaran")
                ? row.Cells["jumlah_pengeluaran"].Value.ToString()
                : row.Cells[2].Value.ToString();
            txtKeterangan.Text = row.DataGridView.Columns.Contains("keterangan")
                ? row.Cells["keterangan"].Value?.ToString()
                : row.Cells[3].Value?.ToString();

            // load image via controller
            imageData = bt.GetImage(id);
            if (imageData != null)
            {
                using (var ms = new MemoryStream(imageData))
                    pbUpload.Image = Image.FromStream(ms);
            }
            else pbUpload.Image = null;
        }

    }
}
