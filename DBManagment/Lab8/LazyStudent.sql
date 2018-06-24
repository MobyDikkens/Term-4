--Sheludko Dima
--IP - 63
--Lab 8

DROP DATABASE LazyStudent
DROP DATABASE LazyStudents

--Creating the database
CREATE DATABASE LazyStudent;

--Creating DB tables

--	Clients
CREATE TABLE LazyStudent.dbo.Clients(
	ClientId INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(25),
	LastName VARCHAR(35),
	RegistrationDate DATE
);

--	Contacts

--	Clients

DROP TABLE LazyStudent.dbo.Contacts
CREATE TABLE LazyStudent.dbo.ClientContacts(
	ContactId INT IDENTITY(1,1) PRIMARY KEY,
	ContactType INT,
	Contact VARCHAR(255),
	OwnerId INT
);

--	Partners
CREATE TABLE LazyStudent.dbo.PartnerContacts(
	ContactId INT IDENTITY(1,1) PRIMARY KEY,
	ContactType INT,
	OwnerType INT,
	Contact VARCHAR(255),
	OwnerId INT
);

--	Teachers
CREATE TABLE LazyStudent.dbo.TeacherContacts(
	ContactId INT IDENTITY(1,1) PRIMARY KEY,
	ContactType INT,
	Contact VARCHAR(255),
	OwnerId INT
);


--	ContactType
CREATE TABLE LazyStudent.dbo.ContactType(
	ContactTypeId INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255)
);


--	OwnerType
CREATE TABLE LazyStudent.dbo.OwnerType(
	OwnerTypeId INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255)
);

--	Teachers
CREATE TABLE LazyStudent.dbo.Teachers(
	TeacherId INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(25),
	LastName VARCHAR(35),
	ServiciesGroupId INT UNIQUE,
	Rate INT
);

--	Servicies
CREATE TABLE LazyStudent.dbo.Servicies(
	ServiceID INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255)
);

--	Partners
CREATE TABLE LazyStudent.dbo.Partners(
	PartnerId INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255),
	ServiciesGroupId INT UNIQUE
);

--	ServiciesGroup
CREATE TABLE LazyStudent.dbo.ServiciesGroups(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	ServiciesGroupId INT,
	ServiceId INT
);

--	Orders
CREATE TABLE LazyStudent.dbo.Orders(
	OrderId BIGINT IDENTITY(1,1) PRIMARY KEY,
	StatusId INT,
	ClientId INT,
	PerformerId INT,
	OrderDate DATE,
	ServiceId INT,
	Discount INT,
	TotalCost INT
);

--	OrderStatuses
CREATE TABLE LazyStudent.dbo.OrderStatuses(
	StatusId INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255)
);

--	OrdersArchive
SELECT * INTO LazyStudent.dbo.OrdersArchive FROM LazyStudent.dbo.Orders;
ALTER TABLE LazyStudent.dbo.OrdersArchive ADD CompletionDate DATE;  


--	Comments
CREATE TABLE LazyStudent.dbo.Comments(
	CommentId INT IDENTITY(1,1) PRIMARY KEY,
	ClientId INT,
	PerformerId INT,
	Comment TEXT,
	Rate INT,
	Date DATE
);

--	Discounts
CREATE TABLE LazyStudent.dbo.Discounts(
	DiscountId INT IDENTITY(1,1) PRIMARY KEY,
	Value FLOAT(5)
);

--	Shares
CREATE TABLE LazyStudent.dbo.Shares(
	ShareID INT IDENTITY(1,1) PRIMARY KEY,
	StartingDate DATE,
	ExceptingDate DATE,
	CompanyId INT,
	ServiceId INT,
	DiscountId INT
);


--	Trigger to create record in OrdersArchive after deleting from Orders
USE LazyStudent
SET IDENTITY_INSERT LazyStudent.dbo.OrdersArchive ON
GO
CREATE OR ALTER TRIGGER trg_delete_from_orders 
	ON dbo.Orders 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.OrdersArchive(
		OrderId,
		StatusId,
		OrderDate,
		CompletionDate,
		ServiceId,
		ClientId,
		PerformerId,
		Discount,
		TotalCost)
	 SELECT 
		OrderId,
		StatusId,
		OrderDate,
		CURRENT_TIMESTAMP,
		ServiceId,
		ClientId,
		PerformerId,
		Discount,
		TotalCost
	FROM deleted
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'Orders'
END


