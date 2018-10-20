--Puts the results into groups after retrieving only the rows 
--with list prices greater than $900.
use [AdventureWorks2]
select ProductModelID,sum([Weight]) as WeightSum from Production.Product
				as GroupedSubTableOfProducts
				where ListPrice > 900
				group by ProductModelID
