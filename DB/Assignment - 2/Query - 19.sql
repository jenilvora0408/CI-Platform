/*   Write a SQL statement to create a Cartesian product between salesperson and
customer, i.e. each salesperson will appear for every customer and vice versa for
those salesmen who belong to a city and customers who require a grade   */

SELECT *
FROM salesman
CROSS JOIN customer
WHERE salesman.city IS NOT NULL AND customer.grade IS NOT NULL