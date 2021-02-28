-- Zad 1.1
SELECT employees.first_name, employees.last_name, departments.department_name as department FROM employees 
LEFT OUTER JOIN departments ON employees.department_id = departments.department_id
WHERE employees.salary > 8000;
-- Zad 1.2
SELECT employees.first_name, employees.last_name, departments.department_name as department FROM employees 
LEFT OUTER JOIN departments USING (department_id)
WHERE employees.salary > 8000;
-- Zad 2
SELECT first_name, last_name, job_id, salary FROM employees
INNER JOIN jobs USING(job_id)
WHERE employees.salary > jobs.min_salary;
-- Zad 3
SELECT employees.first_name, employees.last_name, NVL(departments.department_name, 'NO_DEPARTMENT') as department FROM employees 
LEFT OUTER JOIN departments ON employees.department_id = departments.department_id;
-- Zad 4
SELECT locations.city, locations.postal_code, NVL(departments.department_name, 'NO_DEPARTMENT') FROM locations
LEFT OUTER JOIN departments USING(location_id);
-- Zad 5
SELECT e.last_name, NVL(e1.last_name, 'NO_MANAGER') AS manager FROM employees e
LEFT OUTER JOIN employees e1 ON e.manager_id = e1.employee_id
ORDER BY e.last_name;
-- Zad 6
SELECT employees.first_name, employees.last_name, NVL(departments.department_name, 'NO_DEPARTMENT') as department, NVL(locations.city, 'NO_CITY') as location FROM employees
LEFT OUTER JOIN departments USING(department_id)
LEFT OUTER JOIN locations ON departments.location_id = locations.location_id;
-- Zad 7
SELECT countries.country_name, COUNT(employees.employee_id) as employee_count FROM countries
LEFT OUTER JOIN locations ON countries.country_id = locations.country_id
LEFT OUTER JOIN departments ON locations.location_id = departments.location_id
LEFT OUTER JOIN employees ON departments.department_id = employees.department_id
GROUP BY countries.country_name;
-- Zad 8
SELECT COUNT(employee_id), department_name FROM employees
INNER JOIN departments USING(department_id)
HAVING COUNT(employee_id) > 5
GROUP BY department_name;
-- Zad 9
SELECT COUNT(employee_id), department_name FROM employees e
INNER JOIN departments USING(department_id)
HAVING COUNT(employee_id) > 2 AND salary > (SELECT AVG(salary) FROM employees WHERE job_id = e.job_id)
GROUP BY department_name;