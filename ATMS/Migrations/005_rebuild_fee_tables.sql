-- 005_rebuild_fee_tables.sql

DROP TABLE IF EXISTS fee_ledger CASCADE;
DROP TABLE IF EXISTS fee_heads CASCADE;

CREATE TABLE fee_heads (
    id SERIAL PRIMARY KEY,
    head_name VARCHAR(100) UNIQUE NOT NULL,
    default_amount DECIMAL(10, 2) NOT NULL,
    is_active BOOLEAN DEFAULT TRUE
);

CREATE TABLE fee_ledger (
    id SERIAL PRIMARY KEY,
    student_id INTEGER REFERENCES students_master(id) ON DELETE CASCADE,
    academic_year_id INTEGER REFERENCES academic_years(id), -- Locks the fee to the year
    fee_head_id INTEGER REFERENCES fee_heads(id),
    allocated_amount DECIMAL(10, 2) NOT NULL,
    paid_amount DECIMAL(10, 2) NOT NULL DEFAULT 0,
    receipt_no VARCHAR(50),
    is_cancelled BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Seed some standard school fee heads
INSERT INTO fee_heads (head_name, default_amount) VALUES 
('Term 1 Tuition Fee', 5000.00),
('Term 2 Tuition Fee', 5000.00),
('Term 3 Tuition Fee', 5000.00),
('Annual Computer Fee', 1500.00),
('Books and Uniform', 3000.00);