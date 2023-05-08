using CSGlobal.Global;
using CSGlobal.Module.Timers;
using System.Timers;

namespace WinFormsApp
{
    public class WFTimer : CSTimer
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
                CSGlobals.Log(_LOGFILE, $"\r\n - Timer_Elapsed, interval: {_interval} (milliseconds)", CSLOGGER.TRACE);
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
                CSGlobals.Log(_LOGFILE, $"\r\n - StartTimer, interval: {_interval} (milliseconds)", CSLOGGER.TRACE);
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
                CSGlobals.Log(_LOGFILE, $"\r\n - StopTimer, interval: {_interval} (milliseconds)", CSLOGGER.TRACE);
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
                CSGlobals.Log(_LOGFILE, $"\r\n - ReleaseTimer, interval: {_interval} (milliseconds)", CSLOGGER.TRACE);
                Release();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
