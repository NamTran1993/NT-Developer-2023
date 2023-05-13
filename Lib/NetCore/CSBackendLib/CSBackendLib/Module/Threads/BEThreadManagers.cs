

using BEBackendLib.Module.Enums;
using BEBackendLib.Module.Globals;

namespace BEBackendLib.Module.Threads
{
    public abstract class BEThreadManagers
    {
        private BEStatusThread _status = BEStatusThread.NONE;
        private int _interval = 0;
        private string _sessionID = string.Empty;
        private int _sleepTime = 0;

        private Dictionary<string, BEStatusThread>? _dicThreadManagers = null;
        public string SessionID { get => _sessionID; }
        public Dictionary<string, BEStatusThread>? ThreadManagers { get => _dicThreadManagers; }
        public BEStatusThread Status { get => _status; set => _status = value; }

        public BEThreadManagers(int interval, int sleepTime)
        {
            _dicThreadManagers = new Dictionary<string, BEStatusThread>();
            _status = BEStatusThread.STOP;
            _interval = interval;
            _sleepTime = sleepTime;

            // -- TIME is milliseconds --
            if (_interval <= 0)
                _interval = 100;

            if (_sleepTime <= 0)
                _sleepTime = 100;
        }

        protected abstract void MainProcessing();

        protected void StartProcessing()
        {
            try
            {
                _status = BEStatusThread.RUNNING;
                _sessionID = BEGlobals.CreateGUID(BEGuid.REMOVE_LINE);
                _dicThreadManagers?.Add(_sessionID, _status);

                if (_status == BEStatusThread.STOP)
                    return;

                DoWorking();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetStatusProcessing(string sessionID, BEStatusThread status)
        {
            try
            {
                int idx = FindIndexThread(sessionID);
                if (idx > -1)
                {
                    if (_dicThreadManagers is not null)
                        _dicThreadManagers[sessionID] = status;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StopProcessing(string sessionID)
        {
            try
            {
                int idx = FindIndexThread(sessionID);
                if (idx > -1)
                {
                    if (_dicThreadManagers is not null)
                        _dicThreadManagers.Remove(sessionID);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int FindIndexThread(string sessionID)
        {
            try
            {
                if (_dicThreadManagers is not null && _dicThreadManagers.Count > 0)
                {
                    int idx = 0;
                    foreach (var ss in _dicThreadManagers)
                    {
                        if (ss.Key.CompareTo(sessionID) == 0)
                            return idx;
                        idx++;
                    }
                }
            }
            catch { }
            return -1;
        }


        private void DoWorking()
        {
            try
            {
            READ_STATUS:
                if (_dicThreadManagers is not null && _dicThreadManagers.Count > 0)
                {
                    foreach (var ite in _dicThreadManagers)
                    {
                        BEStatusThread status = ite.Value;

                    PAUSE:
                        if (status == BEStatusThread.PAUSE)
                        {
                            Thread.Sleep(_sleepTime);
                            goto PAUSE;
                        }

                        while (status == BEStatusThread.RUNNING || status == BEStatusThread.RESUME)
                        {
                            MainProcessing();
                            Thread.Sleep(_interval);
                            goto READ_STATUS;
                        }
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
