-- Zad 1
CREATE TABLE Studenci(
    ID INT NOT NULL PRIMARY KEY,
    Name NCHAR(31),
    LastName NCHAR(31) NOT NULL,
    BirthDate DATE NOT NULL,
    AverageGrade NUMERIC(3,2) CHECK(AverageGrade >= 2.0 AND AverageGrade <= 5.5),
    Sex CHAR(1) NOT NULL CHECK(Sex IN ('M', 'F')),
    StudyYear INT NOT NULL CHECK(StudyYear >= 1),
    FieldOfStudy NCHAR(31) NOT NULL
);
-- Zad 2 
INSERT INTO Studenci VALUES (241308, 'Marek', 'Machliñski', '15-MAY-05', 5.0, 'M', 2, 'Computer Science');
INSERT INTO Studenci VALUES (241309, 'Diana', 'Eleonore', '09-FEB-16', 5.0, 'F', 2, 'Anatony');
INSERT INTO Studenci VALUES (241310, 'Britanny', 'Larkin', '94-DEC-25', 4.0, 'F', 5, 'Chemistry');
-- Zad 3
UPDATE Studenci
SET AverageGrade = 4.0;
-- Zad 4
CREATE TABLE Gry(
    Name NCHAR(63) NOT NULL PRIMARY KEY,
    ProductionDate DATE NOT NULL,
    GameType NCHAR(63) NOT NULL,
    Grade INT CHECK (Grade >= 1 AND Grade <= 10),
    Studio NCHAR(63) NOT NULL,
    NumberOfPlayers NCHAR(255),
    Platform NCHAR(12) NOT NULL CHECK (Platform IN ('PC', 'XBOX', 'PLAYSTATION'))
);
-- Zad 5
INSERT INTO Gry VALUES ('DOOM', '93-DEC-10', 'FPS', 10, 'id Software', 'Single player', 'PC');
INSERT INTO Gry VALUES ('DOOM II', '94-SEP-30', 'FPS', 10, 'id Software', 'Single player', 'PC');
INSERT INTO Gry VALUES ('DOOM 3', '04-AUG-03', 'FPS', 2, 'id Software', 'Single player', 'PC');
INSERT INTO Gry VALUES ('DOOM (2016)', '16-MAY-13', 'FPS', 10, 'id Software', 'Single player', 'PC');
INSERT INTO Gry VALUES ('WiedŸmin', '15-MAY-19', 'RPG', 10, 'CD Projekt Red', 'Single player', 'XBOX');
-- Zad 6
UPDATE Gry
SET Grade = 5.0
WHERE Platform = 'XBOX';
-- Zad 7
CREATE TABLE Ulubione_gry(
    StudentID INT NOT NULL,
    GameID NCHAR(63) NOT NULL,
    CONSTRAINT fk_ulubione_gry_studenci FOREIGN KEY (StudentID) REFERENCES Studenci(ID),
    CONSTRAINT fk_ulubione_gry_gry FOREIGN KEY (GameID) REFERENCES Gry(Name)
);
-- Zad 8
DROP TABLE Ulubione_gry;
DROP TABLE Studenci;
DROP TABLE Gry;