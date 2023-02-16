/*write a SQL query to Create Stored procedure in the Northwind database to retrieve
Sales By Category*/

select Categories.CategoryName, SUM(([Order Details].UnitPrice*Quantity)-(([Order Details].UnitPrice*Quantity*Discount))) AS SALES
from ((Categories
inner join Products on Categories.CategoryID = Products.CategoryID)
inner join [Order Details] on Products.ProductID = [Order Details].ProductID)
group by Categories.CategoryName

create procedure SalesByCategory
as
begin 
	select Categories.CategoryName, SUM(([Order Details].UnitPrice*Quantity)-(([Order Details].UnitPrice*Quantity*Discount))) AS SALES
	from ((Categories
	inner join Products on Categories.CategoryID = Products.CategoryID)
	inner join [Order Details] on Products.ProductID = [Order Details].ProductID)
	group by Categories.CategoryName
end

execute SalesByCategory;
