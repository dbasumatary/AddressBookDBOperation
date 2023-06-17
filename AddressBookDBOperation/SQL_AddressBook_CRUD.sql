create database AddressBookCRUD;
use AddressBookCRUD;
CREATE TABLE Contact(contactID int primary key identity(1,1), FirstName varchar(50) not null, LastName varchar(50) not null,
                     Email varchar(100));

CREATE TABLE AddressDetails(contactID int , City varchar(50), State varchar(50), Birthday date not null, ZipCode int, Phone int,
                    foreign key (contactID) references Contact(contactID));

select * from Contact;
select * from AddressDetails;
