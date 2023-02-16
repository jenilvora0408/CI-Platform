/* write a SQL query to Create Stored procedure in the Northwind database to retrieve
Sales by Year*/

select YEAR(orders.OrderDate) as [Date of Order], SUM((UnitPrice*Quantity)-((UnitPrice*Quantity*Discount))) AS SALES
from [Order Details]
inner join Orders 
on [Order Details].OrderID = Orders.OrderID
group by YEAR(Orders.OrderDate)
order by YEAR(Orders.OrderDate) asc

create procedure SalesByYear
as
begin
	select YEAR(orders.OrderDate) as [Date of Order], SUM((UnitPrice*Quantity)-((UnitPrice*Quantity*Discount))) AS SALES
	from [Order Details]
	inner join Orders 
	on [Order Details].OrderID = Orders.OrderID
	group by YEAR(Orders.OrderDate)
	order by YEAR(Orders.OrderDate) asc
end

execute SalesByYear