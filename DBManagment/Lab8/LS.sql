--Sheludko Dima
--IP - 63
--Lab 8

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

--	Partners Discounts 
CREATE TABLE LazyStudent.dbo.PartnerDiscounts(
	PartnerDiscountId INT IDENTITY(1,1) PRIMARY KEY,
	Value FLOAT(5)
);

--	Reffered to registration date Discounts
CREATE TABLE LazyStudent.dbo.DateDiscounts(
	DateDiscountId INT IDENTITY(1,1) PRIMARY KEY,
	RequiredRegistrationDurability INT
);

--	Shares

CREATE TABLE LazyStudent.dbo.Shares(
	ShareID INT IDENTITY(1,1) PRIMARY KEY,
	StartingDate DATE,
	ExceptingDate DATE,
	CompanyId INT,
	ServiceId INT,
	PartnerDiscountId INT
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

--	TriggerContacts

--	Clients Trigger
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


--	Partners Trigger
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

--	Teachers Trigger
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

--	Partner Discounts
GO
CREATE OR ALTER TRIGGER trg_delete_from_partnerdiscounts
	ON dbo.PartnerDiscounts 
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
		'PartnerDiscounts'
END

--	Date Discounts
GO
CREATE OR ALTER TRIGGER trg_delete_from_datediscounts
	ON dbo.DateDiscounts
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
		'DateDiscounts'
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
GRANT SELECT TO customuser

GRANT ALL PRIVILEGES TO director
DENY INSERT, UPDATE, DELETE TO director


GRANT ALL PRIVILEGES TO admin


--	Add disocunts to the table for 1, 2, 3, 4 + years

INSERT INTO LazyStudent.dbo.DateDiscounts(
	RequiredRegistrationDurability,
	Discount)
VALUES(
	'1',
	'0.05');

INSERT INTO LazyStudent.dbo.DateDiscounts(
	RequiredRegistrationDurability,
	Discount)
VALUES(
	'2',
	'0.08');

INSERT INTO LazyStudent.dbo.DateDiscounts(
	RequiredRegistrationDurability,
	Discount)
VALUES(
	'3',
	'0.11');

INSERT INTO LazyStudent.dbo.DateDiscounts(
	RequiredRegistrationDurability,
	Discount)
VALUES(
	'4',
	'0.15');
	
--	Create trigger to add a discount connected with registration date


GO
CREATE OR ALTER TRIGGER trg_insert_orders_add_discount 
	ON LazyStudent.dbo.Orders INSTEAD OF INSERT 
AS
BEGIN
	DECLARE @discount FLOAT(5)
	DECLARE @registrationdate DATE
	DECLARE @orderid INT

	SELECT @orderid = OrderId FROM inserted
	SELECT @discount = 0
	SELECT @registrationdate = RegistrationDate FROM inserted AS I 
		INNER JOIN LazyStudent.dbo.Clients AS C 
		ON C.ClientId = I.ClientId
	
	IF(YEAR(@registrationdate) >= 4)
		SELECT @discount = Discount FROM LazyStudent.dbo.DateDiscounts
			 WHERE RequiredRegistrationDurability = 4
	ELSE IF(YEAR(@registrationdate) >= 3)
		SELECT @discount = Discount FROM LazyStudent.dbo.DateDiscounts
			 WHERE RequiredRegistrationDurability = 3
	ELSE IF(YEAR(@registrationdate) >= 2)
		SELECT @discount = Discount FROM LazyStudent.dbo.DateDiscounts
			 WHERE RequiredRegistrationDurability = 2
	ELSE IF(YEAR(@registrationdate) >= 1)
		SELECT @discount = Discount FROM LazyStudent.dbo.DateDiscounts
			 WHERE RequiredRegistrationDurability = 1


	--	add data
	SELECT * INTO LazyStudent.dbo.Orders FROM inserted

	--	add discout
	UPDATE LazyStudent.dbo.Orders SET
		Discount = @discount
	WHERE
		OrderId = @orderid
	

END
