--1. Provide a SQL script that initializes the database for the Job Board scenario “CareerHub”.

create database careerhub;
use careerhub;

--2.Create tables for Companies, Jobs, Applicants and Applications.
--3.Define appropriate primary keys, foreign keys, and constraints.
--4. Ensure the script handles potential errors, such as if the database or tables already exist.
create table Companies(
    CompanyID int primary key identity(1,1),
    CompanyName varchar(50) not null unique,
    Location varchar(30) not null);

insert into companies (companyname, location) values
('tech corp', 'new york'),
('data systems', 'san francisco'),
('cloud net', 'seattle'),
('fintech ltd', 'boston'),
('health plus', 'chicago'),
('green energy', 'denver'),
('auto drive', 'detroit'),
('cyber secure', 'austin'),
('meditech', 'miami'),
('edu learn', 'los angeles');
insert into companies (companyname, location) values
('tech corps', 'new york')
insert into companies (companyname, location) values
('tech corpss', 'new york')


create table jobs (
    JobID int primary key identity(1,1),
    CompanyID int not null,
    JobTitle varchar(50) not null,
    JobDescription text not null,
    JobLocation varchar(60) not null,
    Salary decimal(10,2) not null,
    JobType varchar(50) not null,
    PostedDate datetime not null,
    constraint fk_jobs_comapny foreign key (CompanyID) references Companies(CompanyID) on delete cascade
);

insert into jobs (companyid, jobtitle, jobdescription, joblocation, salary, jobtype, posteddate) values
(1, 'software engineer', 'develop and maintain software applications', 'new york', 90000.00, 'full-time', '2025-03-27 10:00:00'),
(2, 'data analyst', 'analyze data trends for business insights', 'san francisco', 75000.00, 'full-time', '2025-03-26 09:30:00'),
(3, 'cloud architect', 'design cloud infrastructure', 'seattle', 120000.00, 'full-time', '2025-03-25 08:45:00'),
(4, 'financial analyst', 'evaluate financial performance', 'boston', 85000.00, 'full-time', '2025-03-24 11:15:00'),
(5, 'nurse practitioner', 'provide medical care', 'chicago', 95000.00, 'full-time', '2025-03-23 12:00:00'),
(6, 'renewable energy engineer', 'design sustainable energy solutions', 'denver', 110000.00, 'full-time', '2025-03-22 14:00:00'),
(7, 'automotive engineer', 'develop self-driving car technology', 'detroit', 105000.00, 'full-time', '2025-03-21 15:30:00'),
(8, 'cybersecurity analyst', 'monitor security threats', 'austin', 98000.00, 'full-time', '2025-03-20 16:45:00'),
(9, 'biomedical engineer', 'design medical devices', 'miami', 102000.00, 'full-time', '2025-03-19 17:30:00'),
(10, 'education consultant', 'develop training materials', 'los angeles', 78000.00, 'full-time', '2025-03-18 18:15:00');
insert into jobs (companyid, jobtitle, jobdescription, joblocation, salary, jobtype, posteddate) values
(11, 'software engineer', 'develop and maintain software applicationss', 'new york', 0.00, 'full-time', '2025-03-27 10:00:00')
insert into jobs (companyid, jobtitle, jobdescription, joblocation, salary, jobtype, posteddate) values
(12, 'software engineer', 'develop and maintain software applicationss', 'new york', -1.00, 'full-time', '2025-03-27 10:00:00')
insert into jobs (companyid, jobtitle, jobdescription, joblocation, salary, jobtype, posteddate) values
(12, 'test engineer', 'test and maintain software applicationss', 'new york', 11000.00, 'full-time', '2025-03-27 10:00:00')
insert into jobs (companyid, jobtitle, jobdescription, joblocation, salary, jobtype, posteddate) values
(12, 'civil engineer', 'develop and maintain buildings', 'new york', 15000.00, 'full-time', '2025-03-27 10:00:00')


create table Applicants (
    ApplicantID int primary key identity (1,1),
    FirstName varchar(50) not null,
    LastName varchar(50) not null,
    Email varchar(50) unique not null,
    Phone varchar(20) unique not null,
    resume text not null,
	city varchar(20),
	State varchar(20),
	experience int
	
);

