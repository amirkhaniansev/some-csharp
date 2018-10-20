--By using HAVING clause groups the rows in the Sales.SalesOrderDetail 
--table by product ID and eliminates products whose average 
--order quantities are more than 4.
use [AdventureWorks2]
select ProductID from Sales.SalesOrderDetail as SubTableOfSalesOrderDetail
				 group by ProductID 
				 having AVG(OrderQty) < 4
