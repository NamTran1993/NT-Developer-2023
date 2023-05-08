using CSGlobal.Module.Extensions;
using System.Text;

namespace CSGlobal.Global
{
    public class CSGlobals
    {
        // -----
        public static string _PATH_LOG = string.Empty;
        public static long _MAX_SIZE_LOGFILE = 10 * 1024 * 1024;
        private static readonly object _object = new object();

        // -----
        public static string _FOLDER_NAME_ERROR = "ERROR";
        public static string _FOLDER_NAME_WARNING = "WARNING";
        public static string _FOLDER_NAME_DEBUG = "DEBUG";
        public static string _FOLDER_NAME_TRACE = "TRACE";

        // -----
        public static void InitFolderLog(string baseDirectory)
        {
            try
            {
                if (!string.IsNullOrEmpty(baseDirectory))
                {
                    _PATH_LOG = Path.Combine(baseDirectory, "LogFiles");
                    if (!Directory.Exists(_PATH_LOG))
                        Directory.CreateDirectory(_PATH_LOG);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Log(string fileName, string content, CSLOGGER typeLogger = CSLOGGER.NONE)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                    return;

                string folderLogFile = Path.Combine(_PATH_LOG, DateTime.UtcNow.ToString("yyyy-MM-dd"));
                string full_path = string.Empty;

                lock (_object)
                {
                    switch (typeLogger)
                    {
                        case CSLOGGER.DEBUG:
                            string folder_debug = Path.Combine(folderLogFile, _FOLDER_NAME_DEBUG);
                            if (!Directory.Exists(folder_debug))
                                Directory.CreateDirectory(folder_debug);
                            full_path = Path.Combine(folder_debug, fileName);
                            break;

                        case CSLOGGER.WARNING:
                            string folder_warning = Path.Combine(folderLogFile, _FOLDER_NAME_WARNING);
                            if (!Directory.Exists(folder_warning))
                                Directory.CreateDirectory(folder_warning);
                            full_path = Path.Combine(folder_warning, fileName);
                            break;

                        case CSLOGGER.ERROR:
                            {
                                string folder_error = Path.Combine(folderLogFile, _FOLDER_NAME_ERROR);
                                if (!Directory.Exists(folder_error))
                                    Directory.CreateDirectory(folder_error);
                                full_path = Path.Combine(folder_error, fileName);
                                break;
                            }

                        case CSLOGGER.TRACE:
                            {
                                string folder_trace = Path.Combine(folderLogFile, _FOLDER_NAME_TRACE);
                                if (!Directory.Exists(folder_trace))
                                    Directory.CreateDirectory(folder_trace);
                                full_path = Path.Combine(folder_trace, fileName);
                                break;
                            }
                        default:
                            {
                                string folder = folderLogFile;
                                if (!Directory.Exists(folder))
                                    Directory.CreateDirectory(folder);
                                full_path = Path.Combine(folder, fileName);
                                break;
                            }
                    }

                    // -- 
                    try
                    {
                        FileInfo? file_info = new FileInfo(full_path);
                        if (file_info.Exists)
                        {
                            long length = file_info.Length;
                            if (length > _MAX_SIZE_LOGFILE)
                            {
                                try
                                {
                                    string[] arrFileName = fileName.Split('.');

                                    if (arrFileName is not null && arrFileName.Length > 0)
                                    {
                                        string newFile = $"{arrFileName[0]}_{DateTime.UtcNow.ToString("yyyy_MMdd_HHmmss")}(yyyyMMddHHmmss).log";
                                        file_info.CSFileRename(newFile);
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }

                        using (FileStream file_stream = File.Open(full_path, FileMode.Append, FileAccess.Write, FileShare.Read))
                        {
                            DateTime dtNow = DateTime.UtcNow;
                            /* Example Log
                             * 12/5/2019 9:42:00 AM - Microsoft VSIX Installer
                               12/5/2019 9:42:00 AM - -------------------------------------------
                             **/
                            string date_time = dtNow.ToString("yyyy-MM-dd hh:mm:ss tt");
                            string line = string.Empty;
                            string fullContent = string.Empty;

                            fullContent = $"{date_time}: {content}\r\n";

                            long offset = file_stream.Seek(0, SeekOrigin.End);
                            ASCIIEncoding? encoding = new ASCIIEncoding();

                            byte[]? arrLogs = encoding.GetBytes(fullContent);
                            file_stream.Write(arrLogs, 0, arrLogs.Length);

                            file_stream.Close();
                            file_stream.Dispose();
                            encoding = null;
                            arrLogs = null;
                            file_info = null;
                        }
                    }
                    catch
                    { }
                }
            }
            catch (Exception)
            { }
        }

        public static string CreateGUID(CSGUID type = CSGUID.DEFAULT, bool isUpper = false)
        {
            string res = string.Empty;
            try
            {
                string guid = Guid.NewGuid().ToString();
                string[] arrGuid = guid.Split('-');
                if (arrGuid is not null && arrGuid.Length > 0)
                {
                    res = type switch
                    {
                        CSGUID.DATE => string.Format("{0}-{1}-{2}", DateTime.Now.ToString("yyyy-MM-dd"), arrGuid[0], arrGuid[1]),
                        CSGUID.TIME => string.Format("{0}-{1}-{2}", DateTime.Now.ToString("hh-mm-ss"), arrGuid[0], arrGuid[1]),
                        CSGUID.DEFAULT_2 => string.Format("{0}-{1}", arrGuid[0], arrGuid[2]),
                        CSGUID.DEFAULT_3 => string.Format("{0}-{1}", arrGuid[0], arrGuid[3]),
                        CSGUID.DEFAULT_4 => string.Format("{0}-{1}", arrGuid[0], arrGuid[4]),
                        CSGUID.REMOVE_LINE => guid.Replace("-", ""),
                        _ => guid
                    };

                    if (isUpper)
                        res = res.ToUpper();
                    return res;
                }
            }
            catch
            { }
            return res;
        }
    }
}
