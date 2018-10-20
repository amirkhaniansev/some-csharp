--creating Books table
use [Bookstore]
create table Books (
			ID int primary key,
			Author nvarchar(255),
			Title nvarchar(255),
			Price float,
			[Year] int)