--1. Write an SQL query to insert a new student into the "Students" table with the following details:
--a. First Name: John,b. Last Name: Doe,c. Date of Birth: 1995-08-15,d. Email:john.doe@example.com ,e. Phone Number: 1234567890
insert into Students values(11,'John','Doe','1995-08-15','john.doe@example.com',1234567890);

--2. Write an SQL query to enroll a student in a course. Choose an existing student and course and insert a record into the "Enrollments" table with the enrollment date.
insert into Enrollments(student_id,course_id,enrollment_date) values(1,3,GETDATE());

--3. Update the email address of a specific teacher in the "Teacher" table. Choose any teacher and modify their email address.
update Teacher set email='21euee062@gmail.com' where teacher_id = 2;

--4. Write an SQL query to delete a specific enrollment record from the "Enrollments" table. Select an enrollment record based on the student and course.
delete from Enrollments where student_id=1 and course_id=1;

--5. Update the "Courses" table to assign a specific teacher to a course. Choose any course and teacher from the respective tables.
update Courses set teacher_id=4 where course_id=2;

--6. Delete a specific student from the "Students" table and remove all their enrollment records from the "Enrollments" table. Be sure to maintain referential integrity.
delete from Students where student_id=5;
delete from Enrollments where student_id=5;

--7. Update the payment amount for a specific payment record in the "Payments" table. Choose any payment record and modify the payment amount. 
update Payments set amount=800.00 where payment_id=3;