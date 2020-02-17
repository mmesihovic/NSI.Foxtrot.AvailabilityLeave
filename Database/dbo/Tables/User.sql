CREATE TABLE [dbo].[User]
(
	[UserId] UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	[Firstname] NVARCHAR(50) NOT NULL,
	[Lastname] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(50) NOT NULL,
	[Username] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL,
    [DateCreated] DATETIME NOT NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [DateModified] DATETIME NULL, 
    [ModifiedBy] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User]([UserId]), 
    CONSTRAINT [FK_User_ModifiedBy] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[User]([UserId])
)
