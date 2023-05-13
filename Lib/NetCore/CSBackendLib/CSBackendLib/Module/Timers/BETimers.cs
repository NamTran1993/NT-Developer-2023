
using System.Timers;
using Timer = System.Timers.Timer;
namespace BEBackendLib.Module.Timers
{
    public abstract class BETimers
    {
        protected Timer? _tmrTimer = null;

        protected BETimers(double interval)
        {
            _tmrTimer = new Timer(interval);
            _tmrTimer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
        }

        protected void Start()
        {
            try
            {
                if (_tmrTimer is not null && !_tmrTimer.Enabled)
                    _tmrTimer.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Stop()
        {
            try
            {
                if (_tmrTimer is not null)
                    _tmrTimer.Stop();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Release()
        {
            try
            {
                if (_tmrTimer is not null)
                {
                    _tmrTimer.Dispose();
                    _tmrTimer = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract void Timer_Elapsed(object? source, ElapsedEventArgs e);
    }
}
