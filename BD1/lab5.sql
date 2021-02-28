-- Zad 1 (Jak employee_id zajête to wybraæ inne dowolne)
INSERT INTO Employees(employee_id, last_name, email, hire_date, job_id, manager_id) 
VALUES (207, 'Nowak', 'nowak@gmail.com', '17/12/08', 'AC_ACCOUNT', (SELECT manager_id FROM Employees WHERE last_name = 'Chung'));
-- Zad 2
UPDATE Employees
SET salary = (SELECT AVG(salary) FROM Employees WHERE department_id = 110)
WHERE employee_id = 207;
-- Zad 3
UPDATE Employees
SET manager_id = (SELECT manager_id FROM Employees WHERE salary = (SELECT MAX(salary) FROM Employees))
WHERE employee_id = 207;
-- Zad 4
DELETE FROM Employees
WHERE phone_number IS NULL;
-- Zad 5
UPDATE Employees
SET department_id = (SELECT department_id FROM Employees WHERE salary = (SELECT MIN(salary) FROM Employees))
WHERE last_name LIKE 'K%';
