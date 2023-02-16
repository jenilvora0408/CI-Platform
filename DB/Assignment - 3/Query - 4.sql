/*   write a SQL query to find All Department along with the total salary there   */

SELECT Department.dept_name, SUM(Employee.salary)
FROM Employee 
INNER JOIN Department ON
Employee.dept_id = Department.dept_id
GROUP BY Department.dept_name