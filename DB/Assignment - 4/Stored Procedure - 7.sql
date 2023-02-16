/*7. write a SQL query to Create Stored procedure in the Northwind database to update
Customer Order Details*/

select * from [Order Details] where [Order Details].Quantity = 4

update [Order Details]
set UnitPrice = 87.67, Quantity = 4, Discount = 0
where OrderID = 10248 AND ProductID = 11

create procedure update_table
@unit_price float,
@quantity int,
@discount  int,
@order_id int,
@product_id int

as
begin
	update [Order Details]
	set UnitPrice = @unit_price, Quantity = @quantity, Discount = @discount
	where OrderID = @order_id and ProductID = @product_id
end

declare @unit_price float;
declare @quantity int;
declare @discount int;
declare @order_id int;
declare @product_id int;

set @unit_price = 105.45;
set @quantity = 4;
set @discount = 0;
set @order_id = 10248;
set @product_id = 11;

execute update_table @unit_price, @quantity, @discount, @order_id, @product_id;
