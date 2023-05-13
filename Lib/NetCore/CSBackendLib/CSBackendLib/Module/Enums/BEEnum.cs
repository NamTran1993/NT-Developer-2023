namespace BEBackendLib.Module.Enums
{
    public enum BELogger
    {
        NONE,
        ERROR,
        WARNING,
        DEBUG,
        TRACE
    }

    public enum BEStatusThread
    {
        NONE,
        STOP,
        START,
        PAUSE,
        RESUME,
        RUNNING
    }

    public enum BEGuid
    {
        DEFAULT,
        DEFAULT_2,
        DEFAULT_3,
        DEFAULT_4,
        DATE,
        TIME,
        REMOVE_LINE
    }

    public enum BEMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
