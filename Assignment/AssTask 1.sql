--Task 1: Database Tables
create database SIS;

--1. Students
create table Students( 
student_id int primary key not null, 
first_name varchar(30) not null,
last_name varchar(30) not null, 
date_of_birth date not null,
email varchar(50) unique not null,
phone_number bigint unique not null)

--2. Teacher
create table Teacher(
teacher_id int primary key not null,
first_name varchar(30) not null,
last_name varchar(30) not null, 
email varchar(50) unique not null)


--3. Cources
create table Courses(
course_id int primary key not null,
course_name varchar(30),
credits int check (credits >0),
teacher_id int foreign key(teacher_id) references Teacher(teacher_id) on delete set null)

--4. Enrollments
create table  Enrollments(
enrollment_id int primary key not null,
student_id int foreign key(student_id) references Students(student_id) on delete set null,
course_id int foreign key(course_id) references Courses(course_id),
enrollment_date date)

--5. Payments
create table Payments(
payment_id int primary key not null,
student_id int foreign key(student_id) references Students(student_id) on delete set null,
amount decimal(6,2),
payment_date date)

--1. Insert atleast 10 records for each table each
--Students
insert into Students values(1,'Padmapriya','K','2003-10-25','Priya@gmail.com','9003752576')
insert into Students values(2,'ria','K','2003-11-25','ria@gmail.com','9003752578')
insert into Students values(3,'lia','A','2003-01-31','lia@gmail.com','9875634562')
insert into Students values(4,'sri','B','2003-02-28','sri@gmail.com','9823546785')
insert into Students values(5,'hari','G','2003-06-13','hari@gmail.com','9756483621')
insert into Students values(6,'vela','E','2003-09-14','velan@gmail.com','9765234190')
insert into Students values(7,'mridu','K','2003-08-31','mridu@gmail.com','956483927')
insert into Students values(8,'krish','Y','2003-05-05','krish@gmail.com','9137596835')
insert into Students values(9,'vinay','I','2003-09-05','vinay@gmail.com','9765296870')
insert into Students values(10,'ram','J','2003-11-03','ram@gmail.com','9045672967')

--2. Teacher
insert into Teacher values(1,'Robert', 'Miller', 'robert.miller@example.com')
insert into Teacher values(2,'Jessica', 'Davis', 'jessica.davis@example.com')
insert into Teacher values(3,'William', 'Garcia', 'william.garcia@example.com')
insert into Teacher values(4,'Linda', 'Rodriguez', 'linda.rodriguez@example.com')
insert into Teacher values(5,'Christopher', 'Martinez', 'christopher.martinez@example.com')
insert into Teacher values(6,'Barbara', 'Hernandez', 'barbara.hernandez@example.com')
insert into Teacher values(7,'Matthew', 'Lopez', 'matthew.lopez@example.com')
insert into Teacher values(8,'Susan', 'Gonzalez', 'susan.gonzalez@example.com')
insert into Teacher values(9,'Joseph', 'Perez', 'joseph.perez@example.com')
insert into Teacher values(10,'Elizabeth', 'Thomas', 'elizabeth.thomas@example.com')

--3. Cources
insert into Courses values (1,'Mathematics', 3, 1)
insert into Courses values (2,'Physics', 4, 2)
insert into Courses values (3,'Chemistry', 3, 3)
insert into Courses values (4,'Biology', 4, 4)
insert into Courses values (5,'Computer Science', 3, 5)
insert into Courses values (6,'History', 3, 6)
insert into Courses values (7,'English Literature', 2, 7)
insert into Courses values (8,'Economics', 3, 8)
insert into Courses values (9,'Political Science', 3, 9)
insert into Courses values (10,'Psychology', 3, 10)

--4. Enrollments
insert into Enrollments values (1,1, 1, '2024-01-10')
insert into Enrollments values (2,2, 2, '2024-01-11')
insert into Enrollments values (3,3, 3, '2024-01-12')
insert into Enrollments values (4, 4,4, '2024-01-13')
insert into Enrollments values (5,5, 5, '2024-01-14')
insert into Enrollments values (6,6, 6, '2024-01-15')
insert into Enrollments values (7,7, 7, '2024-01-16')
insert into Enrollments values (8,8, 8, '2024-01-17')
insert into Enrollments values (9,9, 9, '2024-01-18')
insert into Enrollments values (10,10, 10, '2024-01-19')

--5. Payments
insert into Payments values (1,1, 500.00, '2024-02-01')
insert into Payments values (2,2, 450.00, '2024-02-02')
insert into Payments values (3,3, 600.00, '2024-02-03')
insert into Payments values (4,4, 550.00, '2024-02-04')
insert into Payments values (5,5, 700.00, '2024-02-05')
insert into Payments values (6,6, 400.00, '2024-02-06')
insert into Payments values (7,7, 650.00, '2024-02-07')
insert into Payments values (8,8, 480.00, '2024-02-08')
insert into Payments values (9, 9, 520.00, '2024-02-09')
insert into Payments values (10,10, 580.00, '2024-02-10')