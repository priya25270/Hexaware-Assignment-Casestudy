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
student_id int foreign key(student_id) references Students(student_id) on delete cascade,
course_id int foreign key(course_id) references Courses(course_id),
enrollment_date date)


--5. Payments
create table Payments(
payment_id int primary key not null,
student_id int foreign key(student_id) references Students(student_id) on delete cascade,
amount decimal(6,2),
payment_date date)

--values inserting
-- insert into students table
insert into Students (student_id, first_name, last_name, date_of_birth, email, phone_number)
values 
(1, 'joe', 'aoe', '1995-09-15', 'joe.aoe@example.com', 1234567899),
(2, 'jane', 'smith', '1998-07-22', 'jane.smith@example.com', 9876543210),
(3, 'michael', 'johnson', '1997-11-11', 'michael.johnson@example.com', 5556667777),
(4, 'emily', 'davis', '1996-04-05', 'emily.davis@example.com', 4443332222),
(5, 'daniel', 'brown', '1999-09-09', 'daniel.brown@example.com', 1112223333),
(6, 'sophia', 'wilson', '2000-12-12', 'sophia.wilson@example.com', 6667778888),
(7, 'liam', 'martinez', '1995-06-18', 'liam.martinez@example.com', 9998887777),
(8, 'olivia', 'anderson', '1993-03-25', 'olivia.anderson@example.com', 3334445555),
(9, 'ethan', 'thomas', '2001-01-30', 'ethan.thomas@example.com', 8889990000),
(10, 'ava', 'taylor', '1992-05-14', 'ava.taylor@example.com', 2223334444);

-- insert into teacher table
insert into Teacher (teacher_id, first_name, last_name, email)
values 
(1, 'robert', 'miller', 'robert.miller@example.com'),
(2, 'laura', 'jones', 'laura.jones@example.com'),
(3, 'james', 'garcia', 'james.garcia@example.com'),
(4, 'maria', 'rodriguez', 'maria.rodriguez@example.com'),
(5, 'david', 'lee', 'david.lee@example.com'),
(6, 'susan', 'clark', 'susan.clark@example.com'),
(7, 'matthew', 'hall', 'matthew.hall@example.com'),
(8, 'nancy', 'young', 'nancy.young@example.com'),
(9, 'william', 'wright', 'william.wright@example.com'),
(10, 'linda', 'king', 'linda.king@example.com');

-- insert into courses table
insert into Courses (course_id, course_name, credits, teacher_id)
values 
(101, 'mathematics', 3, 1),
(102, 'physics', 4, 2),
(103, 'chemistry', 3, 3),
(104, 'biology', 3, 4),
(105, 'computer science', 5, 5),
(106, 'history', 3, 6),
(107, 'english', 4, 7),
(108, 'geography', 3, 8),
(109, 'economics', 4, 9),
(110, 'psychology', 3, 10);

-- insert into enrollments table
insert into Enrollments (enrollment_id, student_id, course_id, enrollment_date)
values 
(1, 1, 101, '2024-03-01'),
(2, 2, 102, '2024-03-02'),
(3, 3, 103, '2024-03-03'),
(4, 4, 104, '2024-03-04'),
(5, 5, 105, '2024-03-05'),
(6, 6, 106, '2024-03-06'),
(7, 7, 107, '2024-03-07'),
(8, 8, 108, '2024-03-08'),
(9, 9, 109, '2024-03-09'),
(10, 10, 110, '2024-03-10');
insert into Enrollments values(11, 3, 109, '2024-03-10');


-- insert into payments table
insert into Payments (payment_id, student_id, amount, payment_date)
values 
(1, 1, 500.00, '2024-03-05'),
(2, 2, 600.00, '2024-03-06'),
(3, 3, 550.00, '2024-03-07'),
(4, 4, 400.00, '2024-03-08'),
(5, 5, 700.00, '2024-03-09'),
(6, 6, 800.00, '2024-03-10'),
(7, 7, 750.00, '2024-03-11'),
(8, 8, 450.00, '2024-03-12'),
(9, 9, 500.00, '2024-03-13'),
(10, 10, 650.00, '2024-03-14');
insert into Payments values (11,3,400.00,'2023-08-09');

