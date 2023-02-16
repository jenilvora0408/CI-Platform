/*  write a SQL query to list all salespersons along with customer name, city, grade,
order number, date, and amount.  */

SELECT customer.cust_name, customer.city, customer.grade, orders.ord_no, orders.ord_date, orders.purch_amt
FROM ((salesman
INNER JOIN customer ON salesman.salesman_id = customer.salesman_id)
INNER JOIN orders	ON  customer.customer_id = orders.customer_id)