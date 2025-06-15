DROP TABLE IF EXISTS Users;
GO

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL DEFAULT 'Staff' -- 'Admin', 'Manager', 'Staff', 'Supplier'
);
GO

-- Sample users
INSERT INTO Users (Username, Password, Role) VALUES 
('admin', 'admin123', 'Admin'),
('manager', 'manager123', 'Manager'),
('staff', 'staff123', 'Staff'),
('supplier', 'supplier123', 'Supplier');
GO
