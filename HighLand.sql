drop database if exists HighLand;

create database HighLand;

use HighLand;

create table Staffs(
	staff_id int auto_increment primary key,
    user_account varchar(100) not null unique,
    user_password varchar(200) not null,
    staff_name varchar(100) not null,
    staff_address varchar(200),
    staff_age int,
    staff_phone int,
    role int,
    is_active bit default 1
);

create user if not exists 'vtca'@'localhost' identified by 'vtcacademy';
grant all on HighLand.* to 'vtca'@'localhost';

insert into Staffs(user_account, user_password, staff_name, staff_phone, staff_age, role) values
	('staff01', 'e19d5cd5af0378da05f63f891c7467af', 'quan', 0377892812, 21, 1),
    ('staff02', '7733be61632fa6af88d31218e6c4afb2', 'kien', 0362067555, 21, 1),
    ('staff03', '69367521abb4d7383ab9a58bdc53ea1d', 'luan', 0867963364, 21, 1),
    ('staff04', '61d539d16090b7b9db7458f827429830', 'hung', 0366422865, 21, 1);
    -- abcd1234,abcd2345,abcd3456,abcd4567

create table Cards(
	card_id int auto_increment primary key,
    stat int
);
create table Orders(
	order_id int auto_increment primary key,
    order_date datetime,
    stat int,
    total_price float,
    staff_id int,
    card_id int, 
    
    constraint fk_staff 
    foreign key (staff_id) 
    references Staffs(staff_id),
    
    constraint fk_card
    foreign key (card_id)
    references Cards(card_id)
);

create table Drinks(
	drink_id int auto_increment primary key,
    drink_name nvarchar(200) not null,
    is_active bool
);

create table Sizes(
	size_id int auto_increment primary key,
    size_name nvarchar(50) not null
);

create table Drink_Sizes(
	drink_id int not null,
    size_id int not null,
	price float not null,
    primary key (drink_id,size_id),
    constraint fk_DrinkSizes_Sizes 
    foreign key(size_id) 
    references Sizes(size_id),
    
    constraint fk_DrinkSizes_Drinks 
    foreign key(drink_id) 
    references Drinks(drink_id) 
);

create table Order_details(
	order_id int,
    drink_id int,
    size_id int,
    quantity int,
    primary key (order_id,size_id,drink_id),
    constraint fk_Orderdetails_Orders
    foreign key (order_id)
    references Orders(order_id),
    
    constraint fk_Orderdetails_Drinks
    foreign key (drink_id)
    references Drinks(drink_id),
    
    constraint fk_Orderdetails_Sizes
    foreign key (size_id)
    references Sizes(size_id)
); 

insert into Drinks(drink_name,is_active)values
	('FREEZE TRÀ XANH', 1),
    ('COOKIES AND CREAM', 1),
    ('FREEZE SÔ CÔ LA', 1),
    ('FREEZE CARAMEL',1),
    ('FREEZE CLASSIC', 1),
    ('TRÀ SEN VÀNG', 1),
    ('TRÀ VẢI', 1),
    ('TRÀ ĐÀO', 1),
    ('BẠC XỈU ĐÁ', 1),
    ('PHIN SỮA ĐÁ', 1),
    ('PHIN ĐEN ĐÁ', 1),
    ('PHIN SỮA NÓNG', 1),
    ('PHIN ĐEN NÓNG', 1),
    ('AMERICANO',1),
    ('CAPUCHINO', 1),
    ('ESPRESSO', 1),
    ('CARAMEL MASCHIATO', 1),
    ('LATTE', 1),
    ('PHINDI KEM SỮA', 1),
    ('PHINDI HẠNH NHÂN', 1),
    ('PHINDI HỒNG TRÀ', 1),
    ('PHINDICHOCO', 1)
    ;
    
insert into Sizes(size_name) values
	('S'),('M'),('L');

insert into Drink_Sizes(drink_id,size_id,price) values
	(1,1,49000),(1,2,59000),(1,3,65000),
    (2,1,49000),(2,2,59000),(2,3,65000),
    (3,1,49000),(3,2,59000),(3,3,65000),
    (4,1,49000),(4,2,59000),(4,3,65000),
    (5,1,49000),(5,2,59000),(5,3,65000),
    (6,1,39000),(6,2,49000),(6,3,55000),
    (7,1,39000),(7,2,49000),(7,3,55000),
    (8,1,39000),(8,2,49000),(8,3,55000),
    (9,1,29000),(9,2,35000),(9,3,39000),
    (10,1,29000),(10,2,35000),(10,3,39000),
    (11,1,29000),(11,2,35000),(11,3,39000),
    (12,1,29000),(13,1,29000),
    (14,2,44000),(14,3,54000),
    (15,2,54000),(15,3,64000),
    (16,2,44000),(16,3,54000),
    (17,2,59000),(17,3,69000),
    (18,2,54000),(18,3,64000),
    (19,1,39000),(19,2,45000),(19,3,49000),
    (20,1,39000),(20,2,45000),(20,3,49000),
    (21,1,45000),(21,2,50000),(21,3,55000),
    (22,1,39000),(22,2,45000),(22,3,49000)
;    

insert into Cards(stat) values
	(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0),(0);
select * from drinks;
select * from drink_sizes;
select * from Cards;
select * from Staffs;

select * from drinks where drink_id = 1;

select sizes.size_id, size_name, price 
from drink_sizes inner join sizes on drink_sizes.size_id = sizes.size_id 
where drink_id = 18;

select drink_id, drink_name from drinks where is_active = true and drink_id = 1;

select d.drink_id, d.drink_name, s.size_id, s.size_name, ds.price 
from drinks d inner join drink_sizes ds 
on d.drink_id = ds.drink_id 
inner join sizes s 
on s.size_id = ds.size_id
where d.drink_id = 1 and is_active = true;

select sizes.size_id, size_name, price 
from drink_sizes inner join sizes 
on drink_sizes.size_id = sizes.size_id 
where drink_id = 1;

select drink_id, drink_name, is_active 
from drinks
where drink_id = 1 
and is_active = true;

select sizes.size_id, size_name, price 
from drink_sizes inner join sizes 
on drink_sizes.size_id = sizes.size_id 
where drink_id= 1;
