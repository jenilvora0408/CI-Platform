/*  write a SQL query to find Employees who have the biggest salary in their Department  */

SELECT e.emp_name,e.salary,d.dept_name 
FROM Employee as e
INNER JOIN Department as d 
ON e.dept_id=d.dept_id
WHERE (e.salary) in (
	SELECT MAX(e.salary)
	FROM Department as d 
	INNER JOIN Employee e
	ON d.dept_id=e.dept_id
	GROUP BY d.dept_id
);
