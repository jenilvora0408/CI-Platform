/*  Write a SQL statement to generate a list in ascending order of salespersons who
work either for one or more customers or have not yet joined any of the customers  */

SELECT salesman.name
FROM salesman
LEFT JOIN customer
ON salesman.salesman_id = customer.salesman_id