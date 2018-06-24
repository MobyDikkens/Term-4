--Задача №1:
--Виконати наступні завдання:

--1. Використовуючи SELECT двічі, виведіть на екран своє ім’я, прізвище та по-батькові одним
--результуючим набором.


SELECT 
'Dima' AS "Name",
'Sheludko' AS "Surname",
'Maksimovich' AS "Patronymic"

EXCEPT

SELECT
'Victor' AS "Name",
'Pavlik' AS "Surname",
'Valentinovich' AS "Patronymic";

--2. Порівнявши власний порядковий номер в групі з набором із всіх номерів в групі, вивести на
--екран ;-) якщо він менший за усі з них, або :-D в протилежному випадку.


SELECT * FROM 
(SELECT "Name",
CASE
	WHEN	
		(SELECT CAST("UserID" AS INT) FROM IP63.dbo.Users WHERE "Name" = 'Dima')  - 
		(SELECT MAX(CAST("UserID" AS INT)) FROM IP63.dbo.Users) = 0
		THEN ';-)'
	WHEN 
		(SELECT CAST("UserID" AS INT) FROM IP63.dbo.Users WHERE "Name" = 'Dima') - 
		(SELECT MIN(CAST("UserID" AS INT)) FROM IP63.dbo.Users) = 0
		THEN ':-D'
	ELSE NULL
END AS "Result"
FROM IP63.dbo.Users) AS Res
WHERE Res.Result IS NOT NULL;




--3. Не використовуючи таблиці, вивести на екран прізвище та ім’я усіх дівчат своєї групи за
--вийнятком тих, хто має спільне ім’я з студентками іншої групи.

SELECT * FROM 
	(SELECT
		'Vika' AS "FirstName",
		'Bondar' AS "Surname"
	UNION

	SELECT
		'Luda' AS "FirstName",
		'Koroleva' AS "Surname"
	UNION

	SELECT
		'Vera' AS "FirstName",
		'Popova' AS "Surname") AS Res
WHERE Res.FirstName NOT IN ('Valia', 'Vika');

	



--4. Вивести усі рядки з таблиці Numbers (Number INT). Замінити цифру від 0 до 9 на її назву
--літерами. Якщо цифра більше, або менша за названі, залишити її без змін.


SELECT "Number",
	CASE
		WHEN "Number" = 0
			THEN 'Zero'
		WHEN "Number" = 1
			THEN 'One'
		WHEN "Number" = 2
			THEN 'Two'
		WHEN "Number" = 3
			THEN 'Three'
		WHEN "Number" = 4
			THEN 'Four'
		WHEN "Number" = 5
			THEN 'Five'
		WHEN "Number" = 6
			THEN 'Six'
		WHEN "Number" = 7
			THEN 'Seven'
		WHEN "Number" = 8
			THEN 'Eight'
		WHEN "Number" = 9
			THEN 'Nine'

	END
FROM IP63.dbo.Numbers;


--5. Навести приклад синтаксису декартового об’єднання для вашої СУБД.

SELECT "Name", "Surname", "Number" FROM IP63.dbo.Users
CROSS JOIN IP63.dbo.Numbers;



--Задача №2:
--Виконати наступні завдання в контексті бази Northwind:

--1. Вивисти усі замовлення та їх службу доставки. В залежності від ідентифікатора служби
--доставки, переіменувати її на таку, що відповідає вашому імені, прізвищу, або по-батькові.

SELECT "OrderID",
CASE
	WHEN "ShipVia" = 1
		THEN 'Dima'
	WHEN "ShipVia" = 2
		THEN 'Sheludko'
	WHEN "ShipVia" = 3
		THEN 'Maksimovich'
END "ShipVia"
FROM Northwind.dbo.Orders;

--2. Вивести в алфавітному порядку усі країни, що фігурують в адресах клієнтів, працівників, та
--місцях доставки замовлень.


(SELECT 
	"ShipCountry" AS "Country" 
FROM 
	Northwind.dbo.Orders)
UNION

(SELECT 
	"Country" 
FROM 
	Northwind.dbo.Employees)
UNION

(SELECT 
	"Country" 
FROM 
	Northwind.dbo.Suppliers)

ORDER BY 
	"Country"



--3. Вивести прізвище та ім’я працівника, а також кількість замовлень, що він обробив за перший
--квартал 1998 року.


SELECT 
	"LastName",
	"FirstName", 
	COUNT("OrderID") AS "Count" 
FROM 
	Northwind.dbo.Employees
JOIN 
	Northwind.dbo.Orders AS O 
ON 
	O.EmployeeID = Northwind.dbo.Employees.EmployeeID
WHERE 
	YEAR("OrderDate") = 1998
AND 
	MONTH("OrderDate") <= 3
GROUP BY 
	"LastName",
	"FirstName";


--4. Використовуючи СTE знайти усі замовлення, в які входять продукти, яких на складі більше 100
--одиниць, проте по яким немає максимальних знижок.

WITH 
	products 
AS 
   (
		SELECT
			"OrderID",
			"Quantity"
		FROM 
			Northwind.dbo.[Order Details]
		WHERE 
			"Discount" <> (SELECT MAX("Discount") FROM Northwind.dbo.[Order Details])
	)
SELECT 
	* 
FROM 
	products
WHERE 
	products."Quantity" > 100


--5. Знайти назви усіх продуктів, що не продаються в південному регіоні.

SELECT 
	"ProductName"
FROM
	Northwind.dbo.Products AS P
JOIN
	Northwind.dbo.[Order Details] AS OD
ON
	OD.ProductID = P.ProductID
JOIN
	Northwind.dbo.Orders AS O
ON
	O.OrderID = OD.OrderID
JOIN
	Northwind.dbo.Employees AS E
ON
	E.EmployeeID = O.EmployeeID
JOIN
	Northwind.dbo.EmployeeTerritories AS ET
ON 
	ET.EmployeeID = E.EmployeeID
JOIN
	Northwind.dbo.Territories AS T
ON
	T.TerritoryID = ET.TerritoryID
JOIN
	Northwind.dbo.Region AS R
ON
	R.RegionID = T.RegionID
WHERE
	R.RegionDescription <> 'Sourthen';
