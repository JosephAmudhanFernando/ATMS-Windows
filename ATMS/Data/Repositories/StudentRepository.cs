using ATMS.Core.Models;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATMS.Data.Repositories
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<int> InsertAsync(Student student)
        {
            const string sql = @"
        INSERT INTO students_master (
            admission_no, first_name, last_name, standard, section, is_active,
            emis_no, dob, gender, aadhaar_no, religion, community, mother_tongue,
            father_name, father_contact_no, father_income, 
            mother_name, mother_contact_no, mother_income, address, certificates
        )
        VALUES (
            @AdmissionNo, @FirstName, @LastName, @Standard, @Section, @IsActive,
            @EmisNo, @Dob, @Gender, @AadhaarNo, @Religion, @Community, @MotherTongue,
            @FatherName, @FatherContactNo, @FatherIncome, 
            @MotherName, @MotherContactNo, @MotherIncome, @Address, @Certificates
        )
        RETURNING id;";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(sql, student);
        }

        public async Task<IEnumerable<Student>> GetAllActiveAsync()
        {
            const string sql = @"
                SELECT * FROM students_master 
                WHERE is_active = true 
                ORDER BY standard, section, first_name;";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<Student>(sql);
        }

        public async Task<Student?> GetByAdmissionNoAsync(string admissionNo)
        {
            const string sql = "SELECT * FROM students_master WHERE admission_no = @AdmissionNo LIMIT 1;";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Student>(sql, new { AdmissionNo = admissionNo });
        }

        public async Task<bool> SoftDeleteAsync(int studentId)
        {
            const string sql = "UPDATE students_master SET is_active = false WHERE id = @Id;";

            using var connection = new NpgsqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = studentId });
            return rowsAffected > 0;
        }
    }
}