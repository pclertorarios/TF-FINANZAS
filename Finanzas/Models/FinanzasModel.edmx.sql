CREATE DATABASE FinanzasDB
GO
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/30/2018 09:37:20
-- Generated from EDMX file: C:\Users\Alumnos\Documents\Git\TF-FINANZAS\Finanzas\Models\FinanzasModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FinanzasDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Bono_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bono] DROP CONSTRAINT [FK_Bono_Usuario];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bono]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bono];
GO
IF OBJECT_ID(N'[dbo].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuario];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bono'
CREATE TABLE [dbo].[Bono] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [vnominal] float  NOT NULL,
    [vcomercial] float  NOT NULL,
    [años] int  NOT NULL,
    [frecuencia] int  NOT NULL,
    [diasAño] int  NOT NULL,
    [tipoInteres] varchar(20)  NOT NULL,
    [capitalizacion] int  NULL,
    [tasaInteres] float  NOT NULL,
    [tasaDescuento] float  NOT NULL,
    [impuestoRenta] float  NOT NULL,
    [fechaEmision] datetime  NOT NULL,
    [pPrima] float  NOT NULL,
    [pEstructura] float  NOT NULL,
    [pColoca] float  NOT NULL,
    [pFlota] float  NOT NULL,
    [pCAVALI] float  NOT NULL,
    [UsuarioID] int  NOT NULL,
    [nombre] varchar(50)  NOT NULL,
    [tipoActor] varchar(50)  NOT NULL
);
GO

-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] varchar(50)  NOT NULL,
    [password] varchar(50)  NOT NULL,
    [nombre] varchar(50)  NOT NULL,
    [apellido] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Bono'
ALTER TABLE [dbo].[Bono]
ADD CONSTRAINT [PK_Bono]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsuarioID] in table 'Bono'
ALTER TABLE [dbo].[Bono]
ADD CONSTRAINT [FK_Bono_Usuario]
    FOREIGN KEY ([UsuarioID])
    REFERENCES [dbo].[Usuario]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Bono_Usuario'
CREATE INDEX [IX_FK_Bono_Usuario]
ON [dbo].[Bono]
    ([UsuarioID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------