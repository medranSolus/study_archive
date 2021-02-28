-- Zad 1
SELECT AVG(salary) FROM Employees WHERE department_id = 50;
-- Zad 2
SELECT department_id, COUNT(*) AS how_many FROM Employees GROUP BY department_id;
-- Zad 3
SELECT department_id, job_id, AVG(salary) FROM Employees GROUP BY department_id, job_id ORDER BY department_id;
-- Zad 4
SELECT MIN(salary) FROM Employees WHERE department_id = 40 AND job_id = 'HR_REP';
-- Zad 5
SELECT COUNT(DISTINCT job_id) FROM Employees WHERE department_id = 30;
-- Zad 6
SELECT department_id, COUNT(*) AS count FROM Employees GROUP BY department_id HAVING COUNT(*) >= 5;
-- Zad 7
SELECT department_id, MAX(salary) FROM Employees GROUP BY department_id ORDER BY department_id;
-- Zad 8
SELECT job_id, MIN(salary) AS minimal_salary FROM Employees WHERE department_id = 100 GROUP BY job_id ORDER BY minimal_salary;
-- Zad 9 
SELECT last_name, department_id, salary FROM Employees WHERE department_id IN (SELECT department_id FROM Employees GROUP BY department_id HAVING AVG(salary) < 12000) ORDER BY last_name;