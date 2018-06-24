--1. Вивести на екран імена усіх таблиць в базі даних та кількість рядків в них.

DECLARE @Name nvarchar(30)

DECLARE DBNameCursor CURSOR FOR 
	SELECT name FROM sys.databases

OPEN DBNameCursor

WHILE(@@FETCH_STATUS = 0)
BEGIN
	FETCH NEXT FROM DBNameCursor INTO @Name

	EXEC('USE ' + @Name + 
	' SELECT o.name AS "Name", ddps.row_count AS "RowCount" 
	  FROM sys.indexes AS i
	  INNER JOIN sys.objects AS o ON i.OBJECT_ID = o.OBJECT_ID
      INNER JOIN sys.dm_db_partition_stats AS ddps ON i.OBJECT_ID = ddps.OBJECT_ID
	  AND i.index_id = ddps.index_id 
	  WHERE i.index_id < 2  AND o.is_ms_shipped = 0 ORDER BY o.NAME ')	


END
CLOSE DBNameCursor
DEALLOCATE DBNameCursor






SELECT SO.name, SI.rowcnt FROM sysobjects AS SO
	INNER JOIN sysindexes AS SI
	ON SI.id = SO.id
	WHERE SO.type = 'u'





DECLARE DBNameCursor CURSOR FOR 
	SELECT name FROM sys.databases

DECLARE @Name nvarchar(30)

OPEN DBNameCursor

WHILE(@@FETCH_STATUS = 0)
BEGIN
	FETCH NEXT FROM DBNameCursor INTO @Name

	EXEC('USE ' + @Name + 
		'SELECT SO.name, SI.rowcnt FROM sysobjects AS SO
		INNER JOIN sysindexes AS SI
		ON SI.id = SO.id
		WHERE SO.type = ''u''')	


END
CLOSE DBNameCursor
DEALLOCATE DBNameCursor





--2. Видати дозвіл на читання бази даних Northwind усім користувачам вашої СУБД. Код повинен
--працювати в незалежності від імен існуючих користувачів.


USE Northwind
GRANT SELECT ON SCHEMA ::[dbo] TO public



DECLARE DBUsersCursor CURSOR FOR
	SELECT name FROM master.sys.server_principals;

DECLARE @User NVARCHAR(128)

OPEN DBUsersCursor

WHILE(@@FETCH_STATUS = 0)
BEGIN
	FETCH NEXT FROM DBUsersCursor INTO @User

	EXEC('GRANT SELECT ON Northwind TO' + @User)


END
CLOSE DBUsersCursor
DEALLOCATE DBUsersCursor



--3. За допомогою курсору заборонити користувачеві TestUser доступ до всіх таблиць поточної
--бази даних, імена котрих починаються на префікс ‘prod_’.

DECLARE @TableName NVARCHAR(128)
DECLARE @Schema NVARCHAR(128)


DECLARE TableCursor CURSOR FOR
	SELECT TABLE_NAME, TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME LIKE 'prod\\_%'

OPEN TableCursor


WHILE(@@FETCH_STATUS = 0)
BEGIN
	FETCH NEXT FROM TableCursor INTO @TableName, @Schema
	EXEC('DENY SELECT ON ' +'.'+@Schema+'.'+@TableName + ' TO TestUser')
END

CLOSE TableCursor
DEALLOCATE TableCursor

--4. Створити тригер на таблиці Customers, що при вставці нового телефонного номеру буде
--видаляти усі символи крім цифер.

USE Northwind
GO
CREATE FUNCTION dbo.f_RemoveChars(@Input varchar(1000))
RETURNS varchar(1000) AS
BEGIN
DECLARE @pos smallint
SET @Pos = PATINDEX('%[^0-9]%',@Input)
WHILE @Pos > 0 BEGIN
 SET @Input = STUFF(@Input,@pos,1,'')
 SET @Pos = PATINDEX('%[^0-9]%',@Input)
END
RETURN @Input END

USE Northwind
SET ANSI_NULLS ON

GO
CREATE TRIGGER [dbo].[tr_insert_customers] ON [dbo].[Customers] INSTEAD OF INSERT AS 
BEGIN
	DECLARE @Phone NVARCHAR(12)
	SELECT @Phone = Phone FROM inserted
	SELECT @Phone = dbo.f_RemoveChars(@Phone)
	INSERT INTO dbo.Customers SELECT CustomerID ,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,@Phone,Fax FROM inserted
END

INSERT INTO Northwind.dbo.Customers
VALUES (1,'1','23','123','123','123','123','123','123', '12312sdad','213');

SELECT * FROM Northwind.dbo.Customers

--5. Створити таблицю Contacts (ContactId, LastName, FirstName, PersonalPhone, WorkPhone, Email,
--PreferableNumber). Створити тригер, що при вставці даних в таблицю Contacts вставить в
--якості PreferableNumber WorkPhone якщо він присутній, або PersonalPhone, якщо робочий
--номер телефона не вказано.

USE IP63
CREATE TABLE Contacts(
	ContactId int IDENTITY(1,1) Primary Key,
	LastName NVARCHAR(128),
	FirstName NVARCHAR(128),
	PersonalPhone NVARCHAR(20),
	WorkPhone NVARCHAR(20),
	Email NVARCHAR(128),
	PreferableNumber NVARCHAR(20)
)

USE IP63 
GO
CREATE TRIGGER dbo.tr_preferable_number ON dbo.Contacts AFTER INSERT AS
BEGIN
	DECLARE @Work nvarchar(20)
	DECLARE @Personal nvarchar(20)
	DECLARE @Preferable nvarchar(20)

	SELECT @Work = WorkPhone FROM inserted
	SELECT @Personal = PersonalPhone FROM inserted

	IF(@Work IS NULL)
		SELECT @Preferable = @Personal
	ELSE
		SELECT @Preferable = @Work

	UPDATE IP63.dbo.Contacts SET PreferableNumber = @Preferable

END

INSERT INTO IP63.dbo.Contacts (WorkPhone)
VALUES (12);

SELECT * FROM IP63.dbo.Contacts


--6. Створити таблицю OrdersArchive що дублює таблицію Orders та має додаткові атрибути
--DeletionDateTime та DeletedBy. Створити тригер, що при видаленні рядків з таблиці Orders
--буде додавати їх в таблицю OrdersArchive та заповнювати відповідні колонки.

USE Northwind 
SELECT * INTO OrdersArchive FROM Orders WHERE 1 = 0;
ALTER TABLE OrdersArchive
ADD DeletionDateTime DateTime,
	DeletedBy nvarchar(36)

USE Northwind 
GO
CREATE TRIGGER dbo.tr_del_from_orders ON dbo.Orders AFTER DELETE AS
BEGIN
	DECLARE @DeleteTime DateTime
	DECLARE @DeletedBy nvarchar(128)

	SELECT @DeletedBy = USER_NAME()
	SELECT @DeleteTime = CURRENT_TIMESTAMP

	--	list of columns to avoid IDENTITY_INSERT 
	INSERT INTO dbo.OrdersArchive
	(
		OrderID, 
		CustomerID,
		EmployeeID,
		OrderDate,
		RequiredDate,
		ShippedDate,
		ShipVia,
		Freight,
		ShipName,
		ShipAddress,
		ShipCity,
		ShipRegion,
		ShipPostalCode,
		ShipCountry,
		DeletionDateTime,
		DeletedBy)
	SELECT 
		OrderID, 
		CustomerID,
		EmployeeID,
		OrderDate,
		RequiredDate,
		ShippedDate,
		ShipVia,
		Freight,
		ShipName,
		ShipAddress,
		ShipCity,
		ShipRegion,
		ShipPostalCode,
		ShipCountry,
		@DeleteTime,
		@DeletedBy
	FROM deleted
END





--7. Створити три таблиці: TriggerTable1, TriggerTable2 та TriggerTable3. Кожна з таблиць має
--наступну структуру: TriggerId(int) – первинний ключ з автоінкрементом, TriggerDate(Date).
--Створити три тригера. Перший тригер повинен при будь-якому записі в таблицю TriggerTable1
--додати дату запису в таблицю TriggerTable2. Другий тригер повинен при будь-якому записі в
--таблицю TriggerTable2 додати дату запису в таблицю TriggerTable3. Третій тригер працює
--аналогічно за таблицями TriggerTable3 та TriggerTable1. Вставте один рядок в таблицю
--TriggerTable1. Напишіть, що відбулось в коментарі до коду. Чому це сталося?

USE IP63
CREATE TABLE IP63.dbo.TriggerTable1(
	TgiggerID int Primary KEY IDENTITY(1,1),
	TriggerDate date
);

CREATE TABLE IP63.dbo.TriggerTable2(
	TgiggerID int Primary KEY IDENTITY(1,1),
	TriggerDate date
);

CREATE TABLE IP63.dbo.TriggerTable3(
	TgiggerID int Primary KEY IDENTITY(1,1),
	TriggerDate date
);

 
USE IP63 
GO
CREATE TRIGGER dbo.tr_triger1_2 ON dbo.TriggerTable1 AFTER INSERT AS
BEGIN
	INSERT INTO [dbo].[TriggerTable2] (TriggerDate) SELECT [TriggerDate] FROM inserted
END

GO
CREATE TRIGGER dbo.tr_triger2_3 ON dbo.TriggerTable2 AFTER INSERT AS
BEGIN
	INSERT INTO dbo.TriggerTable3 (TriggerDate) SELECT TriggerDate FROM inserted
END

GO
CREATE TRIGGER dbo.tr_triger3_1 ON dbo.TriggerTable3 AFTER INSERT AS
BEGIN
	INSERT INTO dbo.TriggerTable1 (TriggerDate) SELECT TriggerDate FROM inserted
END 



INSERT INTO dbo.TriggerTable1 (TriggerDate) SELECT CURRENT_TIMESTAMP;

--	Maximum stored procedure, function, trigger, or view nesting level exceeded (limit 32).
--	It is smth like recursive triggers and max depth of recursive has reached so we must throw an exeption 
--	and calncell the transactions