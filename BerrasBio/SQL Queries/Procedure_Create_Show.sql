USE BerrasBioContext
GO

CREATE OR ALTER PROCEDURE Create_Show
@MovieID int,
@SaloonID int,
@ShowTime datetime2
AS
/*	Insert Show */
INSERT INTO Show (MovieID, SaloonID, ShowTime)
VALUES(@MovieID, @SaloonID, @ShowTime);
