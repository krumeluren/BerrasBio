USE BerrasBioContext
GO

CREATE OR ALTER PROCEDURE Create_Show_And_Bookable_Seats
@MovieTitle nvarchar(100),
@SaloonName nvarchar(100),
@ShowTime datetime2,
@TicketPrice int
AS
DECLARE @MovieId int = (SELECT TOP 1 MovieID FROM Movie WHERE Movie.Title = @MovieTitle)
DECLARE @SaloonId int = (SELECT TOP 1 SaloonID FROM Saloon WHERE Saloon.Name = @SaloonName)
/*	Insert Show */
EXEC Create_Show 
@MovieID = @MovieId,
@SaloonID = @SaloonId,	
@ShowTime = @ShowTime;
/* Insert bookable seats to new show*/
DECLARE @ShowId int = (SELECT TOP 1 ShowID FROM Show WHERE MovieID = @MovieId AND SaloonID = @SaloonId AND ShowTime = @ShowTime)
EXEC Create_Bookable_Seats 
@SaloonID = @SaloonId,
@ShowID = @ShowId,
@TicketPrice = @TicketPrice;


