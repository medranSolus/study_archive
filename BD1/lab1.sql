SELECT * FROM Employees;
--Zad 1
DESCRIBE Employees;
--Zad 2
SELECT country_name FROM Countries;
--Zad 3
SELECT employee_id, first_name, last_name FROM Employees;
--Zad 4
SELECT COUNT(*) FROM Employees;
--Zad 5
SELECT * FROM Employees WHERE salary>12000;
--Zad 6
SELECT * FROM Employees WHERE salary>5000 AND salary<7000;
--Zad 7
SELECT hire_date FROM Employees WHERE hire_date IN (SELECT MAX(hire_date) FROM Employees) AND ROWNUM = 1;
--Zad 8
SELECT * FROM Employees WHERE hire_date > '01-JAN-07';
--Zad 9 (Wy?wietli? imie nazwisko pracowników salary>5000 i nie manager)
SELECT first_name, last_name FROM Employees WHERE salary>5000 AND manager_id IS NOT NULL;

