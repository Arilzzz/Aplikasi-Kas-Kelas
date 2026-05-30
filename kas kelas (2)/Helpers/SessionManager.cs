using kas_kelas__2_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kas_kelas__2_.Helpers
{
    public static class SessionManager
    {
        // Private static variables untuk menyimpan session
        private static UsersModel _currentAdmin = null;
        private static StudentsModel _currentStudent = null;

        // ADMIN SESSION
        public static void SetAdmin(UsersModel admin)
        {
            _currentAdmin = admin;
            _currentStudent = null; // Clear student session
        }

        public static UsersModel GetAdmin()
        {
            return _currentAdmin;
        }

        public static bool IsAdmin()
        {
            return _currentAdmin != null;
        }

        // STUDENT SESSION
        public static void SetStudent(StudentsModel student)
        {
            _currentStudent = student;
            _currentAdmin = null; // Clear admin session
        }

        public static StudentsModel GetStudent()
        {
            return _currentStudent;
        }

        public static bool IsStudent()
        {
            return _currentStudent != null;
        }

        // Clear session (untuk logout)
        public static void ClearSession()
        {
            _currentAdmin = null;
            _currentStudent = null;
        }

        // Get current role
        public static string GetRole()
        {
            if (_currentAdmin != null) return "admin";
            if (_currentStudent != null) return "siswa";
            return "guest";
        }
    }
}