insert into applicants (firstname, lastname, email, phone, resume,city,State,experience) values
('john', 'doe', 'john.doe@example.com', '1234567890', 'experienced software developer','cbe','tn',1),
('jane', 'smith', 'jane.smith@example.com', '2345678901', 'data analyst with python expertise','salem','tn',1),
('michael', 'johnson', 'michael.johnson@example.com', '3456789012', 'cloud architect with aws experience','erode','tn',3),
('emily', 'davis', 'emily.davis@example.com', '4567890123', 'financial analyst in fintech industry','banglore','karnataka',3),
('david', 'martin', 'david.martin@example.com', '5678901234', 'registered nurse with 5 years experience','chennai','tn',4),
('sarah', 'lee', 'sarah.lee@example.com', '6789012345', 'energy engineer passionate about sustainability','trichy','tn',1),
('robert', 'clark', 'robert.clark@example.com', '7890123456', 'automotive engineer in self-driving tech','tripur','tn',0),
('laura', 'lopez', 'laura.lopez@example.com', '8901234567', 'cybersecurity analyst with cissp certification','pune','Maharashtra',1),
('brian', 'hall', 'brian.hall@example.com', '9012345678', 'biomedical engineer specializing in prosthetics','cbe','tn',3),
('anna', 'young', 'anna.young@example.com', '0123456789', 'education consultant with e-learning expertise','cbe','tn',5);
insert into applicants (firstname, lastname, email, phone, resume,city,State,experience) values
('jo', 'doe', 'john.dos@example.com', '1234567880', 'experienced developer','cbe','tn',0)
insert into applicants (firstname, lastname, email, phone, resume,city,State,experience) values
('don', 'doe', 'john.dos@examples.com', '1234567889', 'experienced developer','cbe','tn',0)

create table Applications (
    ApplicationID int primary key identity(1,1),
    JobID int not null,
    ApplicantID int not null,
    ApplicationDate datetime not null,
    CoverLetter text,
    constraint fk_applications_job foreign key (JobID) references Jobs(JobID) on delete cascade,
    constraint fk_applications_applicant foreign key (ApplicantID) references Applicants(ApplicantID) on delete cascade,
    constraint unique_application unique (JobID, ApplicantID)
);

insert into applications (jobid, applicantid, applicationdate, coverletter) values
(1, 1, '2025-03-27 10:30:00', 'i am excited to apply for this position.'),
(2, 2, '2025-03-26 09:45:00', 'my skills align perfectly with this role.'),
(3, 3, '2025-03-25 08:55:00', 'i have extensive experience in cloud computing.'),
(4, 4, '2025-03-24 11:25:00', 'finance and analysis are my passion.'),
(5, 5, '2025-03-23 12:10:00', 'i am eager to join the healthcare industry.'),
(6, 6, '2025-03-22 14:15:00', 'i am passionate about renewable energy solutions.'),
(7, 7, '2025-03-21 15:40:00', 'automotive engineering is my expertise.'),
(8, 8, '2025-03-20 16:50:00', 'i have experience in threat monitoring and security.'),
(9, 9, '2025-03-19 17:40:00', 'medical technology is my focus area.'),
(10, 10, '2025-03-18 18:20:00', 'education and training are my strengths.');
insert into applications (jobid, applicantid, applicationdate, coverletter) values(1, 11, '2025-03-19 18:21:00', 'education and training are my strengths.');


--5. Write an SQL query to count the number of applications received for each job listing in the "Jobs" table. Display the job title and the corresponding application count. Ensure that it lists all jobs, even if they have no applications.
select j.JobTitle, count(a.ApplicationID) as app_cnt from jobs j 
left join Applications a on j.JobID= a.JobID 
group by j.JobTitle;

--6.Develop an SQL query that retrieves job listings from the "Jobs" table within a specified salary range. Allow parameters for the minimum and maximum salary values. Display the job title, company name, location, and salary for each matching job.
select j.jobtitle, c.companyname, j.joblocation, j.salary from jobs j
join companies c on j.companyid = c.companyid
where j.salary between (select min(salary) from jobs) and (select max(salary) from jobs)  order by salary desc;

