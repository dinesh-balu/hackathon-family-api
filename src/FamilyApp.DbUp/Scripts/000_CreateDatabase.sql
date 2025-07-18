IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'FamilyAppDb')
BEGIN
    CREATE DATABASE FamilyAppDb;
END
GO

USE FamilyAppDb;
GO
