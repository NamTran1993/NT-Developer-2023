using Newtonsoft.Json;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Xml.Serialization;

namespace CSGlobal.Module.Extensions
{
    public static class CSExtensions
    {
        #region String => Int; Int => String
        public static int CSToInt32(this string obj)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static long CSToLong(this string obj)
        {
            try
            {
                return long.Parse(obj.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string CSToString(this int obj)
        {
            try
            {
                return obj.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int[]? CSToArrayInt32(this string[] obj)
        {
            try
            {
                return Array.ConvertAll(obj, s => int.Parse(s));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string[]? CSToArrayString(this int[] obj)
        {
            try
            {
                return Array.ConvertAll(obj, s => s.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region JSON
        public static string CSObjectToJson(this object obj)
        {
            try
            {
                if (obj is not null)
                    return JsonConvert.SerializeObject(obj);
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T? CSJsonToObject<T>(this string obj)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion



        #region XML
        public static T? CSXMLToObject<T>(this string xml)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                using (var stringReader = new StringReader(xml))
                {
                    return (T?)xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static string CSObjectToXML(this object obj)
        {
            string res = string.Empty;
            try
            {
                if (obj is not null)
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    var sb = new StringBuilder();
                    using (TextWriter writer = new StringWriter(sb))
                    {
                        serializer.Serialize(writer, obj);
                    }

                    res = sb.ToString();
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion



        #region File + Folder
        public static bool CSIsExistFile(this string pathFile)
        {
            try
            {
                if (!string.IsNullOrEmpty(pathFile) && File.Exists(pathFile))
                    return true;
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public static string CSFileReadAllText(this string pathFile)
        {
            string res = string.Empty;
            try
            {
                if (!pathFile.CSIsExistFile())
                    throw new Exception("File not exist!");

                res = File.ReadAllText(pathFile);

            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public static bool CSFileWriteAllText(this string pathFile, string content)
        {
            try
            {
                File.WriteAllText(pathFile, content);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool CSFileWriteAllByte(this string pathFile, byte[] content)
        {
            try
            {
                if (content is not null)
                {
                    File.WriteAllBytes(pathFile, content);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public static bool CSWriteByteToFile(this string? fileName, byte[] pdata)
        {
            bool res = false;
            try
            {
                if (fileName is not null && pdata is not null && pdata.Length > 0)
                {
                    using (var stream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        using (var mmf = MemoryMappedFile.CreateFromFile(stream, null, pdata.Length, MemoryMappedFileAccess.ReadWrite, HandleInheritability.Inheritable, true))
                        {
                            using (var view = mmf.CreateViewAccessor())
                            {
                                view.WriteArray(0, pdata, 0, pdata.Length);
                            }
                        }
                        stream.SetLength(pdata.Length);
                    }

                    res = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return res;
        }


        public static bool CSFileDelete(this string pathFile)
        {
            try
            {
                if (pathFile.CSIsExistFile())
                {
                    File.SetAttributes(pathFile, FileAttributes.Normal);
                    File.Delete(pathFile);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }

        public static void CSFileRename(this FileInfo? fileInfo, string newName)
        {
            try
            {
                fileInfo?.MoveTo(fileInfo?.Directory?.FullName + "\\" + newName);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static string[]? CSGetFiles(this string pathFolder, string searchPattern = "*", int numberFiles = 0)
        {
            DirectoryInfo? directory = null;
            string[]? res = null;
            try
            {
                if (numberFiles > 0)
                {
                    res = Directory.EnumerateFiles(pathFolder, searchPattern)
                                          .Take(numberFiles)
                                          .ToArray();
                }
                else
                {
                    directory = new DirectoryInfo(pathFolder);
                    var fileInfos = directory.GetFiles(searchPattern, SearchOption.AllDirectories)
                                             .OrderBy(t => t.LastWriteTime)
                                             .ToList();

                    if (fileInfos is not null)
                        res = fileInfos.Select(x => x.FullName).ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (directory is not null)
                    directory = null;
            }
            return res;
        }


        public static bool CSIsFileLocked(this string? path)
        {
            if (!string.IsNullOrEmpty(path) && path.CSIsExistFile())
            {
                FileInfo? file = new FileInfo(path);
                FileStream? stream = null;

                try
                {
                    stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch (IOException)
                {
                    return true;
                }
                finally
                {
                    if (stream is not null)
                    {
                        stream.Close();
                        stream = null;
                    }

                    if (file is not null)
                        file = null;
                }
            }
            return false;
        }

        public static string CSRenameExtension(this string? path, string extensionOld, string extensionNew, string functions, ref bool res)
        {
            object? obj = new object();
            string? newFile = string.Empty;
            res = false;
            try
            {
                lock (obj)
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (!path.CSIsFileLocked())
                        {
                            FileInfo? fileInfo = new FileInfo(path);
                            if (fileInfo is not null)
                            {
                                if (Path.GetExtension(path).ToLower() == extensionOld)
                                {
                                    newFile = Path.ChangeExtension(path, extensionNew);
                                    Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(path, Path.GetFileName(newFile));
                                    res = true;
                                }
                            }

                            fileInfo = null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                obj = null;
            }
            return newFile;
        }

        public static bool CSIsExistFolder(this string pathFolder)
        {
            try
            {
                if (Directory.Exists(pathFolder))
                    return true;

                return false;
            }
            catch (Exception)
            {
                throw;
            }     
        }

        public static string CSReadAllTextFile(this string pathFile)
        {
            try
            {
                if (pathFile.CSIsExistFile())
                    return File.ReadAllText(pathFile);

                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }   
        }


        public static bool CSCreateFolder(this string pathFolder)
        {
            try
            {
                Directory.CreateDirectory(pathFolder);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string[]? CSGetDirectories(this string pathFolder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                if (searchOption == SearchOption.TopDirectoryOnly)
                    return Directory.GetDirectories(pathFolder, searchPattern).ToArray();

                var directories = new List<string>(GetDirectories(pathFolder, searchPattern));
                return directories.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<string> GetDirectories(string path, string searchPattern)
        {
            DirectoryInfo? directory = null;
            List<string>? res = new List<string>();
            try
            {
                directory = new DirectoryInfo(path);
                var folderInfos = directory.GetDirectories(searchPattern, SearchOption.AllDirectories);
                if (folderInfos is not null)
                {
                    res = folderInfos.Where(x => x.Exists)
                                     .Select(f => f.FullName)
                                     .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                directory = null;
            }
            return res;
        }
        #endregion


        #region Distinct
        public static IEnumerable<T> CSDistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            try
            {
                return items.GroupBy(property).Select(x => x.First());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region IMAGE
        public static byte[]? CSConvertbase64ToByteArray(this string base64)
        {
            try
            {
                if (!string.IsNullOrEmpty(base64))
                    return Convert.FromBase64String(base64);
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region DIFF
        public static IEnumerable<T> CSDequeue<T>(this Queue<T> queues, int count)
        {
            for (int i = 0; i < count && queues.Count > 0; i++)
            {
                yield return queues.Dequeue();
            }
        }

        public static string CSRemoveSpaces(this string obj)
        {
            string res = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    obj = obj.Trim();
                    res = obj.Replace(" ", "");
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
