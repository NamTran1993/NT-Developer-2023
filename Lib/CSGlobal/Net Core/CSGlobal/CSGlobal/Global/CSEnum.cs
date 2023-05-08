namespace CSGlobal.Global
{
    public enum CSLOGGER : int
    {
        NONE,
        ERROR,
        WARNING,
        DEBUG,
        TRACE
    }

    public enum CSTHREAD
    {
        NONE,
        STOP,
        START,
        PAUSE,
        RESUME,
        RUNNING
    }

    public enum CSGUID : int
    {
        DEFAULT,
        DEFAULT_2,
        DEFAULT_3,
        DEFAULT_4,
        DATE,
        TIME,
        REMOVE_LINE
    }
}
