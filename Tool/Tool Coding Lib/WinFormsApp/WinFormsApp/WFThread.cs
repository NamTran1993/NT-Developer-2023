
using BEBackendLib.Module.Enums;
using BEBackendLib.Module.Globals;
using BEBackendLib.Module.Threads;

namespace WinFormsApp
{
    public class WFThread : BEThreadManagers
    {
        private string _LOGFILE = "WFThread.log";

        public WFThread(int interval, int sleepTime) : base(interval, sleepTime)
        {
        }

        protected override void MainProcessing()
        {
            try
            {
                BEGlobals.Log(_LOGFILE, "\r\n MainProcessing running", BELogger.TRACE);
            }
            catch (Exception ex)
            {
                BEGlobals.Log(_LOGFILE, $"\r\n MainProcessing Exception: \r\n {ex.Message} \r\n {ex.StackTrace}", BELogger.ERROR);
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
                BEGlobals.Log(_LOGFILE, $"\r\n Start Exception: \r\n {ex.Message} \r\n {ex.StackTrace}", BELogger.ERROR);
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
                BEGlobals.Log(_LOGFILE, $"\r\n Stop Exception: \r\n {ex.Message} \r\n {ex.StackTrace}", BELogger.ERROR);
            }
        }
    }
}
