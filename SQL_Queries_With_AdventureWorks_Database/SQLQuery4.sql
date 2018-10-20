--Finds the total q order quantity of each sales order from 
--Sales.SalesOrderDetail table.
use [AdventureWorks2]
select SalesOrderDetailID,sum(OrderQty) from Sales.SalesOrderDetail
		as SalesOrderTotalQuantityTable
		group by SalesOrderDetailID
