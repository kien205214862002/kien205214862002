create database Hotel_WPF;
use Hotel_WPF;
create table Tour(
Tour_ID int AUTO_INCREMENT PRIMARY KEY,
Tour_Name text,
Tour_Time date,
Tour_Type text,
Tour_Size text,
Tour_Loca text,
Tour_Vihicles text,
Tour_Price int,
Tour_InTroduction text,
Tour_Consists text,
Tour_Schedule text,
Tour_Duration text,
Tour_Departure text,
Tour_Image text
);
select * from Tour limit 1000
Insert into Tour(Tour_Name,Tour_Time,Tour_Type,Tour_Size,Tour_Loca,Tour_Vihicles,Tour_Price,Tour_InTroduction,Tour_Consists,Tour_Schedule,Tour_Duration,Tour_Departure) values('Vung tau', '2022-12-31', 'Da ngoai', '50', 'Ba Ria - Vung Tau', 'Bus', 
'1500000', 'Da ngoai ngoai bien', 'Tam bien, cam trai', 'Di cam trai tam bien roi ngu', '2 ngay 3 dem', 'Abc')

Delete From Tour Limit 10

create table Hotel(
Hotel_ID int AUTO_INCREMENT PRIMARY KEY,
Hotel_Name text,
Hotel_Fac text,
Hotel_Type text,
Hotel_Size text,
Hotel_Bed text,
Hotel_Loca text,
Hotel_Price int,
Hotel_Dis text,
Hotel_Popu text,
Hotel_Image text
)
Select * from Hotel limit 1

Drop Table Hotel

---Hotel_Name text,Hotel_Fac text,Hotel_Type text,Hotel_Size text,Hotel_Bed text,Hotel_Loca text,Hotel_Price int,Hotel_Dis text,Hotel_Popu text
create table Car(
Car_ID int AUTO_INCREMENT PRIMARY KEY,
Car_Name text,
Car_Type text,
Car_Seat text,
Car_PickUp text,
Car_DropOff text,
Car_Price int,
Car_Package text,
Car_Dropoff_Location text,
Car_Pickup_Location text,
Car_Deposit text,
Car_Image text not null
);

create table Plane_Ticket(
Ticket_ID int AUTO_INCREMENT PRIMARY KEY,
Ticket_Name text,
Ticket_Company text,
Ticket_Price int,
Ticket_FromID text,
Ticket_FromLoca text,
Ticket_ToID text,
Ticket_ToLoca text,
Ticket_Passenger text,
Ticket_Image text not null
)

create table User(
User_ID int AUTO_INCREMENT PRIMARY KEY,
User_Name text,
User_Email text,
User_Pass text,
User_Phone text,
User_Birth Date,
User_Sex text,
User_Avatar text
)
select * from User limit 1


create table Tour_Cus(
Cus_ID int AUTO_INCREMENT PRIMARY KEY,
Cus_Name text,
Cus_Email text,
Cus_Phone text,
Cus_Region text,
Cus_DateStart date,
Cus_DateEnd date,
Cus_member int,
Cus_Price int,
User_Email text not null
)
select * from Tour_Cus limit 1
drop table Tour_CUs;
create table Car_Cus(
Cus_ID int AUTO_INCREMENT PRIMARY KEY,
Cus_Name text,
Cus_Email text,
Cus_Phone text,
Cus_Price int,
User_Email text not null
);

Select * from Car_Cus limit 1
Drop table Car_Cus

create table Plane_Cus(
Cus_ID int AUTO_INCREMENT PRIMARY KEY,
Cus_Name text,
Cus_Email text,
Cus_Phone text,
Cus_Price int,
User_Email text not null
);
Select * from Plane_Cus limit 1
Drop table Plane_Cus

create table Hotel_Cus(
Cus_ID int AUTO_INCREMENT PRIMARY KEY,
Cus_Name text,
Cus_Email text,
Cus_Phone text,
Cus_Nights int,
CUS_CHECKIN DATE,
CUS_CHECKOUT DATE,
Cus_Price int,
User_Email text not null
);
select * from Hotel_Cus limit 10
Drop table Hotel_Cus