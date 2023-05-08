using CSGlobal.Global;
using CSGlobal.Module.Threads;

namespace WinFormsApp
{
    public class WFThread : CSThreadManagers
    {
        private string _LOGFILE = "WFThread.log";

        public WFThread(int interval, int sleepTime) : base(interval, sleepTime)
        {
        }

        protected override void MainProcessing()
        {
            try
            {
                CSGlobals.Log(_LOGFILE, "\r\n MainProcessing running", CSLOGGER.TRACE);
            }
            catch (Exception ex)
            {
                CSGlobals.Log(_LOGFILE, $"\r\n MainProcessing Exception: \r\n {ex.Message} \r\n {ex.StackTrace}", CSLOGGER.ERROR);
            }
        }

        public void Start()
        {
            try
            {
                StartProcessing();
            }
            catch (Exception ex)
            {
                CSGlobals.Log(_LOGFILE, $"\r\n Start Exception: \r\n {ex.Message} \r\n {ex.StackTrace}", CSLOGGER.ERROR);
            }
        }

        public void Stop()
        {
            try
            {
                StopProcessing(SessionID);
            }
            catch (Exception ex)
            {
                CSGlobals.Log(_LOGFILE, $"\r\n Stop Exception: \r\n {ex.Message} \r\n {ex.StackTrace}", CSLOGGER.ERROR);
            }
        }
    }
}
