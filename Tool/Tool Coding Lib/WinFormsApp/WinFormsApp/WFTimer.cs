using BEBackendLib.Module.Enums;
using BEBackendLib.Module.Globals;
using BEBackendLib.Module.Timers;
using System.Timers;

namespace WinFormsApp
{
    public class WFTimer : BETimers
    {
        private string _LOGFILE = "WFTimer.log";
        private double _interval = 0;

        public WFTimer(double interval) : base(interval)
        {
            _interval = interval;
        }

        protected override void Timer_Elapsed(object? source, ElapsedEventArgs e)
        {
            try
            {
                BEGlobals.Log(_LOGFILE, $"\r\n - Timer_Elapsed, interval: {_interval} (milliseconds)", BELogger.TRACE);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StartTimer()
        {
            try
            {
                BEGlobals.Log(_LOGFILE, $"\r\n - StartTimer, interval: {_interval} (milliseconds)", BELogger.TRACE);
                Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StopTimer()
        {
            try
            {
                BEGlobals.Log(_LOGFILE, $"\r\n - StopTimer, interval: {_interval} (milliseconds)", BELogger.TRACE);
                Stop();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RelaseTimer()
        {
            try
            {
                BEGlobals.Log(_LOGFILE, $"\r\n - ReleaseTimer, interval: {_interval} (milliseconds)", BELogger.TRACE);
                Release();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
