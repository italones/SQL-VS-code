-- Задание 1: Создание базы данных
CREATE DATABASE CollegeDB;
GO

USE CollegeDB;
GO

-- Задание 2: Создание таблиц
CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    GroupId INT
);

CREATE TABLE Subjects (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100)
);

CREATE TABLE Grades (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT,
    SubjectId INT,
    Grade INT,
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id)
);

CREATE TABLE Teachers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100)
);

CREATE TABLE Groups (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50)
);

-- Добавим немного тестовых данных
INSERT INTO Groups (Name) VALUES ('PR-21'), ('PR-22');
INSERT INTO Students (FirstName, LastName, GroupId) VALUES ('Ivan', 'Ivanov', 1), ('Petr', 'Petrov', 2);
INSERT INTO Subjects (Name) VALUES ('Math'), ('History');
INSERT INTO Grades (StudentId, SubjectId, Grade) VALUES (1, 1, 5), (1, 2, 4), (2, 1, 3);