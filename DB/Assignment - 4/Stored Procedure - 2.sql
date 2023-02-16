 /*write a SQL query to Create Stored procedure in the Northwind database to retrieve
Employee Sales by Country*/

select orders.ShipCountry as Country, SUM((UnitPrice*Quantity)-((UnitPrice*Quantity*Discount))) AS SALES
from [Order Details]
inner join Orders 
on [Order Details].OrderID = Orders.OrderID
where Orders.ShipCountry = 'FRANCE'
group by Orders.ShipCountry

alter procedure EmployeeSalesByCountry
@Country nvarchar(50)
as
begin
	select orders.ShipCountry as Country, SUM((UnitPrice*Quantity)-((UnitPrice*Quantity*Discount))) AS SALES
	from [Order Details]
	inner join Orders 
	on [Order Details].OrderID = Orders.OrderID
	where Orders.ShipCountry = @Country
	group by Orders.ShipCountry
end

declare @Country nvarchar(50);
set @Country = 'FRANCE';
execute EmployeeSalesByCountry @Country;