using System.Data;
using System.Data.SqlClient;

namespace BEBackendLib.Module.MSSQL
{
    public abstract class BEIMSSQL
    {
        protected SqlConnection? _sqlConnection = null;

        public abstract SqlParameter? AddParameter(string paramName, SqlDbType sqlDbType, object value);

        public abstract DataTable? Execute_Table(string sqlQuery);
        public abstract DataTable? Execute_Table(string sqlQuery, SqlParameter[]? parameters);

        public abstract bool ExecuteNonQuery(string sqlQuery);
        public abstract bool ExecuteNonQuery(string sqlQuery, SqlParameter[]? parameters);

        public abstract DataTable? Execute_StoredProcedure(string procedureName, SqlParameter[]? parameters);
        public abstract object Execute_NoneQueryStoredProcedure(string procedureName, SqlParameter[]? parameters);


        protected void SQLConnection(string connString)
        {
            try
            {
                if (_sqlConnection is null)
                    _sqlConnection = new SqlConnection(connString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void CloseSQLConnection()
        {
            try
            {
                if (_sqlConnection is not null)
                {
                    if (_sqlConnection.State != ConnectionState.Closed)
                        _sqlConnection.Close();

                    _sqlConnection = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected string? GetParameterValue(SqlParameter? parameter)
        {
            string? res = string.Empty;
            try
            {
                switch (parameter?.SqlDbType)
                {
                    case SqlDbType.VarChar:
                        {
                            if (parameter?.Value == DBNull.Value)
                                res = "''";
                            else
                                res = "'" + parameter?.Value?.ToString()?.Replace("'", "''") + "'";
                            break;
                        }

                    case SqlDbType.UniqueIdentifier:
                        {
                            if (parameter.Value == DBNull.Value)
                                res = "''";
                            else
                                res = "'" + parameter?.Value?.ToString()?.Replace("'", "''") + "'";
                            break;
                        }

                    case SqlDbType.DateTime2:
                        {
                            if (parameter.Value == DBNull.Value || string.Compare("null", parameter.Value.ToString(), true) == 0)
                                res = "''";
                            else
                                res = "'" + (parameter.Value ?? string.Empty) + "'";
                            break;
                        }

                    case SqlDbType.Decimal:
                        {
                            if (parameter.Value == DBNull.Value)
                                res = "0";
                            else
                                res = parameter?.Value.ToString();
                            break;
                        }

                    default:
                        {
                            if (parameter?.Value == DBNull.Value)
                                res = "NULL";
                            else
                                res = parameter?.Value.ToString();

                            break;
                        }
                }

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected string ConvertSQLQueryParamToString(string sqlQuery, SqlParameter[]? parameters)
        {
            string? res = string.Empty;
            try
            {
                if (parameters is not null && parameters.Length > 0)
                {
                    res = sqlQuery;
                    foreach (var x in parameters)
                    {
                        string? value = GetParameterValue(x);
                        res = res.Replace(x.ParameterName.ToString(), value);
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