--Task 2
--1. Insert a new student
insert into Students (student_id, first_name, last_name, date_of_birth, email, phone_number) values (11, 'john', 'doe', '1995-08-15', 'john.doe@example.com', 1234567890);

--2. Write an SQL query to enroll a student in a course. Choose an existing student and course and insert a record into the "Enrollments" table with the enrollment date.
insert into enrollments (enrollment_id, student_id, course_id, enrollment_date) values (11, 1, 105, '2025-03-26');

--3. Update the email address of a specific teacher in the "Teacher" table. Choose any teacher and modify their email address.
update teacher set email = 'upmail@example.com' where teacher_id = 2;

--4. Write an SQL query to delete a specific enrollment record from the "Enrollments" table. Select an enrollment record based on the student and course.
delete from enrollments where student_id = 1 and course_id = 105;

--5. Update the "Courses" table to assign a specific teacher to a course. Choose any course and teacher from the respective tables.
update courses set teacher_id = 3 where course_id = 104;

--6. Delete a specific student from the "Students" table and remove all their enrollment records from the "Enrollments" table. Be sure to maintain referential integrity.
delete from students where student_id = 1;

--7. Update the payment amount for a specific payment record in the "Payments" table. Choose any payment record and modify the payment amount. 
update payments set amount = 550.00 where payment_id = 3;

--Task 3
--1. Write an SQL query to calculate the total payments made by a specific student. You will need to join the "Payments" table with the "Students" table based on the student's ID.
select s.student_id, s.first_name, s.last_name, sum(p.amount) as total_payments from Students s
join payments p on s.student_id = p.student_id where s.student_id = 3
group by s.student_id, s.first_name, s.last_name;

--2. Write an SQL query to retrieve a list of courses along with the count of students enrolled in each course. Use a JOIN operation between the "Courses" table and the "Enrollments" table.
select c.course_id, c.course_name, count(e.student_id) as student_count from Courses c
left join Enrollments e on c.course_id = e.course_id
group by c.course_id, c.course_name;

--3. Write an SQL query to find the names of students who have not enrolled in any course. Use a LEFT JOIN between the "Students" table and the "Enrollments" table to identify students without enrollments.
select s.student_id, s.first_name, s.last_name from Students s
left join Enrollments e on s.student_id = e.student_id
where e.student_id is null;

--4. Write an SQL query to retrieve the first name, last name of students, and the names of the courses they are enrolled in. Use JOIN operations between the "Students" table and the "Enrollments" and "Courses" tables.
select s.first_name+' '+ s.last_name as sname, c.course_name from Students s
join Enrollments e on s.student_id = e.student_id
join Courses c on e.course_id = c.course_id;

--5. Create a query to list the names of teachers and the courses they are assigned to. Join the "Teacher" table with the "Courses" table.
select t.first_name+' '+ t.last_name as tname, c.course_name from Teacher t
left join Courses c on t.teacher_id = c.teacher_id;

--6. Retrieve a list of students and their enrollment dates for a specific course. You'll need to join the "Students" table with the "Enrollments" and "Courses" tables.
select s.first_name, s.last_name, e.enrollment_date from Students s
join Enrollments e on s.student_id = e.student_id
join Courses c on e.course_id = c.course_id
where c.course_id = 104;  

--7. Find the names of students who have not made any payments. Use a LEFT JOIN between the "Students" table and the "Payments" table and filter for students with NULL payment records.
select s.Student_id, s.first_name, s.last_name from students s
left join Payments p on s.student_id = p.student_id
where p.student_id is null;

--8. Write a query to identify courses that have no enrollments. You'll need to use a LEFT JOIN between the "Courses" table and the "Enrollments" table and filter for courses with NULL enrollment records.
select c.course_id, c.course_name from courses c
left join enrollments e on c.course_id = e.course_id
where e.course_id is null;

--9. Identify students who are enrolled in more than one course. Use a self-join on the "Enrollments" table to find students with multiple enrollment records.
select e.student_id, s.first_name, s.last_name, count(e.course_id) as course_count from Enrollments e
join Students s on e.student_id = s.student_id
group by e.student_id, s.first_name, s.last_name
having count(e.course_id) > 1;

