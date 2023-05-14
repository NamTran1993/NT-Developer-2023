insert into [dbo].[Customer] (FirstName, LastName, Age, Phone)
values (N'Trần Huy', N'Nam', 30, '0362 972 775')

select * from [dbo].[Customer] with (nolock)

-- change path data --
--ALTER DATABASE db_dev   
--    MODIFY FILE ( NAME = db_dev,   
--                  FILENAME = 'h:\DBDisk\db_dev.mdf');  
--GO
 
--ALTER DATABASE db_dev   
--    MODIFY FILE ( NAME = db_dev_log,   
--                  FILENAME = 'h:\DBDisk_Backup\db_dev_log.ldf');  
--GO

--ALTER DATABASE [db_dev] SET EMERGENCY;
--GO
--ALTER DATABASE [db_dev] set single_user
--GO
--DBCC CHECKDB ([db_dev], REPAIR_ALLOW_DATA_LOSS) WITH ALL_ERRORMSGS;
--GO
--ALTER DATABASE [db_dev] set multi_user
--GO

--ALTER DATABASE db_dev SET EMERGENCY;
--EXEC sp_detach_db db_dev
--EXEC sp_attach_single_file_db @DBName = db_dev, @physname = N'h:\DBDisk\db_dev.mdf'