--�������� � �������� ���� Northwind:

--1. ������ ���� �� ����������� ������ �� ������� Intern.


INSERT INTO Northwind.dbo.Employees
	(LastName,
	FirstName,
	Title)
VALUES 
	('Sheludko',
	 'Dima',
	 'Intern');

--2. ������ ���� ������ �� Director.

UPDATE 
	Northwind.dbo.Employees
SET
	Title = 'Director'
WHERE 
	LastName = 'Sheludko'
AND 
	FirstName = 'Dima'

--3. ��������� ������� Orders � ������� OrdersArchive.

SELECT * INTO
	Northwind.dbo.OrdersArchive
FROM
	Northwind.dbo.Orders;



--4. �������� ������� OrdersArchive.

DELETE FROM Northwind.dbo.OrdersArchive

--5. �� ��������� ������� OrdersArchive, ��������� �� ����������� ��������.


INSERT INTO Northwind.dbo.OrdersArchive
	("OrderDate",
	"ShipAddress",
	"ShipCountry")
SELECT 
	"OrderDate",
	"ShipAddress",
	"ShipCountry"
FROM 
	Northwind.dbo.Orders


--6. � ������� OrdersArchive �������� �� ����������, �� ���� ������� ����������� �� ������.

DELETE FROM Northwind.dbo.OrdersArchive
WHERE "ShipCountry" = 'Germany';



--7. ������ � ���� ��� �������� � ������� ������ �� ������ �����.

INSERT INTO Northwind.dbo.Categories 
	("CategoryName",
	"Description")
VALUES
	('Food',
	'We can eat');


INSERT INTO Northwind.dbo.Products
	("ProductName",
	"CategoryID")
VALUES
	('BigMac',
	(SELECT TOP(1) "CategoryID" FROM Northwind.dbo.Categories
	WHERE "CategoryName" = 'Food'))

INSERT INTO Northwind.dbo.Products
	("ProductName",
	"CategoryID")
VALUES
	('Apple',
	(SELECT TOP(1) "CategoryID" FROM Northwind.dbo.Categories
	WHERE "CategoryName" = 'Food'))


--8. ������� ��������, �� �� ��������� � �����������, �� ���, �� ����� �� ������������.

UPDATE Northwind.dbo.Products
SET "ProductName" = 'Discontinued'
WHERE "ProductID" <> ANY(SELECT "ProductID" FROM Northwind.dbo.[Order Details]);


--9. �������� ������� OrdersArchive.

DROP TABLE IF EXISTS Northwind.dbo.OrdersArchive;

--10. �������� ���� Northwind.

DROP DATABASE IF EXISTS Northwind