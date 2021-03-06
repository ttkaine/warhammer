IF(NOT EXISTS(
	select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'ChangeLog'
))
BEGIN
CREATE TABLE dbo.ChangeLog
	(
	Id int NOT NULL,
	DateTime datetime NOT NULL,
	Comment nvarchar(500) NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.ChangeLog ADD CONSTRAINT
	PK_ChangeLog PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]



INSERT INTO dbo.ChangeLog (Id, DateTime, Comment) VALUES (1,GetDate(),'Initial creation of the Change Log Table');

END