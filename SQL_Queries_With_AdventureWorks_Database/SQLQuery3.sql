--Retrieves employees job titles from HumanResources.Employee
--table without duplicates.
use [AdventureWorks2]
select distinct JobTitle from HumanResources.Employee as JobTitlesTable
