using kas_kelas__2_.Config;
using kas_kelas__2_.Helpers;
using kas_kelas__2_.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kas_kelas__2_.Controllers
{
    public class TransactionController
    {
        private readonly Database db = new Database();

        public void insertPayment(PaymentModel p)
        {
            using (SqlConnection conn = db.GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                INSERT INTO pembayaran_kas 
                (data_student_id, jumlah_pemasukkan, tanggal_pemasukkan) 
                VALUES (@sid, @jumlah, @tgl)";
                cmd.Parameters.AddWithValue("@sid", p.data_student_id);
                cmd.Parameters.AddWithValue("@jumlah", p.jumlah_pemasukkan);
                cmd.Parameters.AddWithValue("@tgl", p.tanggal_pemasukkan);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePayment(PaymentModel p)
        {
            using (SqlConnection conn = db.GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                UPDATE pembayaran_kas
                SET 
                    data_student_id = @sid,
                    jumlah_pemasukkan = @jumlah,
                    tanggal_pemasukkan = @tgl
                WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", p.id);
                cmd.Parameters.AddWithValue("@sid", p.data_student_id);
                cmd.Parameters.AddWithValue("@jumlah", p.jumlah_pemasukkan);
                cmd.Parameters.AddWithValue("@tgl", p.tanggal_pemasukkan);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePayment(int id)
        {
            using (SqlConnection conn = db.GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM pembayaran_kas WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool IsStudentExists(int studentId)
        {
            using (SqlConnection conn = db.GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM data_students WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", studentId);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        // Return payment rows, optional search by student name or nis
        public DataTable GetPayments(string q = null)
        {
            using (SqlConnection conn = db.GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                var sql = @"
                SELECT p.id, s.nama_siswa, s.nis, p.jumlah_pemasukkan, p.tanggal_pemasukkan
                FROM pembayaran_kas p
                JOIN data_students s ON p.data_student_id = s.id";
                if (!string.IsNullOrWhiteSpace(q))
                {
                    sql += " WHERE s.nama_siswa LIKE @q OR s.nis LIKE @q";
                    cmd.Parameters.AddWithValue("@q", "%" + q + "%");
                }
                sql += " ORDER BY p.id DESC";

                cmd.CommandText = sql;
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Columns.Contains("data_student_id"))
                        dt.Columns.Remove("data_student_id");
                    return dt;
                }
            }
        }
        // Tambahkan method ini
        public DataTable GetPaymentsByWeek(int year, int month, int weekNumber)
        {
            var weeks = WeekHelper.GetWeeksInMonth(year, month);
            var selectedWeek = weeks.FirstOrDefault(w => w.WeekNumber == weekNumber);

            if (selectedWeek == null)
                return new DataTable();

            using (var conn = db.GetConnection())
            {
                string query = @"
                    SELECT p.id, p.data_student_id, s.nama_siswa, s.nis, 
                           p.jumlah_pemasukkan, p.tanggal_pemasukkan
                    FROM pembayaran_kas p
                    INNER JOIN data_students s ON p.data_student_id = s.id
                    WHERE p.tanggal_pemasukkan >= @startDate 
                      AND p.tanggal_pemasukkan <= @endDate 
                    ORDER BY p.tanggal_pemasukkan DESC";

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@startDate", selectedWeek.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", selectedWeek.EndDate);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public decimal GetWeeklyPaymentTotal(int year, int month, int weekNumber)
        {
            var weeks = WeekHelper.GetWeeksInMonth(year, month);
            var selectedWeek = weeks.FirstOrDefault(w => w.WeekNumber == weekNumber);

            if (selectedWeek == null)
                return 0;

            using (var conn = db.GetConnection())
            {
                string query = @"
                    SELECT ISNULL(SUM(jumlah_pemasukkan), 0) 
                    FROM pembayaran_kas 
                    WHERE tanggal_pemasukkan >= @startDate 
                      AND tanggal_pemasukkan <= @endDate";

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@startDate", selectedWeek.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", selectedWeek.EndDate);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
        }
    }
}
