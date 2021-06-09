
go 
--Create database prisonerManagementSystem
go 
use prisonerManagementSystem
go


create table prisoner
(
id int identity(1,1)  primary key,
name varchar(100) not null,
cnic varchar(50) not null,
dob varchar(50) not null,
father_name varchar(50) not null,
punishment varchar(100) not null,
category int not null,
imprisonment_duration varchar(50) not null,
cell_type int not null,
allocated_meeting_time varchar(50) not null,
available_meeting_time varchar(50) not null,
[address] varchar(100) not null,
[image] varchar(400) not null,
stipend int
)
go
create table visitor
(
id int identity(1,1)  primary key,
name varchar(100) not null,
cnic varchar(50) not null,
contact varchar(50) not null,
[address] varchar(100) not null,
dob varchar(50) not null,
date_of_visit varchar(50) not null,
duration varchar(50) not null,
[image] varchar(400) not null,
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade
)

go
create table beneficiary
(
id int identity(1,1)  primary key,
name varchar(100) not null,
cnic varchar(50) not null,
contact varchar(50) not null,
[address] varchar(100) not null,
dob varchar(50) not null,
[image] varchar(400) not null,
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade
)

go

create table imprisonment_record
(
id int identity(1,1)  primary key,
jail_name varchar(100) not null,
imprisonment_duration varchar(50) not null,
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade,
reason_of_transfer varchar(100) not null,
entry_date varchar(20),
exit_date varchar(20)
)

go

create table crime_record
(
id int identity(1,1)  primary key,
crime_date varchar(20) not null,
[description] varchar(100) not null,
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade,
punishment varchar(100) not null,
imprisonment_date varchar(50) not null,
release_date varchar(50) not null
)

go

create table asset
(
id int identity(1,1)  primary key,
name varchar(100) not null,
worth int not null,
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade
)



go

create table courts_visited
(
id int identity(1,1)  primary key,
court_name varchar(100) not null,
[address] varchar(100) not null,
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade,
visit_date varchar(20) not null,
[description] varchar(100)
)

go
create table jail_officer
(
id int identity(1,1)  primary key,
name varchar(100) not null,
cnic varchar(50) not null,
contact varchar(50) not null,
[address] varchar(40) not null,
dob varchar(50) not null,
[image] varchar(400) not null,
appointment_date varchar(100) not null,
retirement_date varchar(100),
in_service int not null,
email varchar(50) not null,
[password] varchar(20) not null
)

go
create table court_officer
(
id int identity(1,1)  primary key,
name varchar(100) not null,
cnic varchar(50) not null,
contact varchar(50) not null,
[address] varchar(40) not null,
dob varchar(50) not null,
[image] varchar(400) not null,
appointment_date varchar(100) not null,
retirement_date varchar(100),
in_service int not null,
email varchar(50) not null,
[password] varchar(20) not null
)

go
create table jailer
(
id int identity(1,1)  primary key,
name varchar(100) not null,
cnic varchar(50) not null,
contact varchar(50) not null,
[address] varchar(40) not null,
dob varchar(50) not null,
[image] varchar(400) not null,
appointment_date varchar(100) not null,
retirement_date varchar(100),
in_service int not null,
email varchar(50) not null,
[password] varchar(20) not null
)


go
create table prisoner_transfer_request
(
id int identity(1,1)  primary key,
from_jail varchar(50) not null,
to_jail varchar(50) not null,
reason_of_transfer varchar(50) not null,
[description] varchar(400),
request_date varchar(30),
accept_date varchar(30),
[status] varchar(30),
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade,
court_officer_id int foreign key references court_officer(id) on delete set default  on update cascade,
jailer_id int foreign key references jailer(id) on delete set default  on update cascade
)


go

create table complaint
(
id int identity(1,1)  primary key,
title varchar(100) not null,
description varchar(100) not null,
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade,
reg_date varchar(20) not null,
resolved_date varchar(20),
[status] varchar(30),
court_officer_id int foreign key references court_officer(id) on delete set default  on update cascade,
jail_officer_id int foreign key references jail_officer(id) on delete set default  on update cascade
)


go
create table community_task
(
id int identity(1,1)  primary key,
task varchar(50),
[date] varchar(50),
amount int,
prisonerid int foreign key references prisoner(id) on delete set default  on update cascade
)
