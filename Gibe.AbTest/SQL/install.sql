CREATE TABLE AbExperiment (
	[Id] VARCHAR(50) NOT NULL PRIMARY KEY,
	[Key] VARCHAR(50) NOT NULL,
	[Description] VARCHAR(500) NOT NULL,
	[StartDate] DATETIME NOT NULL,
	[EndDate] DATETIME NULL,
	[Enabled] BIT NOT NULL DEFAULT(1),
	[Weight] INT NOT NULL
)
GO

CREATE TABLE AbVariation (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ExperimentId] VARCHAR(50) NOT NULL,
	[VariationNumber] INT NOT NULL,
	[Enabled] BIT NOT NULL DEFAULT(1),
	[Weight] INT NOT NULL,
	[Definition] NVARCHAR(MAX)
)
GO

ALTER TABLE AbVariation
	ADD CONSTRAINT [FK_AbVariation_AbExperiment]
	FOREIGN KEY ([ExperimentId])
	REFERENCES [AbExperiment] ([Id])
GO