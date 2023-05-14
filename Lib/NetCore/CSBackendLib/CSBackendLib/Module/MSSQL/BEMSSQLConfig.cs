namespace BEBackendLib.Module.MSSQL
{
    public class BEMSSQLConfig
    {
        public string DatabaseName { get; set; } = string.Empty;
        public BEDatabase? DBConfig { get; set; } = new BEDatabase();
    }

    public class BEDatabase
    {
        public string Server { get; init; } = string.Empty;
        public string InitialCatalog { get; init; } = string.Empty;
        public string UID { get; init; } = string.Empty;
        public string PWD { get; init; } = string.Empty;
        public int? TimeoutExec { get; set; } = 10;
        public int? TimeoutOpen { get; set; } = 30;
        public bool IsEncrypt { get; set; } = true;
        public bool IsTrustServerCertificate { get; set; } = true;
        public bool? UseTrustServerCertificate { get; set; } = false;
    }
}
