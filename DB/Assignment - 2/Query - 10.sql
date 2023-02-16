/*  Write a SQL statement to make a report with customer name, city, order number,
order date, and order amount in ascending order according to the order date to
determine whether any of the existing customers have placed an order or not   */

SELECT customer.cust_name, customer.city, orders.ord_no, orders.ord_date, orders.purch_amt
FROM customer
LEFT JOIN orders
ON orders.customer_id = customer.customer_id
ORDER BY orders.ord_date