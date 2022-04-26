USE BerrasBioContext
GO

CREATE OR ALTER PROCEDURE Create_Movie 
@MovieTitle nvarchar(100),
@MovieLength int,
@MovieImage nvarchar(200)
AS
/*	Insert Movie */
INSERT INTO Movie (Title, [Length], Image_Source)
VALUES(@MovieTitle, @MovieLength, 'images/movie_poster/' + @MovieImage);
