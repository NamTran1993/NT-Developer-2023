

using BEBackendLib.Module.Communicates;
using BEBackendLib.Module.Enums;
using BEBackendLib.Module.Extensions;
using BEBackendLib.Module.Globals;
using BEBackendLib.Module.Gmail;
using BEBackendLib.Module.IO.Files;
using BEBackendLib.Module.Jwt;
using BEBackendLib.Module.MSSQL;
using System.Data.SqlClient;
using System.Net.Mail;

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

        private async void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonPost = string.Empty;

                WFJSON jso = new WFJSON()
                {
                    Id = 78912,
                    Customer = "Jason Sweet",
                    Price = 18.00,
                    Quantity = 1
                };

                jsonPost = jso.BEObjectToJson();

                BERequestModel models = new BERequestModel()
                {
                    Method = BEMethod.POST,
                    Url = "https://reqbin.com/echo/post/json",
                    Timeout = 120,
                    HeaderParams = new List<HeaderParam>() { new HeaderParam() { Name = "User-Agent", Value = "Other" } }.ToArray(),
                    JsonRequest = jsonPost
                };

                BEHttpClient? httpClient = new BEHttpClient(models);
                httpClient.InitRequest();
                Task<string?> res = httpClient.GetResponse();
                await res;

                if (res is not null)
                {
                    string? content = res.Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSendGmail_Click(object sender, EventArgs e)
        {
            try
            {
                string att = @"h:\NT-Developer-2023\Tool\Tool Coding Lib\WinFormsApp\WinFormsApp\data\att.txt";
                List<Attachment> attachments = new List<Attachment>();

                Attachment at1 = new Attachment(att);
                attachments.Add(at1);


                BEGmailModel eGmailModel = new BEGmailModel()
                {
                    Body = "Hi tran huy nam",
                    Subject = "C# Gmail",
                    Email = "nt.mailnoname93@gmail.com",
                    Password = "",
                    ToEmails = new List<string>() { "tranhuynam93@gmail.com" }.ToArray(),
                    Attachments = attachments.ToArray()
                };

                BEGmail gmail = new BEGmail(eGmailModel);
                gmail.InitGmail();
                gmail.Send();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnJWT_Click(object sender, EventArgs e)
        {
            try
            {
                JwtModel jwtModel = new JwtModel()
                {
                    ValidIssuer = "ValidIssuer",
                    IssuerSigningKey = "this is my custom Secret key for authentication",
                    MinuteExpires = 5
                };

                BEJwt eJwt = new BEJwt(jwtModel);
                string jwtToken = eJwt.GenerateJwt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMSSQL_Click(object sender, EventArgs e)
        {
            try
            {
                BEMSSQLConfig dbConfig = new BEMSSQLConfig()
                {
                    DatabaseName = "dev",
                    DBConfig = new BEDatabase()
                    {
                        //Server = @"WIN10\SQLEXPRESS",
                        UseTrustServerCertificate = false,
                        Server = @"127.0.0.1",
                        InitialCatalog = "db_dev",
                        UID = "sa",
                        PWD = "12345"
                    }
                };

                BEConnectionMSSQL mSSQL = new BEConnectionMSSQL(dbConfig);

                //string sql = $"select * from [dbo].[Customer] with (nolock) ";
                //var data = mSSQL.Execute_Table(sql);

                string procName = "PROC_GetCustomer";
                bool bAll = false;
                int customerID = 1;

                List<SqlParameter> parameters = new List<SqlParameter>() { };
                SqlParameter? pIsAll = mSSQL?.AddParameter("@isAll", System.Data.SqlDbType.Bit, bAll);
                SqlParameter? pCustomerID = mSSQL?.AddParameter("@customerID", System.Data.SqlDbType.Int, customerID);

                if (pIsAll is not null)
                    parameters.Add(pIsAll);

                if (pCustomerID is not null)
                    parameters.Add(pCustomerID);


                var dt = mSSQL?.Execute_StoredProcedure(procName, parameters.ToArray());
                var aa = mSSQL?.ConvertProcParamToString(procName, parameters.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIO_Click(object sender, EventArgs e)
        {
            try
            {
                // ---
                string path = @"h:\NT-Developer-2023\Tool\Tool Coding Lib\WinFormsApp\WinFormsApp\data\att.txt";
                string path1 = @"h:\NT-Developer-2023\Tool\Tool Coding Lib\WinFormsApp\WinFormsApp\data\A.log";
                string path2 = @"h:\CODE\@@@_LEARN_@@@\Service\2023\2023-01-08\build\";


                bool bExist = path.BECheckExisted();
                bool bLocked = path.BECheckLocked();
              //  string pathNew = path1.BERenameExtension(".log", ".txt");

                var files = path2.BEGetFiles("*", 10, SearchOption.TopDirectoryOnly);
                var files1 = path2.BEGetFiles();
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}