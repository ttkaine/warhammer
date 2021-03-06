/*
   02 July 201507:42:01
   User: 
   Server: TRENT\SQLEXPRESS_2012
   Database: Warhammer
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Page SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Page', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Page', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Page', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Place ADD CONSTRAINT
	FK_Place_Page FOREIGN KEY
	(
	Id
	) REFERENCES dbo.Page
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Place ADD CONSTRAINT
	FK_Place_Place FOREIGN KEY
	(
	IsWithin
	) REFERENCES dbo.Place
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Place SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Place', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Place', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Place', 'Object', 'CONTROL') as Contr_Per 