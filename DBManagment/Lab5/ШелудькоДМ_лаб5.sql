--1. �������� ���� ����� � ����, �� ������� ������ ������� ���������� �����.

IF NOT EXISTS(	SELECT [name] 
				FROM sys.databases
				WHERE [name] = 'Sheludko')
CREATE DATABASE Sheludko;

--2. �������� � ���� ��� ������� Student � ���������� StudentId, SecondName, FirstName, Sex.
--������ ��� ��� ����������� ��� ����� � ����� ����.

IF NOT EXISTS(	SELECT [name]
				FROM sys.tables
				WHERE [name] = 'Student')

CREATE TABLE Sheludko.dbo.Student(
	StudentID INT NOT NULL,
	SecondName VARCHAR(255),
	FirstName VARCHAR(255),
	Sex CHAR(1) DEFAULT 'm');

	

--3. ������������ ������� Student. ������� StudentId �� ����� ��������� ������.

--?????????????????????????????????
--Change it, it works 1/10 times
ALTER TABLE Sheludko.dbo.Student 
ADD PRIMARY KEY(StudentID);

--4. ������������ ������� Student. ������� StudentId ������� ������������� �����������
--��������� � 1 � ������ � 1.


ALTER TABLE Sheludko.dbo.Student 
DROP COLUMN StudentID;

ALTER TABLE Sheludko.dbo.Student 
ADD StudentID INT IDENTITY(1,1);


--5. ������������ ������� Student. ������ ������������� ������� BirthDate �� ���������
--����� �����.

ALTER TABLE Sheludko.dbo.Student
ADD BirthDay DATE DEFAULT SYSDATETIME()

--6. ������������ ������� Student. ������ ������� CurrentAge, �� ���������� ����������� ��
--��� �������� � ������� �����.

ALTER TABLE Sheludko.dbo.Student
ADD CurrentAge AS (YEAR(SYSDATETIME()) - YEAR(BirthDay))

--7. ���������� �������� ���������� �����. �������� �������� Sex ���� ���� ����� �m� �� �f�.

ALTER TABLE Sheludko.dbo.Student
ADD CHECK(Sex = 'm' OR Sex='f')

--8. � ������� Student ������ ���� �� ���� ������ � ������ �����.

INSERT INTO Sheludko.dbo.Student(FirstName, SecondName, Sex)
VALUES('Dima', 'Sheludko', 'm')

INSERT INTO Sheludko.dbo.Student(FirstName, SecondName, Sex)
VALUES('Mihail', 'Sitnik', 'm')

INSERT INTO Sheludko.dbo.Student(FirstName, SecondName, Sex)
VALUES('Vitalik', 'Yrchishin', 'm')


--9. �������� ������������� vMaleStudent �� vFemaleStudent, �� ������� ��������
--����������.

CREATE VIEW vMaleStudent AS
	SELECT FirstName, SecondName, BirthDay FROM Sheludko.dbo.Student
	WHERE Sex = 'm';

CREATE VIEW vFemaleStudent AS
	SELECT FirstName, SecondName, BirthDay FROM Sheludko.dbo.Student
	WHERE Sex = 'f';

--10. ������ ��� ����� ���������� ����� �� TinyInt (��� SmallInt) �� ��������� ���.


ALTER TABLE Sheludko.dbo.Student
ALTER COLUMN StudentID TINYINT

