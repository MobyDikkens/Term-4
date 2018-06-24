--Задача №1:
--Виконати наступні завдання:

--1. Необхідно знайти кількість рядків в таблиці, що містить більше ніж 2147483647 записів.
--Напишіть код для MS SQL Server та ще однієї СУБД (на власний вибір).

SELECT COUNT_BIG(*) FROM Northwind.dbo.Categories;

--2. Підрахувати довжину свого прізвища за допомогою SQL.

SELECT LEN('Sheludko') AS "SurnameLenth";

--3. У рядку з своїм прізвищем, іменем, та по-батькові замінити пробіли на знак ‘_’ (нижнє
--підкреслення).

SELECT REPLACE('Sheludko Dmitro Maksimovich', ' ', '_') AS "MyInfo";

--4. Створити генератор імені електронної поштової скриньки, що шляхом конкатенації
--об’єднував би дві перші літери з колонки імені, та чотири перші літери з колонки прізвища
--користувача, що зберігаються в базі даних, а також домену з вашим прізвищем.


SELECT  REPLACE(CONCAT(LEFT("Name", 2), LEFT("Surname", 4),  '@gmail.com'), ' ', '') AS "Email" 
FROM IP63.dbo.Users;


--5. За допомогою SQL визначити, в який день тиждня ви народилися.


SELECT *,
CASE
	WHEN DATEPART(dw,IP63.dbo.Users.Birthday) = 1
	THEN 'Monday'
	WHEN DATEPART(dw,IP63.dbo.Users.Birthday) = 2
	THEN 'Tuesday'
	WHEN DATEPART(dw,IP63.dbo.Users.Birthday) = 3
	THEN 'Wednesday'
	WHEN DATEPART(dw,IP63.dbo.Users.Birthday) = 4
	THEN 'Thursday'
	WHEN DATEPART(dw,IP63.dbo.Users.Birthday) = 5
	THEN 'Friday'
	WHEN DATEPART(dw,IP63.dbo.Users.Birthday) = 6
	THEN 'Saturday'
	WHEN DATEPART(dw,IP63.dbo.Users.Birthday) = 7
	THEN 'Sunday'
END
FROM IP63.dbo.Users;



--Задача №2:
--Виконати наступні завдання в контексті бази Northwind:

--1. Вивести усі данні по продуктам, їх категоріям, та постачальникам, навіть якщо останні з
--певних причин відсутні.

USE Northwind
SELECT * FROM Products
LEFT JOIN Categories AS C 
	ON Products.CategoryID = C.CategoryID
LEFT JOIN Suppliers AS S 
	ON Products.SupplierID = S.SupplierID;



--2. Показати усі замовлення, що були зроблені в квітні 1988 року та не були відправлені.

SELECT * FROM Northwind.dbo.Orders
WHERE 
YEAR("OrderDate") = 1988
AND
MONTH("OrderDate") = 4
AND
"ShipRegion" IS NULL;
--if ShipRegion == NULL, we dont have enought data to make a transer

--3. Відібрати усіх працівників, що відповідають за північний регіон.
--Northern                                          

SELECT "FirstName", "LastName" FROM
Northwind.dbo.EmployeeTerritories AS ET
JOIN Northwind.dbo.Employees AS E
ON E.EmployeeID = ET.EmployeeID
JOIN Northwind.dbo.Territories AS T
ON T.TerritoryID = ET.TerritoryID
JOIN Northwind.dbo.Region AS R
ON R.RegionDescription = 'Northern';


--4. Вирахувати загальну вартість з урахуванням знижки усіх замовлень, що були здійснені на
--непарну дату.


SELECT SUM("UnitPrice" * "Quantity" - "UnitPrice" * "Quantity" * "Discount") AS RES
FROM Northwind.dbo.[Order Details] AS OD
JOIN Northwind.dbo.Orders AS O ON O.OrderID = OD.OrderID
WHERE
CAST(DATEPART(dw,"OrderDate") AS INT) % 2 = 1;


--5. Знайти адресу відправлення замовлення з найбільшою ціною (враховуючи усі позиції
--замовлення, їх вартість, кількість, та наявність знижки).


SELECT TOP(1) MAX("UnitPrice" * "Quantity" - "UnitPrice" * "Quantity" * "Discount") AS Max,
"ShipAddress"
FROM Northwind.dbo.[Order Details] AS OD
JOIN Northwind.dbo.Orders AS O ON O.OrderID = OD.OrderID
GROUP BY "ShipAddress"
ORDER BY 'Max' DESC;
