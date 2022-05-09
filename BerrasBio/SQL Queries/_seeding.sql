
USE BerrasBioContext

DELETE FROM Bookable_Seats;
DELETE FROM Show;
DELETE FROM Movie;
DELETE FROM Seat;
DELETE FROM Saloon;
DELETE FROM AspNetUsers;

GO

EXEC Create_Saloon @SaloonName = 'Salong 1', @SeatCount = 50;

EXEC Create_Movie @MovieTitle = 'The Northman', @MovieLength = 8160, @MovieImage = 'thenorthman.jpg';
EXEC Create_Movie @MovieTitle = 'Sonic the Hedgehog 2', @MovieLength = 7320, @MovieImage = 'Sonic_the_Hedgehog_2_film_poster.jpg';

EXEC Create_Show_And_Bookable_Seats @MovieTitle = 'The Northman',  @SaloonName = 'Salong 1', @ShowTime = '2022-11-11 13:23:44', @TicketPrice = 298;
EXEC Create_Show_And_Bookable_Seats @MovieTitle = 'The Northman',  @SaloonName = 'Salong 1', @ShowTime = '2022-05-11 13:23:44', @TicketPrice = 229;
EXEC Create_Show_And_Bookable_Seats @MovieTitle = 'The Northman',  @SaloonName = 'Salong 1', @ShowTime = '2022-06-15 13:23:44', @TicketPrice = 259;
EXEC Create_Show_And_Bookable_Seats @MovieTitle = 'Sonic the Hedgehog 2',  @SaloonName = 'Salong 1', @ShowTime = '2022-11-11 18:23:44', @TicketPrice = 278;
EXEC Create_Show_And_Bookable_Seats @MovieTitle = 'Sonic the Hedgehog 2',  @SaloonName = 'Salong 1', @ShowTime = '2022-06-22 18:23:44', @TicketPrice = 199;
EXEC Create_Show_And_Bookable_Seats @MovieTitle = 'Sonic the Hedgehog 2',  @SaloonName = 'Salong 1', @ShowTime = '2022-04-15 18:23:44', @TicketPrice = 259;

select * from Seat;
select * from Movie;
select * from Saloon;
select * from Show;
select * from Bookable_Seats;
select * from AspNetUsers;
