/*   Write a SQL statement to create a Cartesian product between salesperson and
customer, i.e. each salesperson will appear for all customers and vice versa for that
salesperson who belongs to that city   */

SELECT *
FROM salesman
CROSS JOIN customer
WHERE salesman.city = customer.city