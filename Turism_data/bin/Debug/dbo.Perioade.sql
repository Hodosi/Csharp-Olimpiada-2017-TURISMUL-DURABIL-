CREATE TABLE [dbo].[Perioade]
(
	[IdPerioade] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nume] VARCHAR(50) NULL, 
    [DataStart] DATETIME NULL, 
    [DataStop] DATETIME NULL, 
    [Frecventa] VARCHAR(50) NULL
)
