IF EXISTS (select 1 from sys.procedures 
          where name = 'PROC_GetCustomer')
BEGIN
    DROP PROCEDURE [dbo].[PROC_GetCustomer]
END

GO

CREATE PROCEDURE PROC_GetCustomer (
	@isAll bit, 
	@customerID int 
)

AS
BEGIN
		declare @sqlQuery nvarchar (max);
		declare @table nvarchar (max);

		set @table = '[dbo].[customer]';

		if (@isAll = 1)
			begin
				set @sqlQuery = 'select * from ' + @table + ' with (nolock) ';
			end
		else
			begin
				set @sqlQuery = 'select * from ' + @table + ' with (nolock) ' + 'where [CustomerID] = ' + cast (@customerID as nvarchar(max));
			end

		print (@sqlQuery);
		exec (@sqlQuery);
END


--exec PROC_GetCustomer 0, 1
--exec PROC_GetCustomer 1, null