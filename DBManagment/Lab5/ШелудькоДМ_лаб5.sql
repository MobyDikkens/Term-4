--1. Створити базу даних з ім’ям, що відповідає вашому прізвищу англійською мовою.

IF NOT EXISTS(	SELECT [name] 
				FROM sys.databases
				WHERE [name] = 'Sheludko')
CREATE DATABASE Sheludko;

--2. Створити в новій базі таблицю Student з атрибутами StudentId, SecondName, FirstName, Sex.
--Обрати для них оптимальний тип даних в вашій СУБД.

IF NOT EXISTS(	SELECT [name]
				FROM sys.tables
				WHERE [name] = 'Student')

CREATE TABLE Sheludko.dbo.Student(
	StudentID INT NOT NULL,
	SecondName VARCHAR(255),
	FirstName VARCHAR(255),
	Sex CHAR(1) DEFAULT 'm');

	

--3. Модифікувати таблицю Student. Атрибут StudentId має стати первинним ключем.

--?????????????????????????????????
--Change it, it works 1/10 times
ALTER TABLE Sheludko.dbo.Student 
ADD PRIMARY KEY(StudentID);

--4. Модифікувати таблицю Student. Атрибут StudentId повинен заповнюватися автоматично
--починаючи з 1 і кроком в 1.


ALTER TABLE Sheludko.dbo.Student 
DROP COLUMN StudentID;

ALTER TABLE Sheludko.dbo.Student 
ADD StudentID INT IDENTITY(1,1);


--5. Модифікувати таблицю Student. Додати необов’язковий атрибут BirthDate за відповідним
--типом даних.

ALTER TABLE Sheludko.dbo.Student
ADD BirthDay DATE DEFAULT SYSDATETIME()

--6. Модифікувати таблицю Student. Додати атрибут CurrentAge, що генерується автоматично на
--базі існуючих в таблиці даних.

ALTER TABLE Sheludko.dbo.Student
ADD CurrentAge AS (YEAR(SYSDATETIME()) - YEAR(BirthDay))

--7. Реалізувати перевірку вставлення даних. Значення атрибуту Sex може бути тільки ‘m’ та ‘f’.

ALTER TABLE Sheludko.dbo.Student
ADD CHECK(Sex = 'm' OR Sex='f')

--8. В таблицю Student додати себе та двох «сусідів» у списку групи.

INSERT INTO Sheludko.dbo.Student(FirstName, SecondName, Sex)
VALUES('Dima', 'Sheludko', 'm')

INSERT INTO Sheludko.dbo.Student(FirstName, SecondName, Sex)
VALUES('Mihail', 'Sitnik', 'm')

INSERT INTO Sheludko.dbo.Student(FirstName, SecondName, Sex)
VALUES('Vitalik', 'Yrchishin', 'm')


--9. Створити представлення vMaleStudent та vFemaleStudent, що надають відповідну
--інформацію.

CREATE VIEW vMaleStudent AS
	SELECT FirstName, SecondName, BirthDay FROM Sheludko.dbo.Student
	WHERE Sex = 'm';

CREATE VIEW vFemaleStudent AS
	SELECT FirstName, SecondName, BirthDay FROM Sheludko.dbo.Student
	WHERE Sex = 'f';

--10. Змінити тип даних первинного ключа на TinyInt (або SmallInt) не втрачаючи дані.


ALTER TABLE Sheludko.dbo.Student
ALTER COLUMN StudentID TINYINT

