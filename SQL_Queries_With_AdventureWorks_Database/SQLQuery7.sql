--uspGetEmployeeManagersPerDepartment stored procedure that returns 
--direct managers of employees who work at specified department
use [AdventureWorks2]
go
create procedure uspGetEmployeeManagersPerDepartment
   (@BusinessEntityID integer)
    as
	begin
	declare @ManagerID integer = (select BusinessEntityID from HumanResources.Employee 
				  where OrganizationNode = 
				  (select OrganizationNode  from HumanResources.Employee
				  where BusinessEntityID = @BusinessEntityID).GetAncestor(1)
				  )

	--selecting employee BusinessEntityID, first name,last name,job title  
	select employee.BusinessEntityID as BusinessEntityID,
		   businessEntity.FirstName as  EmployeeFirstName,
		   businessEntity.LastName as EmployeeLastName,
		   employee.JobTitle as EmployeeJobTitle,

		   --selecting manager gender,first name,last name

		   (select employee.Gender from HumanResources.Employee employee 
							where BusinessEntityID = @ManagerID)
							as ManagerGender ,

			(select businessEntity.FirstName from Person.Person businessEntity
							where BusinessEntityID = @ManagerID)
							as ManagerFirstName,

			(select businessEntity.LastName from Person.Person businessEntity
							where BusinessEntityID = @ManagerID)
							as ManagerLastName 

           from HumanResources.Employee employee 

			full join Person.Person businessEntity
			on businessEntity.BusinessEntityID = employee.BusinessEntityID

			where employee.BusinessEntityID = @BusinessEntityID		
	end
