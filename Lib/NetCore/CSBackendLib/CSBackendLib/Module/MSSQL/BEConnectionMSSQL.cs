using System.Data;
using System.Data.SqlClient;

namespace BEBackendLib.Module.MSSQL
{
    public class BEConnectionMSSQL : BEIMSSQL, IDisposable
    {
        private BEMSSQLConfig? _dbConfig = null;
        private bool _isUseTrustServerCertificate = false;
        private int _timeOutExec = 0;
        private string _connString = string.Empty;

        public string ConnString { get => _connString; set => _connString = value; }

        public BEConnectionMSSQL(BEMSSQLConfig dbConfig)
        {
            _dbConfig = dbConfig;
            InitConnectionString();
            SQLConnection(_connString);
        }

        public override SqlParameter? AddParameter(string paramName, SqlDbType sqlDbType, object value)
        {
            SqlParameter? res = new SqlParameter();
            try
            {
                // -----
                res.ParameterName = paramName;
                res.SqlDbType = sqlDbType;
                res.IsNullable = true;


                // -----
                if (value is null || value.ToString() == "")
                {
                    switch (sqlDbType)
                    {
                        case SqlDbType.TinyInt:
                        case SqlDbType.SmallInt:
                        case SqlDbType.Int:
                        case SqlDbType.BigInt:
                        case SqlDbType.Float:
                        case SqlDbType.Decimal:
                        case SqlDbType.Real:
                            res.Value = 0;
                            break;

                        case SqlDbType.Bit:
                            res.Value = false;
                            break;

                        case SqlDbType.DateTime:
                        case SqlDbType.SmallDateTime:
                        case SqlDbType.VarBinary:
                        case SqlDbType.Xml:
                        case SqlDbType.Structured:
                        case SqlDbType.Image:
                        case SqlDbType.Binary:
                        case SqlDbType.Timestamp:
                        case SqlDbType.UniqueIdentifier:
                            res.Value = DBNull.Value;
                            break;

                        default:
                            res.Value = "";
                            break;
                    }
                }
                else
                {
                    switch (sqlDbType)
                    {
                        case SqlDbType.UniqueIdentifier:
                            if (value is not null)
                            {
                                {
                                    string? ite = value.ToString();
                                    if (ite is not null)
                                        res.Value = Guid.Parse(ite);
                                }
                            }

                            break;

                        case SqlDbType.Int:
                            {
                                {
                                    string? ite = value.ToString();
                                    if (ite is not null)
                                        res.Value = int.Parse(ite);
                                }
                            }
                            break;

                        case SqlDbType.BigInt:
                            {
                                {
                                    string? ite = value.ToString();
                                    if (ite is not null)
                                        res.Value = Int64.Parse(ite);
                                }
                            }

                            break;

                        case SqlDbType.TinyInt:
                            {
                                {
                                    string? ite = value.ToString();
                                    if (ite is not null)
                                        res.Value = Byte.Parse(ite);
                                }
                            }

                            break;

                        case SqlDbType.SmallInt:
                            {
                                {
                                    string? ite = value.ToString();
                                    if (ite is not null)
                                        res.Value = Int16.Parse(ite);
                                }
                            }

                            break;

                        case SqlDbType.Bit:
                            {
                                {
                                    string? ite = value.ToString();
                                    if (ite is not null)
                                        res.Value = bool.Parse(ite);
                                }
                            }

                            break;

                        case SqlDbType.VarChar:
                        case SqlDbType.NVarChar:

                        case SqlDbType.NChar:
                            {
                                {
                                    string? ite = value.ToString();
                                    if (ite is not null)
                                    {
                                        res.Size = ite.Length;
                                        res.Value = ite;
                                    }
                                }
                            }

                            break;

                        case SqlDbType.VarBinary:
                            res.Size = -1;
                            res.Value = value;
                            break;

                        case SqlDbType.DateTime:
                            if (value.GetType() != typeof(string) && value.GetType() != typeof(String))
                            {
                                if ((DateTime)value != DateTime.MinValue)
                                    res.Value = value;
                                else
                                    res.Value = DBNull.Value;
                            }
                            else
                                res.Value = value;

                            break;

                        default:
                            res.Value = value; break;
                    }
                }

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool ExecuteNonQuery(string sqlQuery)
        {
            bool res = false;
            try
            {
                if (sqlQuery.Length == 0)
                    throw new Exception("SQL Query is nullOrEmpty, please check again!");

                if (_sqlConnection is null)
                    SQLConnection(_connString);

                else
                {
                    if (_sqlConnection.State == ConnectionState.Closed)
                        _sqlConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, _sqlConnection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = _timeOutExec;

                        int obj = cmd.ExecuteNonQuery();

                        cmd.Dispose();
                        res = obj > 0;
                    }
                }  
            }
            catch (Exception)
            {
                throw;
            }
            finally { CloseSQLConnection(); }
            return res;
        }

        public override bool ExecuteNonQuery(string sqlQuery, SqlParameter[]? parameters)
        {
            bool res = false;
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
            finally { CloseSQLConnection(); }
            return res;
        }

        public override object Execute_NoneQueryStoredProcedure(string procedureName, SqlParameter[]? parameters)
        {
            throw new NotImplementedException();
        }

        public override DataTable? Execute_StoredProcedure(string procedureName, SqlParameter[]? parameters)
        {
            throw new NotImplementedException();
        }

        public override DataTable? Execute_Table(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public override DataTable? Execute_Table(string sqlQuery, SqlParameter[]? parameters)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            try
            {
                _connString = string.Empty;
                _dbConfig = null;

                CloseSQLConnection();        
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void InitConnectionString()
        {
            try
            {
                if (_dbConfig is null)
                    throw new Exception("MSSQL Config is null, please check again!");

                if (_dbConfig.DatabaseName.Length == 0)
                    throw new Exception("MSSQL DatabaseName is nullorEmpty, please check again!");

                BEDatabase? dbConfigDetail = _dbConfig.DBConfig;
                if (dbConfigDetail is null)
                    dbConfigDetail = new BEDatabase();

                if (dbConfigDetail?.Server.Length == 0)
                    throw new Exception("MSSQL Server is nullorEmpty, please check again!");

                if (dbConfigDetail?.InitialCatalog.Length == 0)
                    throw new Exception("MSSQL InitialCatalog is nullorEmpty, please check again!");

                bool? bUseTrustServerCertificate = false;
                bUseTrustServerCertificate = dbConfigDetail?.UseTrustServerCertificate;

                if (bUseTrustServerCertificate is not null && !bUseTrustServerCertificate.Value)
                {
                    if (dbConfigDetail?.UID.Length == 0)
                        throw new Exception("MSSQL UID is nullorEmpty, please check again!");

                    if (dbConfigDetail?.PWD.Length == 0)
                        throw new Exception("MSSQL PWD is nullorEmpty, please check again!");
                }
                else
                    _isUseTrustServerCertificate = true;

                int? iTimeOut = 0;
                iTimeOut = dbConfigDetail?.Timeout;

                if (iTimeOut is not null)
                    _timeOutExec = iTimeOut.Value;

                if (_timeOutExec <= 0)
                    _timeOutExec = 10000;

                if (_connString.Length == 0)
                {
                    // --- Use Windows Certificate ---
                    if (_isUseTrustServerCertificate)
                        _connString = $"Server={dbConfigDetail?.Server};Database={_dbConfig.DatabaseName};Trusted_Connection={_isUseTrustServerCertificate};MultipleActiveResultSets={_isUseTrustServerCertificate}";

                    // -- Use Account Login ---
                    else
                    {
                        _connString = $"server={dbConfigDetail?.Server};Initial Catalog={dbConfigDetail?.InitialCatalog};" +
                            $"uid={dbConfigDetail?.UID};pwd={dbConfigDetail?.PWD};Timeout={_timeOutExec};" +
                            $"Encrypt={dbConfigDetail?.IsEncrypt};trustServerCertificate={dbConfigDetail?.IsTrustServerCertificate}";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