--	Create Table Delitings History
CREATE TABLE LazyStudent.dbo.DeletingsHistory(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	UserName SYSNAME,
	DeletionDate DATE,
	TableName VARCHAR(30)
);

--	Create triggers on delete to add info to the table

--	Clients
GO
CREATE OR ALTER TRIGGER trg_delete_from_clients
	ON dbo.Clients 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'Clients'
END

--	Contacts

--	Clients
GO
CREATE OR ALTER TRIGGER trg_delete_from_clientcontacts
	ON dbo.ClientContacts 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'ClientContacts'
END


--	Partners
GO
CREATE OR ALTER TRIGGER trg_delete_from_partnercontacts
	ON dbo.PartnerContacts 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'PartnerContacts'
END

--	Teachers
GO
CREATE OR ALTER TRIGGER trg_delete_from_teachercontacts
	ON dbo.TeacherContacts 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'TeacherContacts'
END




--	Teachers
GO
CREATE OR ALTER TRIGGER trg_delete_from_teachers
	ON dbo.Teachers 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'Teachers'
END

--	Partners
GO
CREATE OR ALTER TRIGGER trg_delete_from_partners
	ON dbo.Partners 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'Partners'
END

--	Comments
GO
CREATE OR ALTER TRIGGER trg_delete_from_comments
	ON dbo.Comments
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'Comments'
END

--	Discounts
GO
CREATE OR ALTER TRIGGER trg_delete_from_discounts
	ON dbo.Discounts 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'Discounts'
END

--	Shares
GO
CREATE OR ALTER TRIGGER trg_delete_from_shares
	ON dbo.Shares 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'Shares'
END

--	Servicies

GO
CREATE OR ALTER TRIGGER trg_delete_from_servicies
	ON dbo.Servicies 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'Servicies'
END

--	ServiciesGroups
GO
CREATE OR ALTER TRIGGER trg_delete_from_serviciesgroups
	ON dbo.ServiciesGroups 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'ServiciesGroups'
END

--	ContactType

GO
CREATE OR ALTER TRIGGER trg_delete_from_contacttype
	ON dbo.ContactType 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'ContactType'
END


--	OwnerType

GO
CREATE OR ALTER TRIGGER trg_delete_from_ownertype
	ON dbo.OwnerType 
	AFTER DELETE 
AS
BEGIN
	INSERT INTO dbo.DeletingsHistory(
		UserName,
		DeletionDate,
		TableName)
	SELECT
		CURRENT_USER,
		CURRENT_TIMESTAMP,
		'OwnerType'
END

--	Create roles

CREATE LOGIN Administrator
WITH PASSWORD = 'JEe8dYjED!@wat6ANP#&kMvy=rhRdM&E6huzL3GXmM5WJWGJF-%Cwppv6XuajveB!KzN!C7$&%sVJ#+meAKCCmPhK2^&wQfFBrpve+MYD_PDRE?xW%JFzH4g_2NdYf_k'

CREATE LOGIN Director
WITH PASSWORD = 'R_&FMUWn665$UWprff2wXe_cE2rMT-KH6zPeF=CVy$yG?MCaHc2R^+zzZEc7k=b@$2k_@a$T+BD226Se$9?pfsG3XYRr*VAENqzq!NM9j73JyT&3wRQ6VpPFP^&QU-!y'

CREATE LOGIN CustomUser
WITH PASSWORD = 'VictorPavlik'

CREATE USER admin FOR LOGIN Administrator
CREATE USER director FOR LOGIN Director
CREATE USER customuser FOR LOGIN CustomUser

USE LazyStudent
DENY UPDATE, INSERT, DELETE TO customuser

GRANT CONTROL TO admin


ALTER USER Director WITH DEFAULT_SCHEMA = dbo
GRANT SELECT ON USER::Director TO director
DENY ALTER, INSERT, UPDATE TO director
