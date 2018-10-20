--Finds all the employees from HumanResources.Employee table who were 
--hired after 2009-01-01.
use [AdventureWorks2]
select * from HumanResources.Employee as SubTableOfEmployee
		 where DATEADD(year,0,'2009-01-01') < HireDate
		 
