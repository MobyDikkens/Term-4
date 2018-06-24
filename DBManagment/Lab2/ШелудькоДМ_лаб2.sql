--������ �1:
--�������� ������� ��������:

--1. ��������� ������ ������� ����� � �������, �� ������ ����� ��2147483647�������.
--�������� ��� ����MS�SQL�Server��� �� ���� ���� (�� ������� ����).

SELECT COUNT_BIG(*) FROM Northwind.dbo.Categories;

--2. ϳ��������� ������� ����� ������� �� ��������� SQL.

SELECT LEN('Sheludko') AS "SurnameLenth";

--3. � ����� � ���� ��������, ������, �� ��-������� ������� ������ �� ���� �_� (����
--�����������).

SELECT REPLACE('Sheludko Dmitro Maksimovich', ' ', '_') AS "MyInfo";

--4. �������� ��������� ���� ���������� ������� ��������, �� ������ ������������
--�ᒺ������ �� �� ����� ����� � ������� ����, �� ������ ����� ����� � ������� �������
--�����������, �� ����������� � ��� �����, � ����� ������ � ����� ��������.


SELECT  REPLACE(CONCAT(LEFT("Name", 2), LEFT("Surname", 4),  '@gmail.com'), ' ', '') AS "Email" 
FROM IP63.dbo.Users;


--5. �� ��������� SQL ���������, � ���� ���� ������ �� ����������.


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



--������ �2:
--�������� ������� �������� � �������� ���� Northwind:

--1. ������� �� ���� �� ���������, �� ���������, �� ��������������, ����� ���� ������ �
--������ ������ ������.

USE Northwind
SELECT * FROM Products
LEFT JOIN Categories AS C 
	ON Products.CategoryID = C.CategoryID
LEFT JOIN Suppliers AS S 
	ON Products.SupplierID = S.SupplierID;



--2. �������� �� ����������, �� ���� ������� � ���� 1988 ���� �� �� ���� ���������.

SELECT * FROM Northwind.dbo.Orders
WHERE 
YEAR("OrderDate") = 1988
AND
MONTH("OrderDate") = 4
AND
"ShipRegion" IS NULL;
--if ShipRegion == NULL, we dont have enought data to make a transer

--3. ³������ ��� ����������, �� ���������� �� ������� �����.
--Northern                                          

SELECT "FirstName", "LastName" FROM
Northwind.dbo.EmployeeTerritories AS ET
JOIN Northwind.dbo.Employees AS E
ON E.EmployeeID = ET.EmployeeID
JOIN Northwind.dbo.Territories AS T
ON T.TerritoryID = ET.TerritoryID
JOIN Northwind.dbo.Region AS R
ON R.RegionDescription = 'Northern';


--4. ���������� �������� ������� � ����������� ������ ��� ���������, �� ���� ������� ��
--������� ����.


SELECT SUM("UnitPrice" * "Quantity" - "UnitPrice" * "Quantity" * "Discount") AS RES
FROM Northwind.dbo.[Order Details] AS OD
JOIN Northwind.dbo.Orders AS O ON O.OrderID = OD.OrderID
WHERE
CAST(DATEPART(dw,"OrderDate") AS INT) % 2 = 1;


--5. ������ ������ ����������� ���������� � ��������� ����� (���������� �� �������
--����������, �� �������, �������, �� �������� ������).


SELECT TOP(1) MAX("UnitPrice" * "Quantity" - "UnitPrice" * "Quantity" * "Discount") AS Max,
"ShipAddress"
FROM Northwind.dbo.[Order Details] AS OD
JOIN Northwind.dbo.Orders AS O ON O.OrderID = OD.OrderID
GROUP BY "ShipAddress"
ORDER BY 'Max' DESC;
