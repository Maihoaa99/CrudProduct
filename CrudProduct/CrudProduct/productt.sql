create database Modell
go 
use Modell
go

create table [order]
(
	id int primary key identity,
	created_at datetime
)
create table product 
(
	id int primary key identity,
	name nvarchar(100),
	unit_price varchar(20)
)
create table order_detail
(
	id int primary key identity,
	order_id int foreign key references [order](id),
	product_id int foreign key references product(id)
)