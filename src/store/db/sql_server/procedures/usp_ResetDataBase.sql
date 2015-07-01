-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_ResetDataBase
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
	EXEC sp_MSForEachTable 'ALTER TABLE ? DISABLE TRIGGER ALL';
	EXEC sp_MSForEachTable 'DELETE FROM ?';
	EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL';
	EXEC sp_MSForEachTable 'ALTER TABLE ? ENABLE TRIGGER ALL';
	EXEC sp_MSForEachTable 'IF OBJECTPROPERTY(object_id(''?''), ''TableHasIdentity'') = 1 DBCC CHECKIDENT (''?'', RESEED, 0)';
	--EXEC sp_MSFOREACHTABLE 'SELECT * FROM ?' 

	DECLARE @dbName VARCHAR(50)
	SELECT @dbName = DB_NAME()

	DBCC SHRINKDATABASE(@dbName)

	DECLARE @logName VARCHAR(50)
	SELECT @logName = name FROM sys.master_files WHERE database_id = db_id() AND type = 1

	DBCC SHRINKFILE(@logName, 1)
END

GO
