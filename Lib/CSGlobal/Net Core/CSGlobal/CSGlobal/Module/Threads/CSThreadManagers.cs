using CSGlobal.Global;

namespace CSGlobal.Module.Threads
{
    public abstract class CSThreadManagers
    {
        private CSTHREAD _status = CSTHREAD.NONE;
        private int _interval = 0;
        private string _sessionID = string.Empty;
        private int _sleepTime = 0;

        private Dictionary<string, CSTHREAD>? _dicThreadManagers = null;
        public string SessionID { get => _sessionID; }
        public Dictionary<string, CSTHREAD>? ThreadManagers { get => _dicThreadManagers; }
        public CSTHREAD Status { get => _status; set => _status = value; }

        public CSThreadManagers(int interval, int sleepTime)
        {
            _dicThreadManagers = new Dictionary<string, CSTHREAD>();
            _status = CSTHREAD.STOP;
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
                _status = CSTHREAD.RUNNING;
                _sessionID = CSGlobals.CreateGUID(CSGUID.REMOVE_LINE);
                _dicThreadManagers?.Add(_sessionID, _status);
                
                if (_status == CSTHREAD.STOP)
                    return;

                DoWorking();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetStatusProcessing(string sessionID, CSTHREAD status)
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
                        if (ss.Key == sessionID)
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
                        CSTHREAD status = ite.Value;

                    PAUSE:
                        if (status == CSTHREAD.PAUSE)
                        {
                            Thread.Sleep(_sleepTime);
                            goto PAUSE;
                        }

                        while (status == CSTHREAD.RUNNING || status == CSTHREAD.RESUME)
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
