--Case study

create database Casestudy

create table Artist (
    Artist_id int primary key not null,
    Name varchar(40) not null,
    Biography varchar(300),
    BirthDate DATE,
    Nationality varchar(50),
    Website varchar(300),
    Contact_Information varchar(200)
)

create table Artwork (
    Artwork_id int primary key not null,
    Title varchar(100) NOT NULL,
    Description varchar(450),
    Creation_Date DATE,
    Medium varchar(100),
    Image_URL varchar(350),
    Artist_id int NOT NULL foreign key (Artist_id) references Artist(Artist_id)
)

Create table Users (
    UserId int primary key not null,
    Username varchar(250) not null,
    Password varchar(10) not null,
    Email varchar(250) UNIQUE not null,
    FirstName varchar(100),
    LastName varchar(100),
    DateOfBirth DATE,
    ProfilePicture varchar(350),
	)

Create table User_Favorite_Artwork (
    UserId int not null,
    Artwork_id int  not null,
    primary key (UserId, Artwork_id),
    foreign key (UserID) references Users(UserId) on delete cascade,
    foreign key (Artwork_id) references Artwork(Artwork_id) on delete cascade
)

create table Gallery (
    Gallery_id int primary key not null,
    Name varchar(250) not null,
    Description varchar(300),
    Location varchar(100),
    Curator int not null,
    OpeningHours varchar(255) foreign key (Curator) references Artist(Artist_id)
)

create table Artwork_Gallery (
Artwork_id int not null foreign key (Artwork_id) references Artwork(Artwork_id) ON DELETE CASCADE,
Gallery_id int not null foreign key (Gallery_id) references Gallery(Gallery_id) ON DELETE CASCADE
primary key (Artwork_id, Gallery_id)
)
