/*  write a SQL query to find the details of an order. Return ord_no, ord_date,
purch_amt, Customer Name, grade, Salesman, commission   */

SELECT orders.ord_no as [Order No.], orders.ord_date as [Date of Order], orders.purch_amt as [Purchase Amount], customer.cust_name as [Customer Name], customer.grade,
 salesman.name as [Salesman Name], salesman.commission
FROM ((orders
INNER JOIN customer ON orders.customer_id = customer.customer_id)
INNER JOIN salesman ON orders.salesman_id = salesman.salesman_id)