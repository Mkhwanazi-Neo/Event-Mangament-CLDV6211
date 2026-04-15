USE master
IF EXISTS (SELECT * FROM sys.databases WHERE name ='EventEaseDB_ST10456845')
DROP DATABASE EventEaseDB_ST10456845
CREATE DATABASE EventEaseDB_ST10456845;

USE EventEaseDB_ST10456845;

-- Venue Table
CREATE TABLE Venue (
    VenueID INT IDENTITY(1,1) PRIMARY KEY,
    VenueName NVARCHAR(255) NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    Capacity INT NOT NULL CHECK (Capacity > 0),
    ImageUrl NVARCHAR(500) NULL
);

-- Event Table
CREATE TABLE Event (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    EventName NVARCHAR(255) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description NVARCHAR(1000) NULL,
    VenueID INT NULL, -- Venue may not be assigned initially
    FOREIGN KEY (VenueID) REFERENCES Venue(VenueID) ON DELETE SET NULL
);

-- Booking Table
CREATE TABLE Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT NOT NULL,
    VenueID INT NOT NULL,
    BookingDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (EventID) REFERENCES Event(EventID) ON DELETE CASCADE,
    FOREIGN KEY (VenueID) REFERENCES Venue(VenueID) ON DELETE CASCADE,
    CONSTRAINT UQ_Venue_Event UNIQUE (VenueID, EventID) -- Prevents double booking of the same venue for the same event
);

-- Ensure no two bookings overlap for the same venue
CREATE UNIQUE INDEX UQ_Venue_Booking ON Booking (VenueID, BookingDate);

-- Insert data into Venue table
INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl)
VALUES 
('City Hall', '123 Main Street, Cityville', 500, 'https://example.com/images/grand_hall.jpg'),
('Golf Club Center', '456 Lakeshore Road, Seaside', 200, 'https://example.com/images/lakeside_pavilion.jpg'),
('Riverside Park', '789 River Road, Riverpark', 300, 'https://example.com/images/riverside_conference.jpg'),
('Rooftop Center ', '101 Skyline Boulevard, Hilltop', 350, 'https://example.com/images/skyline_venue.jpg'),
('The City Garden', '202 Garden Street, Greenfield', 200, 'https://example.com/images/green_garden.jpg');

-- Insert data into Event table
INSERT INTO Event (EventName, EventDate, Description, VenueID)
VALUES 
('Car expo', '2025-05-15 09:00:00', 'Annual conference on car inovation.', 1),
('Bank confrence', '2025-06-20 18:00:00', 'A gathering of bank officals on the finacal year.', 2),
('Business Seminar', '2025-07-10 14:00:00', 'Seminar on business management and strategy.', 3),
('Music Concert', '2025-08-25 19:00:00', 'Live music concert featuring popular artitst.', 4),
('Wedding ceromony', '2025-09-12 15:00:00', 'An gathering of family members on the union of marrage.', 5);

-- Insert data into Booking table
INSERT INTO Booking (EventID, VenueID, BookingDate)
VALUES 
(1, 1, '2026-05-01 10:00:00'),
(2, 2, '2026-06-01 12:00:00'),
(3, 3, '2026-07-01 14:30:00'),
(4, 4, '2026-08-01 11:00:00'),
(5, 5, '2026-09-01 09:00:00');

SELECT * FROM Venue;
SELECT * FROM Event;
SELECT * FROM Booking;