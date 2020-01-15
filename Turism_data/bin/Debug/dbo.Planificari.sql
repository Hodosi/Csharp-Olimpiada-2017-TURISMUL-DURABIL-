CREATE TABLE [dbo].[Planificari] (
    [IDVizita]     INT          IDENTITY (1, 1) NOT NULL,
    [IDLocalitate] INT          NULL,
    [Frecventa]    VARCHAR (50) NULL,
    [DataStart]     DATETIME     NULL,
    [DataStop]     DATETIME     NULL,
    [Ziua]         INT          NULL,
    PRIMARY KEY CLUSTERED ([IDVizita] ASC)
);