--10. Find teachers who are not assigned to any courses. Use a LEFT JOIN between the "Teacher" table and the "Courses" table and filter for teachers with NULL course assignments.
select t.teacher_id, t.first_name, t.last_name from Teacher t
left join Courses c on t.teacher_id = c.teacher_id
where c.teacher_id is null;

--Task 4
--1.Write an SQL query to calculate the average number of students enrolled in each course. Use aggregate functions and subqueries to achieve this.
select avg(student_count) as avg_students_per_course from 
(select course_id, count(student_id) as student_count from Enrollments group by course_id) as course_enrollment_counts;

--2.Use a subquery to find the maximum payment amount and then retrieve the student(s) associated with that amount.
select s.student_id, s.first_name, s.last_name, total_payments from Students s
join (select student_id, sum(amount) as total_payments from payments group by student_id
) p on s.student_id = p.student_id
where p.total_payments = (select max(total_payment) from 
(select student_id, sum(amount) as total_payment from Payments group by student_id) as payment_sums);

--3.Use subqueries to find the course(s) with the maximum enrollment count.
select c.course_id, c.course_name, count(e.student_id) as total_enrollments from Courses c
join Enrollments e on c.course_id = e.course_id
group by c.course_id, c.course_name
having count(e.student_id) = (select max(enrollment_count) from
(select course_id, count(student_id) as enrollment_count from Enrollments group by course_id) as enrollment_counts);

--4.Use subqueries to sum payments for each teacher's courses.
select t.teacher_id, t.first_name, t.last_name, 
(select sum(p.amount) from Payments p join enrollments e on p.student_id = e.student_id join courses c on e.course_id = c.course_id
where c.teacher_id = t.teacher_id) as total_payments from Teacher t;

--5. Identify students who are enrolled in all available courses. Use subqueries to compare a student's enrollments with the total number of courses.
select s.student_id, s.first_name, s.last_name from Students s
where (select count(distinct e.course_id) from enrollments e where e.student_id = s.student_id) = 
      (select count(course_id) from Courses);

--6. Retrieve the names of teachers who have not been assigned to any courses. Use subqueries to find teachers with no course assignments.
select first_name, last_name from Teacher
where teacher_id not in (select distinct teacher_id from courses where teacher_id is not null);

--7. Calculate the average age of all students. Use subqueries to calculate the age of each student based on their date of birth.
select avg(datediff(year, date_of_birth, getdate())) as avg_age from Students;

--8. Identify courses with no enrollments. Use subqueries to find courses without enrollment records.
select course_id, course_name from Courses where course_id not in (select distinct course_id from Enrollments);

--9. Calculate the total payments made by each student for each course they are enrolled in. Use subqueries and aggregate functions to sum payments.
select s.student_id, s.first_name, s.last_name, c.course_name, (select sum(p.amount) from Payments p
join enrollments e on p.student_id = e.student_id
where e.course_id = c.course_id and e.student_id = s.student_id) as total_payment from Students s
join enrollments e on s.student_id = e.student_id
join courses c on e.course_id = c.course_id;

--10. Identify students who have made more than one payment. Use subqueries and aggregate functions to count payments per student and filter for those with counts greater than one.
select student_id, first_name, last_name from Students
where student_id in (select student_id from Payments group by student_id having count(payment_id) > 1);

--11. Write an SQL query to calculate the total payments made by each student. Join the "Students" table with the "Payments" table and use GROUP BY to calculate the sum of payments for each student.
select s.student_id, s.first_name, s.last_name, sum(p.amount) as total_payments from Students s
join payments p on s.student_id = p.student_id
group by s.student_id, s.first_name, s.last_name;

--12. Retrieve a list of course names along with the count of students enrolled in each course. Use JOIN operations between the "Courses" table and the "Enrollments" table and GROUP BY to count enrollments.
select c.course_name, count(e.student_id) as student_count from Courses c
left join enrollments e on c.course_id = e.course_id
group by c.course_name;

--13. Calculate the average payment amount made by students. Use JOIN operations between the "Students" table and the "Payments" table and GROUP BY to calculate the average.
select avg(p.amount) as avg_payment from Payments p;








