CREATE TABLE [Logging].[Log] (
	  [Id] [int] IDENTITY(1,1) NOT NULL,
	  [MachineName] [nvarchar](50) NULL,
	  [Logged] NVARCHAR(50) NULL,
	  [Level] [nvarchar](50) NULL,
	  [Message] [nvarchar](max) NULL,
	  [Callsite] [nvarchar](max) NULL,
	  [Exception] [nvarchar](max) NULL,
    CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([Id] ASC)
)
