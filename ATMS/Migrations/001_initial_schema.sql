-- 001_initial_schema.sql

CREATE TABLE IF NOT EXISTS students_master (
    id SERIAL PRIMARY KEY,
    admission_no VARCHAR(20) UNIQUE NOT NULL,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100),
    standard VARCHAR(10) NOT NULL, -- e.g., 'LKG', '10th'
    section VARCHAR(5),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS fee_heads (
    id SERIAL PRIMARY KEY,
    head_name VARCHAR(100) UNIQUE NOT NULL,
    default_amount DECIMAL(10, 2) NOT NULL
);

-- Note: We will add more columns (guardian info, etc.) in 002_*.sql later as requirements clarify.
CREATE TABLE IF NOT EXISTS fee_ledger (
    id SERIAL PRIMARY KEY,
    student_id INTEGER REFERENCES students_master(id),
    fee_head_id INTEGER REFERENCES fee_heads(id),
    allocated_amount DECIMAL(10, 2) NOT NULL,
    paid_amount DECIMAL(10, 2) NOT NULL DEFAULT 0,
    receipt_no VARCHAR(50),
    is_cancelled BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE SEQUENCE IF NOT EXISTS seq_receipt_number START 1000;