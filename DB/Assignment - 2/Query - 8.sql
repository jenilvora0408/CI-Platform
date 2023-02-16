/*  write a SQL query to display the customer name, customer city, grade, salesman,
salesman city. The results should be sorted by ascending customer_id.  */

SELECT customer.cust_name as [Name of Customer], customer.city, customer.grade, salesman.name as [Name of Salesman], salesman.city
FROM customer
LEFT JOIN salesman
ON customer.salesman_id = salesman.salesman_id
ORDER BY customer.customer_id ASC