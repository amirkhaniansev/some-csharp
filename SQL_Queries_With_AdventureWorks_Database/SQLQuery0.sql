--Returns all rows and only a subset of the columns (Name, ProductNumber, ListPrice) from
--the Product table. Change ListPrice column heading to ‘Price’.
use [AdventureWorks2]
select [Name],ProductNumber,ListPrice from Production.Product
							as SubTableOfProduct
							
