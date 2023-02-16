/*	Write a SQL statement to make a Cartesian product between salesman and
customer i.e. each salesman will appear for all customers and vice versa for those
salesmen who must belong to a city which is not the same as his customer and the
customers should have their own grade   */

SELECT *
FROM salesman
CROSS JOIN customer
WHERE salesman.city IS NOT NULL	
AND salesman.city <> customer.city AND customer.grade IS NOT NULL