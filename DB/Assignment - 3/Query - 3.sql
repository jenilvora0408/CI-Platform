/*  write a SQL query to find All Department along with the number of people there   */

SELECT Department.dept_name, COUNT(*) as Count
FROM Employee 
INNER JOIN Department ON
Employee.dept_id = Department.dept_id
GROUP BY Department.dept_name