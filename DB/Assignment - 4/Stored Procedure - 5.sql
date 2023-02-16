/*write a SQL query to Create Stored procedure in the Northwind database to retrieve
Ten Most Expensive Products*/

select top(10) Products.ProductID, Products.ProductName, Products.QuantityPerUnit, Products.UnitPrice
from products
order by Products.UnitPrice desc

create procedure ten_most_expensive
as
begin
	select top(10) Products.ProductID, Products.ProductName, Products.QuantityPerUnit, Products.UnitPrice
	from products
	order by Products.UnitPrice desc
end

execute ten_most_expensive