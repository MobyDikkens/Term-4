--1. �������� ��������� ���������, �� ��� ������� ���� ��������� ���� �������, ��� �� ��-
--�������.

GO
CREATE OR ALTER FUNCTION initials() RETURNS CHAR(50)
	BEGIN
	DECLARE @PIB CHAR(50) = 'Sheludko Dmitriy Maksimovich'
	RETURN @PIB
	END
GO

DECLARE @res CHAR(50)
EXECUTE @res = initials;
SELECT @res;

--2. � ������� ���� Northwind �������� ��������� ���������, �� ������ ��������� ��������
--�������� �������. � ��� ������� ��������� � ���������� �F� �� ����� ���������� ��
--�����������-����, � ��� ������������ ��������� �M� � ������. � ������������ �������
--������� �� ����� ����������� ��� ��, �� �������� �� ���������.


USE Northwind
GO
CREATE OR ALTER PROCEDURE sex_filter
	@sex CHAR(1) 
AS
BEGIN
	IF(@sex = 'F')
		SELECT * FROM Northwind.dbo.Employees
		WHERE TitleOfCourtesy = 'Ms.'
		OR TitleOfCourtesy = 'Mrs.'
	ELSE
		BEGIN
			IF(@sex = 'M')
				SELECT * FROM Northwind.dbo.Employees
				WHERE TitleOfCourtesy <> 'Ms.'
				AND TitleOfCourtesy <> 'Mrs.'
			ELSE
				SELECT 'Unknown' AS 'Result'			
			 
		END
END

EXECUTE sex_filter @sex = 'M'


--3. � ������� ���� Northwind �������� ��������� ���������, �� �������� �� ���������� ��
--������� �����. � ���� ���, ���� ����� �� ������ � ������� ���������� �� �������� ����.


USE Northwind
GO
CREATE OR ALTER PROCEDURE oredrs_per_period
	@start CHAR(10) = '1990-01-01',
	@end CHAR(10) = '2000-01-01'
AS
BEGIN

	IF(@end IS NOT NULL AND @start IS NOT NULL)
		SELECT * FROM Northwind.dbo.Orders
		WHERE OrderDate < CAST(@end AS DATETIME)
		AND OrderDate > CAST(@start AS DATETIME)
END

EXECUTE oredrs_per_period '1997-01-01'


--4. � ������� ���� Northwind �������� ��������� ���������, �� � ��������� �� ����������
--��������� ������� �������� �������� �� ������ ��� �������� �� ���� ��������.
--��������� ��������� ����������� �� ���� �� ���� ��������.


USE Northwind
GO 
CREATE OR ALTER PROCEDURE get_products_by_category
	@first CHAR(21) = 'Food',
	@second CHAR(21) = 'Seafood',
	@third CHAR(21) = 'Produce',
	@forth CHAR(21) = 'Confections',
	@fifth CHAR(21) = 'Beverages'
AS
BEGIN
	SELECT CategoryName, ProductName FROM Northwind.dbo.Categories AS C
	JOIN Northwind.dbo.Products AS P 
	ON P.CategoryID = C.CategoryID
	WHERE CategoryName = @first
	OR CategoryName = @second
	OR CategoryName = @third
	OR CategoryName = @forth
	OR CategoryName = @fifth
END 



--5. � ������� ���� Northwind ������������ ��������� ��������� Ten Most Expensive Products
--��� ������ �񳺿 ���������� � ������� ��������, � ����� ���� ������������� �� �����
--��������.

USE Northwind
GO
ALTER PROCEDURE [dbo].[Ten Most Expensive Products] 
AS
	SET ROWCOUNT 10
	SELECT *,ContactName, CategoryName FROM Products AS P
	JOIN Northwind.dbo.Suppliers AS S
	ON S.SupplierID = P.SupplierID
	JOIN Northwind.dbo.Categories AS C
	ON C.CategoryID = p.CategoryID
	ORDER BY UnitPrice DESC

EXECUTE [Ten Most Expensive Products]



