using BEBackendLib.Module.Enums;

namespace BEBackendLib.Module.Communicates
{
    public class BERequestModel
    {
        public string? Url { get; set; } = string.Empty;
        public BEMethod Method { get; set; } = BEMethod.GET;
        public string JsonRequest { get; set; } = string.Empty;
        public HeaderParam[]? HeaderParams { get; set; } = null;
        public HeaderParam? Authorizations { get; set; } = null;
        public string ContentType { get; set; } = "application/json";
        public int Timeout { get; set; } = 60; // --- seconds ---
        public string HostProxy { get; set; } = string.Empty;
        public int Port { get; set; }
    }

    public class HeaderParam
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

}
