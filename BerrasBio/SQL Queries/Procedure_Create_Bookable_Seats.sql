USE BerrasBioContext
GO

CREATE OR ALTER PROCEDURE Create_Bookable_Seats
@SaloonID int,
@ShowID int,
@TicketPrice int
AS
/*Insert bookable seats to show */
INSERT INTO Bookable_Seats(ShowID, SeatID, Ticket_Price)
SELECT @ShowID, SeatID, @TicketPrice FROM Seat WHERE SaloonID = @SaloonID;


