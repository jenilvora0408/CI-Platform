SELECT salesman.name, customer.cust_name, customer.city
FROM salesman
INNER JOIN customer
ON salesman.city = customer.city