/*    write a SQL query to find Departments that have less than 3 people in it   */

SELECT Department.dept_name
FROM Department
INNER JOIN Employee
ON Department.dept_id = Employee.dept_id
GROUP BY Department.dept_name
HAVING COUNT(Department.dept_name) < 3
