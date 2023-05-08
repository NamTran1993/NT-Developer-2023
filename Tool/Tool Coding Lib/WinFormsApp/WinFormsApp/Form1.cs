using CSGlobal.Global;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private string _LOGFILE = "Form1.log";
        private string _baseDic = string.Empty;

        private WFTimer? _timers = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _baseDic = AppDomain.CurrentDomain.BaseDirectory;
            CSGlobals.InitFolderLog(_baseDic);
        }


        private void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                CSGlobals.Log(_LOGFILE, $"\r\n btnLog_Click \r\n - Path: {_baseDic}", CSLOGGER.TRACE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGUID_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    string guid = CSGlobals.CreateGUID(CSGUID.REMOVE_LINE, true);
                }

                {
                    string guid = CSGlobals.CreateGUID(CSGUID.DEFAULT, true);
                }

                {
                    string guid = CSGlobals.CreateGUID(CSGUID.TIME, false);
                }

                {
                    string guid = CSGlobals.CreateGUID(CSGUID.TIME, true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTimersStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (_timers is null)
                    _timers = new WFTimer(1000);

                _timers?.StartTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTimerStop_Click(object sender, EventArgs e)
        {
            try
            {
                _timers?.StopTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                _timers?.RelaseTimer();
                _timers = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}