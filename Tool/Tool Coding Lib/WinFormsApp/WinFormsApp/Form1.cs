

using BEBackendLib.Module.Communicates;
using BEBackendLib.Module.Enums;
using BEBackendLib.Module.Globals;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private string _LOGFILE = "Form1.log";
        private string _baseDic = string.Empty;

        private WFTimer? _timers = null;
        private WFThread? _threadMan = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _baseDic = AppDomain.CurrentDomain.BaseDirectory;
            BEGlobals.InitFolderLog(_baseDic);
        }


        private void btnLog_Click(object sender, EventArgs e)
        {
            try
            {
                BEGlobals.Log(_LOGFILE, $"\r\n btnLog_Click \r\n - Path: {_baseDic}", BELogger.TRACE);
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
                    string guid = BEGlobals.CreateGUID(BEGuid.REMOVE_LINE, true);
                }

                {
                    string guid = BEGlobals.CreateGUID(BEGuid.DEFAULT, true);
                }

                {
                    string guid = BEGlobals.CreateGUID(BEGuid.TIME, false);
                }

                {
                    string guid = BEGlobals.CreateGUID(BEGuid.TIME, true);
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

        private void btnThread_Click(object sender, EventArgs e)
        {
            try
            {
                if (_threadMan is null)
                    _threadMan = new WFThread(2000, 100);

                _threadMan?.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThreStop_Click(object sender, EventArgs e)
        {
            try
            {
                _threadMan?.Stop();
                _threadMan = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnGetHttp_Click(object sender, EventArgs e)
        {
            try
            {
                BERequestModel models = new BERequestModel()
                {
                    Method = BEMethod.GET,
                    Url = "https://api.github.com/users/hadley/orgs",
                    Timeout = 120,
                    HeaderParams = new List<HeaderParam>() { new HeaderParam() { Name = "User-Agent", Value = "Other" } }.ToArray()
                };

                BEHttpClient? httpClient = new BEHttpClient(models);
                httpClient.InitRequest();
                Task<string?> res = httpClient.GetResponse();
                await res;

                if (res is not null)
                {
                    string? content = res.Result;
                }

                httpClient = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}