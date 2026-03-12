-- Script de creación de base de datos y procedimientos almacenados
-- Se asume el uso de SQL Server

-- 1. Creación de la Tabla
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(MAX),
        Price DECIMAL(18, 2) NOT NULL,
        CreatedDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 2. Procedimientos Almacenados

-- Obtener todos los productos
CREATE OR ALTER PROCEDURE sp_GetProducts
AS
BEGIN
    SELECT Id, Name, Description, Price, CreatedDate FROM Products ORDER BY CreatedDate DESC;
END
GO

-- Obtener producto por Id
CREATE OR ALTER PROCEDURE sp_GetProductById
    @Id INT
AS
BEGIN
    SELECT Id, Name, Description, Price, CreatedDate FROM Products WHERE Id = @Id;
END
GO

-- Insertar producto
CREATE OR ALTER PROCEDURE sp_InsertProduct
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Products (Name, Description, Price, CreatedDate)
    VALUES (@Name, @Description, @Price, GETDATE());
END
GO

-- Actualizar producto
CREATE OR ALTER PROCEDURE sp_UpdateProduct
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(18, 2)
AS
BEGIN
    UPDATE Products
    SET Name = @Name,
        Description = @Description,
        Price = @Price
    WHERE Id = @Id;
END
GO

-- Eliminar producto
CREATE OR ALTER PROCEDURE sp_DeleteProduct
    @Id INT
AS
BEGIN
    DELETE FROM Products WHERE Id = @Id;
END
GO
