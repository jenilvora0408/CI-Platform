/*   Write a SQL statement to generate a list of all the salesmen who either work for one
or more customers or have yet to join any of them. The customer may have placed
one or more orders at or above order amount 2000, and must have a grade, or he
may not have placed any orders to the associated supplier    */

SELECT salesman.name 
FROM ((salesman
LEFT JOIN customer ON salesman.salesman_id = customer.customer_id)
INNER JOIN orders ON customer.customer_id = orders.customer_id)
WHERE orders.purch_amt >= 2000 AND grade IS NOT NULL