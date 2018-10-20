--Returns only the rows for Product from Production.Product table 
--that have a product line of ‘S’ and that have days to manufacture that 
--is less than 5. Sort by name in ascending order.
use [AdventureWorks2]
select * from Production.Product as SubTableOfProduct
		 where ProductLine = 'S' and DaysToManufacture < 5
		 order by [Name]
