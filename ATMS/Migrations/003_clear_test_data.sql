-- 003_clear_test_data.sql
-- TRUNCATE deletes all rows and CASCADE ensures linked rows in other tables are also deleted.
-- RESTART IDENTITY resets the auto-incrementing 'id' columns back to 1.

TRUNCATE TABLE fee_ledger, students_master, fee_heads RESTART IDENTITY CASCADE;