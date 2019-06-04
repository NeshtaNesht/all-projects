--
-- Скрипт сгенерирован Devart dbForge Studio 2019 for SQL Server, Версия 5.7.31.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 05.06.2019 0:15:10
-- Версия сервера: 14.00.1000
--



USE [Оптовый склад]
GO

IF DB_NAME() <> N'Оптовый склад' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[Contracts]
--
PRINT (N'Создать таблицу [dbo].[Contracts]')
GO
CREATE TABLE dbo.Contracts (
  id int IDENTITY,
  duration int NULL,
  isDelete bit NULL DEFAULT (0),
  date_create date NULL,
  CONSTRAINT PK_Contracts_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать процедуру [dbo].[DeleteContract]
--
GO
PRINT (N'Создать процедуру [dbo].[DeleteContract]')
GO
CREATE OR ALTER PROCEDURE dbo.DeleteContract
@id   int
AS 
BEGIN
	UPDATE Contracts 
SET isDelete = 1
WHERE id = @id;
END
GO

--
-- Создать процедуру [dbo].[AddEditContract]
--
GO
PRINT (N'Создать процедуру [dbo].[AddEditContract]')
GO
CREATE OR ALTER PROCEDURE dbo.AddEditContract @id_for_update INT,
@duration INT,
@operation INT
AS
BEGIN
  IF @operation = 0
  BEGIN
    INSERT INTO Contracts (duration, isDelete)
      SELECT
        @duration
       ,0
  END
  ELSE
  BEGIN
    UPDATE Contracts
    SET duration = @duration
       ,isDelete = DEFAULT
    WHERE id = @id_for_update;
  END
END
GO

--
-- Создать таблицу [dbo].[Out]
--
PRINT (N'Создать таблицу [dbo].[Out]')
GO
CREATE TABLE dbo.Out (
  id int IDENTITY,
  number_invoice int NULL,
  date date NULL,
  id_contract int NULL,
  CONSTRAINT PK_Out_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Out_id_contract] для объекта типа таблица [dbo].[Out]
--
PRINT (N'Создать внешний ключ [FK_Out_id_contract] для объекта типа таблица [dbo].[Out]')
GO
ALTER TABLE dbo.Out
  ADD CONSTRAINT FK_Out_id_contract FOREIGN KEY (id_contract) REFERENCES dbo.Contracts (id)
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[Out] (таблица)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[Out] (таблица)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Расход', 'SCHEMA', N'dbo', 'TABLE', N'Out'
GO

--
-- Создать процедуру [dbo].[UpdateOut]
--
GO
PRINT (N'Создать процедуру [dbo].[UpdateOut]')
GO
-- Обновление данных расхода
CREATE OR ALTER PROCEDURE dbo.UpdateOut
@number_invoice   INT,
@date             DATE,
@number_contract  int
AS 
BEGIN
	UPDATE Out 
SET number_invoice = @number_invoice
   ,date = @date
   ,id_contract = @number_contract
WHERE id = IDENT_CURRENT('Out');
END
GO

--
-- Создать таблицу [dbo].[Advent]
--
PRINT (N'Создать таблицу [dbo].[Advent]')
GO
CREATE TABLE dbo.Advent (
  id int IDENTITY,
  number_invoice int NULL,
  date date NULL,
  id_contract int NULL,
  CONSTRAINT PK_Advent_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Advent_id_contract] для объекта типа таблица [dbo].[Advent]
--
PRINT (N'Создать внешний ключ [FK_Advent_id_contract] для объекта типа таблица [dbo].[Advent]')
GO
ALTER TABLE dbo.Advent
  ADD CONSTRAINT FK_Advent_id_contract FOREIGN KEY (id_contract) REFERENCES dbo.Contracts (id)
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[Advent] (таблица)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[Advent] (таблица)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Приход', 'SCHEMA', N'dbo', 'TABLE', N'Advent'
GO

--
-- Создать таблицу [dbo].[Units]
--
PRINT (N'Создать таблицу [dbo].[Units]')
GO
CREATE TABLE dbo.Units (
  id int IDENTITY,
  name nvarchar(15) NULL,
  CONSTRAINT PK_Units_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[Units] (таблица)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[Units] (таблица)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Единицы измерения', 'SCHEMA', N'dbo', 'TABLE', N'Units'
GO

--
-- Создать таблицу [dbo].[Out_Detail]
--
PRINT (N'Создать таблицу [dbo].[Out_Detail]')
GO
CREATE TABLE dbo.Out_Detail (
  id int IDENTITY,
  id_out int NULL,
  product nvarchar(max) NULL,
  id_unit int NULL,
  count int NULL,
  price float NULL,
  CONSTRAINT PK_Out_Detail_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Out_Detail_id_out] для объекта типа таблица [dbo].[Out_Detail]
--
PRINT (N'Создать внешний ключ [FK_Out_Detail_id_out] для объекта типа таблица [dbo].[Out_Detail]')
GO
ALTER TABLE dbo.Out_Detail
  ADD CONSTRAINT FK_Out_Detail_id_out FOREIGN KEY (id_out) REFERENCES dbo.Out (id)
GO

--
-- Создать внешний ключ [FK_Out_Detail_id_unit] для объекта типа таблица [dbo].[Out_Detail]
--
PRINT (N'Создать внешний ключ [FK_Out_Detail_id_unit] для объекта типа таблица [dbo].[Out_Detail]')
GO
ALTER TABLE dbo.Out_Detail
  ADD CONSTRAINT FK_Out_Detail_id_unit FOREIGN KEY (id_unit) REFERENCES dbo.Units (id)
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[Out_Detail] (таблица)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[Out_Detail] (таблица)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Детали расхода', 'SCHEMA', N'dbo', 'TABLE', N'Out_Detail'
GO

--
-- Создать процедуру [dbo].[DeleteOutDetail]
--
GO
PRINT (N'Создать процедуру [dbo].[DeleteOutDetail]')
GO
CREATE OR ALTER PROCEDURE dbo.DeleteOutDetail
@id INT
AS 
BEGIN
	DELETE FROM Out_Detail WHERE id = @id
END
GO

--
-- Создать таблицу [dbo].[Advent_Detail]
--
PRINT (N'Создать таблицу [dbo].[Advent_Detail]')
GO
CREATE TABLE dbo.Advent_Detail (
  id int IDENTITY,
  id_advent int NULL,
  product nvarchar(max) NULL,
  id_unit int NULL,
  count int NULL,
  price float NULL,
  CONSTRAINT PK_Advent_Detail_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Advent_Detail_id_advent] для объекта типа таблица [dbo].[Advent_Detail]
--
PRINT (N'Создать внешний ключ [FK_Advent_Detail_id_advent] для объекта типа таблица [dbo].[Advent_Detail]')
GO
ALTER TABLE dbo.Advent_Detail
  ADD CONSTRAINT FK_Advent_Detail_id_advent FOREIGN KEY (id_advent) REFERENCES dbo.Advent (id)
GO

--
-- Создать внешний ключ [FK_Advent_Detail_id_unit] для объекта типа таблица [dbo].[Advent_Detail]
--
PRINT (N'Создать внешний ключ [FK_Advent_Detail_id_unit] для объекта типа таблица [dbo].[Advent_Detail]')
GO
ALTER TABLE dbo.Advent_Detail
  ADD CONSTRAINT FK_Advent_Detail_id_unit FOREIGN KEY (id_unit) REFERENCES dbo.Units (id)
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[Advent_Detail] (таблица)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[Advent_Detail] (таблица)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Детали прихода', 'SCHEMA', N'dbo', 'TABLE', N'Advent_Detail'
GO

--
-- Создать процедуру [dbo].[DeleteAdventDetail]
--
GO
PRINT (N'Создать процедуру [dbo].[DeleteAdventDetail]')
GO
CREATE OR ALTER PROCEDURE dbo.DeleteAdventDetail
@id   int
AS 
BEGIN
	DELETE FROM Advent_Detail WHERE id = @id
END
GO

--
-- Создать процедуру [dbo].[AddEditAdventDetail]
--
GO
PRINT (N'Создать процедуру [dbo].[AddEditAdventDetail]')
GO
-- Хранимая процедура добавления/редактирования деталей прихода
CREATE OR ALTER PROCEDURE dbo.AddEditAdventDetail
@id_for_update  INT,
@product        NVARCHAR(MAX),
@unit           NVARCHAR(10),
@count          INT,
@price          FLOAT,
@operation      INT   
AS 
BEGIN
  -- Если это операция добавления
	IF @operation = 0
  BEGIN
  -- Вставляем данные
    INSERT INTO Advent_Detail (id_advent, product, id_unit, count, price)
      SELECT
        (SELECT a.id FROM Advent a WHERE a.id = IDENT_CURRENT('Advent')),
        @product,
        (SELECT u.id FROM Units u WHERE u.name = @unit),
        @count,
        @price
  END
  ELSE
  BEGIN
    -- Иначе обновляем
    UPDATE Advent_Detail 
    SET id_advent = IDENT_CURRENT('Advent')
       ,product = @product
       ,id_unit = (SELECT u.id FROM Units u WHERE u.name = @unit)
       ,count = @count
       ,price = @price
    WHERE id = @id_for_update;
  END
END
GO

--
-- Создать таблицу [dbo].[Suppliers]
--
PRINT (N'Создать таблицу [dbo].[Suppliers]')
GO
CREATE TABLE dbo.Suppliers (
  id int IDENTITY,
  name nvarchar(100) NULL,
  address nvarchar(250) NULL,
  phone nvarchar(30) NULL,
  inn nvarchar(50) NULL,
  id_contract int NULL,
  CONSTRAINT PK_Suppliers_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Suppliers_id_contract] для объекта типа таблица [dbo].[Suppliers]
--
PRINT (N'Создать внешний ключ [FK_Suppliers_id_contract] для объекта типа таблица [dbo].[Suppliers]')
GO
ALTER TABLE dbo.Suppliers
  ADD CONSTRAINT FK_Suppliers_id_contract FOREIGN KEY (id_contract) REFERENCES dbo.Contracts (id)
GO

--
-- Создать процедуру [dbo].[DeleteSupplier]
--
GO
PRINT (N'Создать процедуру [dbo].[DeleteSupplier]')
GO
CREATE OR ALTER PROCEDURE dbo.DeleteSupplier
@id   int
AS 
BEGIN
	DELETE FROM Suppliers WHERE id = @id
END
GO

--
-- Создать процедуру [dbo].[AddEditSupplier]
--
GO
PRINT (N'Создать процедуру [dbo].[AddEditSupplier]')
GO
CREATE OR ALTER PROCEDURE dbo.AddEditSupplier @id_for_update INT,
@name NVARCHAR(100),
@address NVARCHAR(50),
@phone NVARCHAR(30),
@inn NVARCHAR(50),
@contract INT,
@operation INT
AS
BEGIN
  IF @operation = 0
  BEGIN
    INSERT INTO Suppliers (name, address, phone, inn, id_contract)
      SELECT
        @name
       ,@address
       ,@phone
       ,@inn
       ,@contract
  END
  ELSE
  BEGIN
    UPDATE Suppliers
    SET name = @name
       ,address = @address
       ,phone = @phone
       ,inn = @inn
       ,id_contract = @contract
    WHERE id = @id_for_update;
  END
END
GO

--
-- Создать таблицу [dbo].[Roles]
--
PRINT (N'Создать таблицу [dbo].[Roles]')
GO
CREATE TABLE dbo.Roles (
  id int IDENTITY,
  name_role nvarchar(50) NULL,
  CONSTRAINT PK_Roles_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[Roles] (таблица)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[Roles] (таблица)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Роли пользователей', 'SCHEMA', N'dbo', 'TABLE', N'Roles'
GO

--
-- Создать таблицу [dbo].[Users]
--
PRINT (N'Создать таблицу [dbo].[Users]')
GO
CREATE TABLE dbo.Users (
  id int IDENTITY,
  name nvarchar(50) NULL,
  password nvarchar(50) NULL,
  id_role int NULL,
  CONSTRAINT PK_Users_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Users_id_role] для объекта типа таблица [dbo].[Users]
--
PRINT (N'Создать внешний ключ [FK_Users_id_role] для объекта типа таблица [dbo].[Users]')
GO
ALTER TABLE dbo.Users
  ADD CONSTRAINT FK_Users_id_role FOREIGN KEY (id_role) REFERENCES dbo.Roles (id)
GO

--
-- Создать таблицу [dbo].[Rest]
--
PRINT (N'Создать таблицу [dbo].[Rest]')
GO
CREATE TABLE dbo.Rest (
  id int IDENTITY,
  product nvarchar(max) NULL,
  count int NULL,
  CONSTRAINT PK_Rest_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[Rest] (таблица)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[Rest] (таблица)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', 'Остатки на складе', 'SCHEMA', N'dbo', 'TABLE', N'Rest'
GO

--
-- Создать процедуру [dbo].[UpdateAdvent]
--
GO
PRINT (N'Создать процедуру [dbo].[UpdateAdvent]')
GO
-- Хранимая процедура обновления данных прихода и вставки в таблицу остатков
CREATE OR ALTER PROCEDURE dbo.UpdateAdvent @number_invoice INT,
@date DATE,
@number_contract INT
AS
BEGIN
  UPDATE Advent
  SET number_invoice = @number_invoice
     ,date = @date
     ,id_contract = @number_contract
  WHERE id = IDENT_CURRENT('Advent');
  INSERT INTO Rest (product, count)
    SELECT
      ad.product
     ,SUM(ad.count)
    FROM Advent_Detail ad
    WHERE ad.id_advent = (SELECT
        a.id
      FROM Advent a
      WHERE a.id = IDENT_CURRENT('Advent'))
      GROUP BY ad.product
END
GO

--
-- Создать процедуру [dbo].[AddEditOutDetail]
--
GO
PRINT (N'Создать процедуру [dbo].[AddEditOutDetail]')
GO
CREATE OR ALTER PROCEDURE dbo.AddEditOutDetail @id_for_update INT,
@product NVARCHAR(MAX),
@count INT,
@price FLOAT,
@operation INT
AS
BEGIN
  IF @operation = 0
  BEGIN
    INSERT INTO Out_Detail (id_out, product, id_unit, count, price)
      SELECT
        (SELECT
            a.id
          FROM Out a
          WHERE a.id = IDENT_CURRENT('Out'))
       ,@product
       ,(SELECT
            u.id
          FROM Units u
          INNER JOIN Advent_Detail ad
            ON ad.id_unit = u.id
          WHERE ad.product = @product
          GROUP BY ad.product
                  ,u.id)
       ,@count
       ,@price
    --Обновляем остатки
    UPDATE Rest
    SET product = @product
       ,count = (count - @count)
    WHERE product = @product;
  END
END
GO

--
-- Создать таблицу [dbo].[Customers]
--
PRINT (N'Создать таблицу [dbo].[Customers]')
GO
CREATE TABLE dbo.Customers (
  id int IDENTITY,
  name nvarchar(100) NULL,
  address nvarchar(250) NULL,
  phone nvarchar(30) NULL,
  inn nvarchar(25) NULL,
  id_contract int NULL,
  CONSTRAINT PK_Customers_id PRIMARY KEY CLUSTERED (id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_Customers_id_contract] для объекта типа таблица [dbo].[Customers]
--
PRINT (N'Создать внешний ключ [FK_Customers_id_contract] для объекта типа таблица [dbo].[Customers]')
GO
ALTER TABLE dbo.Customers
  ADD CONSTRAINT FK_Customers_id_contract FOREIGN KEY (id_contract) REFERENCES dbo.Contracts (id)
GO

--
-- Создать процедуру [dbo].[DeleteCustomer]
--
GO
PRINT (N'Создать процедуру [dbo].[DeleteCustomer]')
GO
CREATE OR ALTER PROCEDURE dbo.DeleteCustomer
@id   int
AS 
BEGIN
	DELETE FROM Customers WHERE id = @id
END
GO

--
-- Создать процедуру [dbo].[AddEditCustomer]
--
GO
PRINT (N'Создать процедуру [dbo].[AddEditCustomer]')
GO
CREATE OR ALTER PROCEDURE dbo.AddEditCustomer @id_for_update INT,
@name NVARCHAR(100),
@address NVARCHAR(50),
@phone NVARCHAR(30),
@inn NVARCHAR(50),
@contract INT,
@operation INT
AS
BEGIN
  IF @operation = 0
  BEGIN
    INSERT INTO Customers (name, address, phone, inn, id_contract)
      SELECT
        @name
       ,@address
       ,@phone
       ,@inn
       ,@contract
  END
  ELSE
  BEGIN
    UPDATE Customers
    SET name = @name
       ,address = @address
       ,phone = @phone
       ,inn = @inn
       ,id_contract = @contract
    WHERE id = @id_for_update;
  END
END
GO
SET NOEXEC OFF
GO