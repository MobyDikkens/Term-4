--Виконати в контексті бази Northwind:

--1. Додати себе як співробітника компанії на позицію Intern.


INSERT INTO Northwind.dbo.Employees
	(LastName,
	FirstName,
	Title)
VALUES 
	('Sheludko',
	 'Dima',
	 'Intern');

--2. Змінити свою посаду на Director.

UPDATE 
	Northwind.dbo.Employees
SET
	Title = 'Director'
WHERE 
	LastName = 'Sheludko'
AND 
	FirstName = 'Dima'

--3. Скопіювати таблицю Orders в таблицю OrdersArchive.

SELECT * INTO
	Northwind.dbo.OrdersArchive
FROM
	Northwind.dbo.Orders;



--4. Очистити таблицю OrdersArchive.

DELETE FROM Northwind.dbo.OrdersArchive

--5. Не видаляючи таблицю OrdersArchive, наповнити її інформацією повторно.


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


--6. З таблиці OrdersArchive видалити усі замовлення, що були зроблені замовниками із Берліну.

DELETE FROM Northwind.dbo.OrdersArchive
WHERE "ShipCountry" = 'Germany';



--7. Внести в базу два продукти з власним іменем та іменем групи.

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


--8. Помітити продукти, що не фігурують в замовленнях, як такі, що більше не виробляються.

UPDATE Northwind.dbo.Products
SET "ProductName" = 'Discontinued'
WHERE "ProductID" <> ANY(SELECT "ProductID" FROM Northwind.dbo.[Order Details]);


--9. Видалити таблицю OrdersArchive.

DROP TABLE IF EXISTS Northwind.dbo.OrdersArchive;

--10. Видатили базу Northwind.

DROP DATABASE IF EXISTS Northwind