--7.Write an SQL query that retrieves the job application history for a specific applicant. Allow a parameter for the ApplicantID, and return a result set with the job titles, company names, and application dates for all the jobs the applicant has applied to.
select a.ApplicantID,j.jobtitle, c.companyname, a.applicationdate from applications a
join jobs j on a.jobid = j.jobid
join companies c on j.companyid = c.companyid
where a.applicantid = 11;

--8.Create an SQL query that calculates and displays the average salary offered by all companies for job listings in the "Jobs" table. Ensure that the query filters out jobs with a salary of zero.
select avg(salary) as average_salary from jobs where salary > 0;

--9.Write an SQL query to identify the company that has posted the most job listings. Display the company name along with the count of job listings they have posted. Handle ties if multiple companies have the same maximum count.
select c.companyname, count(j.jobid) as job_count from companies c
join jobs j on c.companyid = j.companyid
group by c.companyname
having count(j.jobid) = (select max(job_count)
from (select count(jobid) as job_count from jobs group by companyid) as job_counts);

--10.Find the applicants who have applied for positions in companies located in 'CityX' and have at least 3 years of experience.
select distinct a.applicantid, a.firstname, a.lastname, a.experience, c.companyname, j.jobtitle from applicants a
join applications ap on a.applicantid = ap.applicantid
join jobs j on ap.jobid = j.jobid
join companies c on j.companyid = c.companyid
where c.location = 'Boston' and a.experience >= 3;

--11. Retrieve a list of distinct job titles with salaries between $60,000 and $80,000.
select distinct jobtitle from jobs where salary between 60000 and 80000;

--12. Find the jobs that have not received any applications.
select j.jobid, j.jobtitle, c.companyname, j.joblocation from jobs j
join companies c on j.companyid = c.companyid
left join applications a on j.jobid = a.jobid
where a.applicationid is null;

--13.Retrieve a list of job applicants along with the companies they have applied to and the positions they have applied for.
select a.applicantid, a.firstname, a.lastname, c.companyname, j.jobtitle from applications app
join applicants a on app.applicantid = a.applicantid
join jobs j on app.jobid = j.jobid
join companies c on j.companyid = c.companyid;

--14.Retrieve a list of companies along with the count of jobs they have posted, even if they have not received any applications.
select c.companyid, c.companyname, count(j.jobid) as job_count from companies c
left join jobs j on c.companyid = j.companyid
group by c.companyid, c.companyname;

--15.List all applicants along with the companies and positions they have applied for, including those who have not applied.
select a.applicantid, a.firstname, a.lastname, j.jobtitle, c.companyname from applicants a
left join applications app on a.applicantid = app.applicantid
left join jobs j on app.jobid = j.jobid
left join companies c on j.companyid = c.companyid;

--16. Find companies that have posted jobs with a salary higher than the average salary of all jobs.
select distinct c.companyid, c.companyname,j.salary from companies c
join jobs j on c.companyid = j.companyid
where j.salary > (select avg(salary) from jobs);

--17.Display a list of applicants with their names and a concatenated string of their city and state.
select applicantid, firstname, lastname, concat(city, ', ', state) as location from applicants;

--18.Retrieve a list of jobs with titles containing either 'Developer' or 'Engineer'.
select jobid, jobtitle, companyid, joblocation, salary from jobs 
where jobtitle like '%developer%' or jobtitle like '%engineer%';

--19.Retrieve a list of applicants and the jobs they have applied for, including those who have not applied and jobs without applicants.
select a.applicantid, a.firstname, a.lastname, j.jobid, j.jobtitle, c.companyname
from applicants a
full outer join applications app on a.applicantid = app.applicantid
full outer join jobs j on app.jobid = j.jobid
full outer join companies c on j.companyid = c.companyid
order by a.applicantid, j.jobid;

--20.List all combinations of applicants and companies where the company is in a specific city and the applicant has more than 2 years of experience. For example: city=Chennai
select a.applicantid, a.firstname, a.lastname, c.companyid, c.companyname, c.location from applicants a
join companies c on 1=1 where c.location = 'boston' and a.experience > 2;



