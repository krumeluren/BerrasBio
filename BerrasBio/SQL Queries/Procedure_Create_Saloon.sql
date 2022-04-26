USE BerrasBioContext
GO

CREATE OR ALTER PROCEDURE Create_Saloon 
@SaloonName nvarchar(100),
@SeatCount int
AS
/*	Insert a saloon */
INSERT INTO Saloon (Name) 
VALUES(@SaloonName)
/*Insert seats in saloon*/
DECLARE @seat_i int = 0
WHILE @seat_i < @SeatCount 
BEGIN
    SET @seat_i = @seat_i + 1
    INSERT INTO Seat(SaloonID)
	VALUES((SELECT TOP 1 SaloonID FROM Saloon
			WHERE Saloon.Name = @SaloonName
			))
END

