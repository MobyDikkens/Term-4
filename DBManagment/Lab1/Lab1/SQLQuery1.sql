--1. ������� �� ��������� ������� SELECT ��� �������, ��� �� ��-������� �� �����.

SELECT 'Sheludko' AS "LastName", 'Dima' AS "FirstName", 'Maksimovich' AS "Fathername";

--2. ������� �� ��� � ������� Products.

SELECT * FROM Northwind.dbo.Products;

--3. ������ �� ����� �������� � 򳺿 � �������, ������ ���� ���������.

SELECT * FROM Northwind.dbo.Products WHERE "Discontinued" = 1;

--4. ������� �� ���� �볺��� �������� ��������.

SELECT DISTINCT "City" FROM Northwind.dbo.Customers;

--5. ������� �� ����� �������-������������� � ������� ����������� ����������.

SELECT "CompanyName" FROM Northwind.dbo.Suppliers ORDER BY "CompanyName" DESC;

--6. �������� �� ����� ���������, �������� ����� ��������� �� �� ���������� �����.????????

SELECT "OrderId" AS "1", "ProductId" AS "2", "UnitPrice" AS "3", "Quantity" AS "4", "Discount" AS "5" FROM Northwind.dbo.[Order Details];

--7. ������� �� �������� ����� �볺���, �� ����������� � ����� ����� ������ �������, ����,
--��-�������.

SELECT "ContactName" FROM Northwind.dbo.Customers WHERE "ContactName" LIKE 'D% S%';

--8. �������� �� ����������, � ������� �������� ���� � �����.

SELECT "ShipAddress" FROM Northwind.dbo.Orders WHERE "ShipAddress" LIKE '% %';

--9. ������� ����� ��� ��������, �� ����������� �� ���� % ��� _, � ����������� �� �������
--����� ������ ����.

SELECT "ProductName" FROM Northwind.dbo.Products WHERE "ProductName" LIKE '\%%a' OR "ProductName" LIKE '\_%a';