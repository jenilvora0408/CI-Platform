/*write a SQL query to Create Stored procedure in the Northwind database to insert
Customer Order Details*/

insert into [Order Details]
values (10324, 42, 19.768, 4, 0.1)

select * from [Order Details]

create proc spInsertOrderDetails
@OrderID int,
@ProductID int,
@UnitPrice int,
@Quantity int,
@Discount int
 
as
begin

insert into [Order Details]
(OrderId, ProductID, UnitPrice, Quantity, Discount)
values
(@OrderId, @ProductID, @UnitPrice, @Quantity, @Discount)

end

declare 
@OrderID int,
@ProductID int,
@UnitPrice int,
@Quantity int,
@Discount int;

set @OrderID = 10324
set @ProductID = 16
set @UnitPrice = 13.90
set @Quantity = 21
set @Discount = 0.15

execute spInsertOrderDetails @OrderID,
@ProductID,
@UnitPrice,
@Quantity,
@Discount;