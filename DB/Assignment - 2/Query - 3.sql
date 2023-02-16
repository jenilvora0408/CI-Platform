SELECT customer.cust_name, customer.city, salesman.name, salesman.commission
FROM customer
INNER JOIN salesman
ON customer.salesman_id = salesman.salesman_id