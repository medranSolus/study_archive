-- Zad 1
SELECT last_name, salary 
FROM employees 
WHERE salary > 0.8 * (SELECT salary FROM employees WHERE last_name = 'Abel');

-- Zad 2
SELECT last_name, salary 
FROM employees 
WHERE salary > 0.8 * (SELECT AVG(salary) FROM employees WHERE last_name LIKE 'C%');

-- Zad 3
SELECT last_name, salary, departments.department_name, locations.city 
FROM employees
LEFT JOIN departments USING(department_id)
LEFT JOIN locations USING(location_id)
WHERE salary > (SELECT MAX(salary) FROM employees LEFT JOIN departments USING(department_id) WHERE department_name = 'Sales');

-- Zad 4
SELECT first_name, last_name, hire_date 
FROM employees 
WHERE hire_date > (SELECT hire_date FROM employees WHERE salary = (SELECT MAX(salary) FROM employees));

-- Zad 5
SELECT department_name 
FROM departments dep 
WHERE (SELECT COUNT(*) FROM employees WHERE department_id = dep.department_id) > (SELECT COUNT(*) FROM employees WHERE hire_date > '07/01/01');

-- Zad 6
WITH managers AS 
    (SELECT manager_id, COUNT(*) AS employee_count
    FROM employees
    GROUP BY manager_id
    HAVING manager_id IS NOT NULL)
SELECT manager_id, (SELECT employee_count FROM managers WHERE manager_id = emp.manager_id) AS employee_count, last_name, salary
FROM employees emp
WHERE manager_id IS NOT NULL
ORDER BY manager_id;

-- Zad 7
SELECT last_name, salary 
FROM employees emp
WHERE manager_id IS NOT NULL AND salary > (SELECT salary FROM employees WHERE emp.manager_id = employee_id);
