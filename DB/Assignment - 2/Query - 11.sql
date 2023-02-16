/*   Write a SQL statement to generate a report with customer name, city, order number,
order date, order amount, salesperson name, and commission to determine if any of
the existing customers have not placed orders or if they have placed orders through
their salesman or by themselves   */SELECT customer.cust_name, customer.city, orders.ord_no, orders.ord_date, orders.purch_amt, salesman.name, salesman.commissionFROM ((customerLEFT JOIN orders ON customer.customer_id = orders.customer_id)LEFT JOIN salesman ON customer.salesman_id = salesman.salesman_id)