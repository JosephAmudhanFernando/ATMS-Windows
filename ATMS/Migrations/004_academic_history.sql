-- 004_academic_history.sql

-- 1. Create the Academic Years table
CREATE TABLE IF NOT EXISTS academic_years (
    id SERIAL PRIMARY KEY,
    year_name VARCHAR(20) UNIQUE NOT NULL, -- e.g., '2026-2027'
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    is_active BOOLEAN DEFAULT FALSE
);

-- 2. Create the Enrollment mapping table
CREATE TABLE IF NOT EXISTS student_enrollments (
    id SERIAL PRIMARY KEY,
    student_id INTEGER REFERENCES students_master(id) ON DELETE CASCADE,
    academic_year_id INTEGER REFERENCES academic_years(id),
    standard VARCHAR(10) NOT NULL,
    section VARCHAR(5),
    enrolled_on TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    -- Ensure a student can only be enrolled once per academic year
    UNIQUE(student_id, academic_year_id) 
);

-- 3. Seed the current academic year automatically
INSERT INTO academic_years (year_name, start_date, end_date, is_active)
VALUES ('2026-2027', '2026-06-01', '2027-04-30', TRUE)
ON CONFLICT (year_name) DO NOTHING;