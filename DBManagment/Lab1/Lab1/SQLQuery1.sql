--1. Вивести за допомогою команди SELECT своє прізвище, ім’я та по-батькові на екран.

SELECT 'Sheludko' AS "LastName", 'Dima' AS "FirstName", 'Maksimovich' AS "Fathername";

--2. Вибрати всі дані з таблиці Products.

SELECT * FROM Northwind.dbo.Products;

--3. Обрати всі назви продуктів з тієї ж таблиці, продаж яких припинено.

SELECT * FROM Northwind.dbo.Products WHERE "Discontinued" = 1;

--4. Вивести всі міста клієнтів уникаючи дублікатів.

SELECT DISTINCT "City" FROM Northwind.dbo.Customers;

--5. Вибрати всі назви компаній-постачальників в порядку зворотньому алфавітному.

SELECT "CompanyName" FROM Northwind.dbo.Suppliers ORDER BY "CompanyName" DESC;

--6. Отримати всі деталі замовлень, замінивши назви стовбчиків на їх порядковий номер.????????

SELECT "OrderId" AS "1", "ProductId" AS "2", "UnitPrice" AS "3", "Quantity" AS "4", "Discount" AS "5" FROM Northwind.dbo.[Order Details];

--7. Вивести всі контактні імена клієнтів, що починаються з першої літери вашого прізвища, імені,
--по-батькові.

SELECT "ContactName" FROM Northwind.dbo.Customers WHERE "ContactName" LIKE 'D% S%';

--8. Показати усі замовлення, в адресах доставки яких є пробіл.

SELECT "ShipAddress" FROM Northwind.dbo.Orders WHERE "ShipAddress" LIKE '% %';

--9. Вивести назви тих продуктів, що починаються на знак % або _, а закінчуються на останню
--літеру вашого імені.

SELECT "ProductName" FROM Northwind.dbo.Products WHERE "ProductName" LIKE '\%%a' OR "ProductName" LIKE '\_%a';