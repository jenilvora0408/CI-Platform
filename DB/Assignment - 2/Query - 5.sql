SELECT customer.cust_name, customer.city as Customer_City, salesman.name, salesman.city as Salesman_City, salesman.commission
FROM customer
INNER JOIN salesman
ON customer.salesman_id = salesman.salesman_id 
WHERE salesman.commission > 0.12 AND customer.city <> salesman.city