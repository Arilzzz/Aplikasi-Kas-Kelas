using kas_kelas__2_.Config;
using kas_kelas__2_.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kas_kelas__2_.Controllers
{
    public class StudentController
    {
        private readonly Database db = new Database();

        public DataTable GetAll()
        {
            using (var conn = db.GetConnection())
            using (var da = new SqlDataAdapter("SELECT id, nama_siswa, " +
                "nis FROM data_students ORDER BY nama_siswa ASC", conn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable Search(string q)
        {
            using (var conn = db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT id, nama_siswa, nis
                    FROM data_students
                    WHERE nama_siswa LIKE @q OR nis LIKE @q
                    ORDER BY nama_siswa ASC";
                cmd.Parameters.AddWithValue("@q", "%" + q + "%");
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public void Insert(StudentsModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            using (var conn = db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO data_students (nama_siswa, nis) VALUES (@nama, @nis)";
                cmd.Parameters.AddWithValue("@nama", model.nama_siswa ?? string.Empty);
                cmd.Parameters.AddWithValue("@nis", model.nis ?? string.Empty);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(StudentsModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            using (var conn = db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE data_students SET nama_siswa=@nama, nis=@nis WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", model.id);
                cmd.Parameters.AddWithValue("@nama", model.nama_siswa ?? string.Empty);
                cmd.Parameters.AddWithValue("@nis", model.nis ?? string.Empty);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM data_students WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
