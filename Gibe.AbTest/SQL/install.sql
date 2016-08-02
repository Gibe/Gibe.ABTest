CREATE TABLE AbExperiment (
	[Key] VARCHAR(50) NOT NULL PRIMARY KEY,
	[StartDate] DATETIME NOT NULL,
	[EndDate] DATETIME NULL,
	[Enabled] BIT NOT NULL DEFAULT(1),
	[Weight] INT NOT NULL
)
GO

CREATE TABLE AbVariation (
	[Id] INT NOT NULL PRIMARY KEY,
	[ExperimentKey] VARCHAR(50) NOT NULL,
	[VariationNumber] INT NOT NULL,
	[Enabled] BIT NOT NULL DEFAULT(1),
	[Weight] INT NOT NULL,
	[Definition] NVARCHAR(MAX)
)
GO

ALTER TABLE AbVariation
	ADD CONSTRAINT [FK_AbVariation_Experiment]
	FOREIGN KEY ([ExperimentKey])
	REFERENCES [AbExperiment] ([Key])
GO