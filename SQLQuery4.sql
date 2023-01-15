use HotelDB;
-- Room type
create table roomType(
	roomTypeId int primary key,
	numberOfBeds int NOT NULL,
	PricePerNight MONEY NOT NULL
);

-- hotel room table
CREATE TABLE Room (
	room_ID INT PRIMARY KEY,
    room_number INT NOT NULL,
    room_type_ID INT NOT NULL,
    pricePerNight MONEY NOT NULL,
    capacity INT NOT NULL,
    description TEXT,
    isOccupied bit,
	isClean bit,
	RoomImage varchar(255) NOT NULL,
	CONSTRAINT fk_type FOREIGN KEY (room_type_ID) REFERENCES roomType(roomTypeId)
);
EXEC sp_help Room;
-- Table Reservation: stocker l'histoire des reservation 
create table Reservation(
	reservation_ID int primary key,
	roomID int,
	DateDeReservation date NOT NULL,
	Check_In date NOT NULL,
	Check_Out date NOT NULL,
	NumberOfGuests int NOT NULL,
	isPaid bit NOT NULL,
	customerName varchar(20),
	customerEmail varchar(255),
	customerPhone varchar(20),
	customerAdress varchar(255),
	CONSTRAINT fk_res FOREIGN KEY (roomID) REFERENCES Room(room_ID)
);

create table review(
	review_ID int primary key,
	reservation_ID int,
	roomId int,
	review text,
	CONSTRAINT fk_rev FOREIGN KEY (reservation_ID) REFERENCES reservation(reservation_ID)
);
