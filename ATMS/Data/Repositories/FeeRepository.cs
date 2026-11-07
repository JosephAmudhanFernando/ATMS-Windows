using ATMS.Core.Models;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATMS.Data.Repositories
{
    public class FeeRepository
    {
        private readonly string _connectionString;

        public FeeRepository(string connectionString)
        {
            _connectionString = connectionString;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        // 1. Get the current active academic year (e.g., 2026-2027)
        public async Task<AcademicYear?> GetActiveAcademicYearAsync()
        {
            const string sql = "SELECT * FROM academic_years WHERE is_active = true LIMIT 1;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<AcademicYear>(sql);
        }

        // 2. Fetch all active fee categories (Tuition, Transport, etc.)
        public async Task<IEnumerable<FeeHead>> GetActiveFeeHeadsAsync()
        {
            const string sql = "SELECT * FROM fee_heads WHERE is_active = true ORDER BY id;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<FeeHead>(sql);
        }

        // 3. Fetch a specific student's fee ledger for the active year
        public async Task<IEnumerable<FeeLedger>> GetStudentLedgerAsync(int studentId, int academicYearId)
        {
            const string sql = @"
                SELECT 
                    fl.*, 
                    fh.head_name AS FeeHeadName 
                FROM fee_ledger fl
                INNER JOIN fee_heads fh ON fl.fee_head_id = fh.id
                WHERE fl.student_id = @StudentId 
                  AND fl.academic_year_id = @AcademicYearId
                  AND fl.is_cancelled = false
                ORDER BY fh.id;";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<FeeLedger>(sql, new { StudentId = studentId, AcademicYearId = academicYearId });
        }

        // 4. Allocate a fee to a student (e.g., assigning Term 1 Tuition)
        public async Task<int> AllocateFeeAsync(int studentId, int academicYearId, int feeHeadId, decimal amount)
        {
            const string sql = @"
                INSERT INTO fee_ledger (student_id, academic_year_id, fee_head_id, allocated_amount)
                VALUES (@StudentId, @AcademicYearId, @FeeHeadId, @Amount)
                RETURNING id;";

            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(sql, new
            {
                StudentId = studentId,
                AcademicYearId = academicYearId,
                FeeHeadId = feeHeadId,
                Amount = amount
            });
        }

        // Add this inside FeeRepository.cs
        public async Task<bool> ProcessPaymentAsync(
            int ledgerId, decimal amountPaid, string receiptNo)
        {
            const string sql = @"
        UPDATE fee_ledger 
        SET paid_amount = paid_amount + @AmountPaid, 
            receipt_no = @ReceiptNo 
        WHERE id = @LedgerId;";

            using var connection = new NpgsqlConnection(_connectionString);

            var rows = await connection.ExecuteAsync(sql, new
            {
                AmountPaid = amountPaid,
                ReceiptNo = receiptNo,
                LedgerId = ledgerId
            });

            return rows > 0;
        }
    }
}