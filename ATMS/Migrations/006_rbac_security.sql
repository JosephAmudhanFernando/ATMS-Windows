CREATE TABLE roles (
    id SERIAL PRIMARY KEY,
    role_name VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE permissions (
    code VARCHAR(50) PRIMARY KEY,
    description VARCHAR(100) NOT NULL
);

CREATE TABLE role_permissions (
    role_id INTEGER REFERENCES roles(id) ON DELETE CASCADE,
    permission_code VARCHAR(50) REFERENCES permissions(code) ON DELETE CASCADE,
    PRIMARY KEY (role_id, permission_code)
);

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    role_id INTEGER REFERENCES roles(id),
    is_active BOOLEAN DEFAULT TRUE
);

-- 1. Seed Roles
INSERT INTO roles (id, role_name) VALUES 
(1, 'ATMS'), (2, 'Management'), (3, 'School Admin'), (4, 'Principal');

-- 2. Seed Granular Permissions
INSERT INTO permissions (code, description) VALUES
('MasterData.View', 'View Master Data'),
('MasterData.Create', 'Add New Master Data'),
('MasterData.Edit', 'Modify Existing Master Data'),
('MasterData.Delete', 'Delete Master Data'),
('Finance.FeeEntry', 'Collect Fees and View Daily Transactions'),
('Finance.EditTransaction', 'Modify Completed Fee Transactions'),
('Reports.Daily', 'View 1-Day Snapshots'),
('Reports.Aggregate', 'View Monthly/Yearly Financial Reports');

-- 3. Map Privileges to Roles based on Management Requirements

-- ATMS: Full Access
INSERT INTO role_permissions (role_id, permission_code)
SELECT 1, code FROM permissions;

-- Management: Full Access EXCEPT Finance modules
INSERT INTO role_permissions (role_id, permission_code)
SELECT 2, code FROM permissions WHERE code NOT LIKE 'Finance.%';

-- School Admin: Operational, 1-Day Reports, No Master Edit/Delete, No Fee Altering
INSERT INTO role_permissions (role_id, permission_code) VALUES
(3, 'MasterData.View'),
(3, 'MasterData.Create'),
(3, 'Finance.FeeEntry'),
(3, 'Reports.Daily');

-- Principal: Same as School Admin, but NO Fee Entry
INSERT INTO role_permissions (role_id, permission_code) VALUES
(4, 'MasterData.View'),
(4, 'Reports.Daily');