--6. � ������� ���� Northwind �������� �������, �� ������ ��� ��������� (TitleOfCourtesy,
--FirstName, LastName) �� �������� �� ������ �������.
--�������: �Dr.�, �Yevhen�, �Nedashkivskyi� �&gt; �Dr. Yevhen Nedashkivskyi�


USE Northwind
GO 
CREATE OR ALTER PROCEDURE [Concat Status PIB]
	@TitleOfCourtesy CHAR(21),
	@FirstName CHAR(21),
	@LastName CHAR(21)
AS
	SELECT CONCAT(@TitleOfCourtesy,@FirstName,@LastName)

EXECUTE [Concat Status PIB] 'President', 'Dima', 'Sheludko'

--7. � ������� ���� Northwind �������� �������, �� ������ ��� ��������� (UnitPrice, Quantity,
--Discount) �� �������� ������ ����.

USE Northwind
GO
CREATE OR ALTER PROCEDURE [Calculate Cost]
	@UnitPrice INT = 0,
	@Quantity INT = 0,
	@Discount FLOAT(2) = 0
AS
	SELECT @UnitPrice * @Quantity * @Discount

EXECUTE [Calculate Cost] 1,2,0.1

--8. �������� �������, �� ������ �������� ���������� ���� � ��������� ���� �� Pascal Case.
--�������: ̳� ��������� ��� �&gt; ̳�������������

GO
CREATE FUNCTION dbo.PascalCase(@value VARCHAR(100))
RETURNS VARCHAR(100)
AS
BEGIN
  DECLARE @ptr INT

  SELECT @value = STUFF(LOWER(@value), 1, 1, UPPER(LEFT(@value, 1))),
         @ptr = PATINDEX('%[^a-zA-Z][a-z]%', @value COLLATE Latin1_General_Bin)

  WHILE @ptr > 0
    SELECT @value = STUFF(@value, @ptr, 2, UPPER(SUBSTRING(@value, @ptr, 2))),
           @ptr = PATINDEX('%[^a-zA-Z][a-z]%', @value COLLATE Latin1_General_Bin)

  RETURN @value
END

DECLARE @r CHAR(100)
EXECUTE @r = dbo.PascalCase 'Hello world asd'
SELECT @r



--9. � ������� ���� Northwind �������� �������, �� � ��������� �� ������� �����, ������� ��
--��� ��� ����������� � ������ �������.


USE Northwind
GO
CREATE OR ALTER FUNCTION empl_by_country(@country CHAR(21))
	 RETURNS @empltbl TABLE(
	  [EmployeeID] INT
      ,[LastName] NVARCHAR(60)
      ,[FirstName] NVARCHAR(60)
      ,[Title] NVARCHAR(60)
      ,[TitleOfCourtesy] NVARCHAR(60)
      ,[BirthDate] DATETIME
      ,[HireDate] DATETIME
      ,[Address] NVARCHAR(60)
      ,[City] NVARCHAR(60)
      ,[Region] NVARCHAR(60)
      ,[PostalCode] NVARCHAR(60)
      ,[Country] NVARCHAR(60)
      ,[HomePhone] NVARCHAR(60)
      ,[Extension] NVARCHAR(60)
      ,[Photo] IMAGE
      ,[Notes] ntext
      ,[ReportsTo] INT
      ,[PhotoPath] nvarchar(255)
      ,[Salary] decimal(18, 2)
	  )
AS
BEGIN

	INSERT INTO @empltbl SELECT * FROM Northwind.dbo.Employees
	WHERE Employees.Country = @country;
	RETURN
END
GO
SELECT * FROM empl_by_country('UK')

--10. � ������� ���� Northwind �������� �������, �� � ��������� �� ���� ����������� ������
--������� ������ �볺���, ���� ���� ��������������.
USE Northwind
GO
CREATE OR ALTER FUNCTION fres(@CompanyTitle CHAR(21)) 
RETURNS @tblres TABLE(ID NCHAR(100)) 
AS
BEGIN

	INSERT INTO @tblres 
	SELECT CustomerID 
	FROM Orders AS O
	JOIN shippers AS S
	ON O."ShipVia" = S."ShipperID"
	WHERE "CompanyName" = @CompanyTitle
	RETURN
END
GO


SELECT * FROM fres('Speedy Express')

