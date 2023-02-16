/* write a SQL query to find those customers with a grade less than 300. Return
cust_name, customer city, grade, Salesman, salesmancity. The result should be
ordered by ascending customer_id.  */SELECT customer.cust_name, customer.city, customer.grade, salesman.name, salesman.cityFROM customerLEFT JOIN salesmanON customer.salesman_id = salesman.salesman_idWHERE customer.grade < 300