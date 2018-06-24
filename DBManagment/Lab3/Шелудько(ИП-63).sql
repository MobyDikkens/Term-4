--������ �1:
--�������� ������� ��������:

--1. �������������� SELECT ����, ������� �� ����� ��� ���, ������� �� ��-������� �����
--������������ �������.


SELECT 
'Dima' AS "Name",
'Sheludko' AS "Surname",
'Maksimovich' AS "Patronymic"

EXCEPT

SELECT
'Victor' AS "Name",
'Pavlik' AS "Surname",
'Valentinovich' AS "Patronymic";

--2. ��������� ������� ���������� ����� � ���� � ������� �� ��� ������ � ����, ������� ��
--����� ;-) ���� �� ������ �� �� � ���, ��� :-D � ������������ �������.


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




--3. �� �������������� �������, ������� �� ����� ������� �� ��� ��� ����� �� ����� ��
--��������� ���, ��� �� ������ ��� � ����������� ���� �����.

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

	



--4. ������� �� ����� � ������� Numbers (Number INT). ������� ����� �� 0 �� 9 �� �� �����
--�������. ���� ����� �����, ��� ����� �� ������, �������� �� ��� ���.


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


--5. ������� ������� ���������� ����������� �ᒺ������ ��� ���� ����.

SELECT "Name", "Surname", "Number" FROM IP63.dbo.Users
CROSS JOIN IP63.dbo.Numbers;



--������ �2:
--�������� ������� �������� � �������� ���� Northwind:

--1. ������� �� ���������� �� �� ������ ��������. � ��������� �� �������������� ������
--��������, ������������ �� �� ����, �� ������� ������ ����, �������, ��� ��-�������.

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

--2. ������� � ���������� ������� �� �����, �� ��������� � ������� �볺���, ����������, ��
--����� �������� ���������.


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



--3. ������� ������� �� ��� ����������, � ����� ������� ���������, �� �� ������� �� ������
--������� 1998 ����.


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


--4. �������������� �TE ������ �� ����������, � �� ������� ��������, ���� �� ����� ����� 100
--�������, ����� �� ���� ���� ������������ ������.

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


--5. ������ ����� ��� ��������, �� �� ���������� � ��������� �����.

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
