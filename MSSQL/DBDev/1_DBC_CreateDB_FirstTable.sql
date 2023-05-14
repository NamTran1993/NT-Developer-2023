--create database db_dev;

use db_dev;

create table Customer(
	CustomerID  int not null IDENTITY(1,1) PRIMARY KEY,
	LastName nvarchar (max) not null,
	FistName nvarchar (max),
	Age int,
	Phone nvarchar (max),
);

-- Index
CREATE INDEX idx_customerID ON Customer (CustomerID);
