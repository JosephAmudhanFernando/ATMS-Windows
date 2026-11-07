-- 002_add_student_details.sql
ALTER TABLE students_master
ADD COLUMN emis_no VARCHAR(50),
ADD COLUMN dob DATE,
ADD COLUMN gender VARCHAR(20),
ADD COLUMN aadhaar_no VARCHAR(20),
ADD COLUMN religion VARCHAR(50),
ADD COLUMN community VARCHAR(50),
ADD COLUMN mother_tongue VARCHAR(50),
ADD COLUMN father_name VARCHAR(100),
ADD COLUMN father_contact_no VARCHAR(20),
ADD COLUMN father_income DECIMAL(10, 2),
ADD COLUMN mother_name VARCHAR(100),
ADD COLUMN mother_contact_no VARCHAR(20),
ADD COLUMN mother_income DECIMAL(10, 2),
ADD COLUMN address TEXT,
ADD COLUMN certificates TEXT;