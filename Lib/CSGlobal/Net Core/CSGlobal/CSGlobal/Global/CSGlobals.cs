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

        public static void Log(string fileName, string content, CSLOGGER typeLogger = CSLOGGER.NONE, bool bEnd = true)
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
                            string date_time = dtNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            string line = string.Empty;


                            string fullContent = string.Empty;
                            if (bEnd)
                            {
                                line = "----------[END]----------\r\n";
                                fullContent = $"{date_time}{content}\r\n{line}";
                            }
                            else
                                fullContent = $"{date_time}{content}\r\n";

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
    }
}
