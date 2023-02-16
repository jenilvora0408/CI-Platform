/*  Write a SQL statement to join the tables salesman, customer and orders so that the
same column of each table appears once and only the relational rows are returned.  */SELECT s.salesman_id,s.name,s.city[salesman city],s.commission,
c.customer_id,c.cust_name,c.city[cust_city],c.grade,
o.ord_no,o.purch_amt,o.ord_date 
FROM orders as o INNER JOIN customer as c ON o.customer_id=c.customer_id
INNER JOIN salesman as s ON c.salesman_id=s.salesman_id;