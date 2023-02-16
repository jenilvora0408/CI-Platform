--1. Create a stored procedure in the Northwind database that will calculate the average
--value of Freight for a specified customer.Then, a business rule will be added that will
--be triggered before every Update and Insert command in the Orders controller,and
--will use the stored procedure to verify that the Freight does not exceed the average
--freight. If it does, a message will be displayed and the command will be cancelled.

SELECT * FROM CUSTOMERS;
SELECT * FROM Orders ;
SELECT AVG(Freight) FROM ORDERS WHERE customerId='ALFKI';

CREATE PROCEDURE spGetAvgFridge
@id nchar(5),
@AvgFridge float out
AS
BEGIN
	SELECT @AvgFridge=AVG(Freight) FROM ORDERS WHERE customerId=@id;
END

DECLARE @AvgFridge float;
DECLARE @id nchar(5);
SET @id='VICTE';
EXECUTE spGetAvgFridge @id,@AvgFridge out;
print @AvgFridge;


--===============================================
--trigger
--=============================================
CREATE TRIGGER trInsteadInsertOrders
ON Orders
INSTEAD OF INSERT 
AS
BEGIN
	DECLARE @AvgFridge float;
	DECLARE @InsertedFridge float;
	DECLARE @CustomerID nchar(5);
	DECLARE @EmployeeID int;
	DECLARE @OrderDate datetime;
	DECLARE @RequiredDate datetime;
	DECLARE @ShippedDate datetime;
	DECLARE @ShipVia int;
	DECLARE @ShipName nvarchar(40);
	DECLARE @ShipAddress nvarchar(60);
	DECLARE @ShipCity nvarchar(15);
	DECLARE @ShipRegion nvarchar(15);
	DECLARE @ShipPostalCode nvarchar(10);
	DECLARE @ShipCountry nvarchar(15);
	DECLARE @id nchar(5);
	SELECT
      @CustomerID=[CustomerID]
      ,@EmployeeID=[EmployeeID]
      ,@OrderDate=[OrderDate]
      ,@RequiredDate=[RequiredDate]
      ,@ShippedDate=[ShippedDate]
      ,@ShipVia=[ShipVia]
      ,@InsertedFridge=[Freight]
      ,@ShipName=[ShipName]
      ,@ShipAddress=[ShipAddress]
      ,@ShipCity=[ShipCity]
      ,@ShipRegion=[ShipRegion]
      ,@ShipPostalCode=[ShipPostalCode]
      ,@ShipCountry=[ShipCountry]
	  FROM inserted;

	EXECUTE spGetAvgFridge @CustomerID,@AvgFridge out;

	IF @AvgFridge>@InsertedFridge
	BEGIN
		INSERT INTO ORDERS VALUES(
		@CustomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@ShipVia,@InsertedFridge,@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry
		);
		SELECT 'VALUE INSERTED';
	END
	ELSE 
		SELECT 'Fridge value exeeds';
END

DROP TRIGGER trInsteadInsertOrders;


INSERT INTO [dbo].[Orders]
           ([CustomerID]
           ,[EmployeeID]
           ,[OrderDate]
           ,[RequiredDate]
           ,[ShippedDate]
           ,[ShipVia]
           ,[Freight]
           ,[ShipName]
           ,[ShipAddress]
           ,[ShipCity]
           ,[ShipRegion]
           ,[ShipPostalCode]
           ,[ShipCountry])
     VALUES
           ('VICTE'
           ,6
           ,'1996-07-04 00:00:00.000'
           ,'1996-08-04 00:00:00.000'
           ,'1996-07-14 00:00:00.000'
           ,3
           ,48
           ,'Frankenversand'
           ,'Berliner Platz 43'
           ,'München'
           ,NULL
           ,'80805'
           ,'Brazil')

SELECT * FROM ORDERS WHERE CustomerID='VICTE';
--===========================================================
--Update Trigger
--===========================================================

CREATE TRIGGER trInsteadUpdateOrders
ON Orders
INSTEAD OF UPDATE 
AS
BEGIN
	DECLARE @OrderId int;
	DECLARE @AvgFridge float;
	DECLARE @InsertedFridge float;
	DECLARE @CustomerID nchar(5);
	DECLARE @EmployeeID int;
	DECLARE @OrderDate datetime;
	DECLARE @RequiredDate datetime;
	DECLARE @ShippedDate datetime;
	DECLARE @ShipVia int;
	DECLARE @ShipName nvarchar(40);
	DECLARE @ShipAddress nvarchar(60);
	DECLARE @ShipCity nvarchar(15);
	DECLARE @ShipRegion nvarchar(15);
	DECLARE @ShipPostalCode nvarchar(10);
	DECLARE @ShipCountry nvarchar(15);
	DECLARE @id nchar(5);
	SELECT
	@OrderId=[OrderId]
      ,@CustomerID=[CustomerID]
      ,@EmployeeID=[EmployeeID]
      ,@OrderDate=[OrderDate]
      ,@RequiredDate=[RequiredDate]
      ,@ShippedDate=[ShippedDate]
      ,@ShipVia=[ShipVia]
      ,@InsertedFridge=[Freight]
      ,@ShipName=[ShipName]
      ,@ShipAddress=[ShipAddress]
      ,@ShipCity=[ShipCity]
      ,@ShipRegion=[ShipRegion]
      ,@ShipPostalCode=[ShipPostalCode]
      ,@ShipCountry=[ShipCountry]
	  FROM inserted;

	EXECUTE spGetAvgFridge @CustomerID,@AvgFridge out;

	IF @AvgFridge>@InsertedFridge
	BEGIN		
	UPDATE [dbo].[Orders]
	   SET [CustomerID] = @CustomerID
		  ,[EmployeeID] = @EmployeeID
		  ,[OrderDate] = @OrderDate
		  ,[RequiredDate] = @RequiredDate
		  ,[ShippedDate] = @ShippedDate
		  ,[ShipVia] = @ShipVia
		  ,[Freight] = @InsertedFridge
		  ,[ShipName] = @ShipName
		  ,[ShipAddress] = @ShipAddress
		  ,[ShipCity] =@ShipCity
		  ,[ShipRegion] = @ShipRegion
		  ,[ShipPostalCode] =@ShipPostalCode
		  ,[ShipCountry] = @ShipCountry
		WHERE OrderID=@OrderId;
		SELECT 'VALUE updated';
	END
	ELSE 
		SELECT 'Fridge value exeeds';
END

DROP TRIGGER trInsteadUpdateOrders;


UPDATE [dbo].[Orders]
   SET [CustomerID] = 'VICTE'
      ,[EmployeeID] = 6
      ,[OrderDate] = '1996-07-04 00:00:00.000'
      ,[RequiredDate] = '1996-08-04 00:00:00.000'
      ,[ShippedDate] = '1996-07-14 00:00:00.000'
      ,[ShipVia] = 3
      ,[Freight] = 40
      ,[ShipName] = 'Frankenversand'
      ,[ShipAddress] = 'Berliner Platz 43'
      ,[ShipCity] ='Rajkot'
      ,[ShipRegion] = NULL
      ,[ShipPostalCode] ='80805'
      ,[ShipCountry] = 'Brazil'
	WHERE OrderID=10251;


SELECT * FROM ORDERS WHERE OrderID=10251;
