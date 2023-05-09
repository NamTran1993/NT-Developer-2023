
using Newtonsoft.Json;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Xml.Serialization;

namespace BEBackendLib.Module.Extensions
{
    public static class BEExtension
    {
        #region String => Int; Int => String
        public static int BEToInt32(this string obj)
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

        public static long BEToLong(this string obj)
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

        public static string BEToString(this int obj)
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

        public static int[]? BEToArrayInt32(this string[] obj)
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

        public static string[]? BEToArrayString(this int[] obj)
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
        public static string BEObjectToJson(this object obj)
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

        public static T? BEJsonToObject<T>(this string obj)
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
        public static T? BEXMLToObject<T>(this string xml)
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


        public static string BEObjectToXML(this object obj)
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
        public static bool BEIsExistFile(this string pathFile)
        {
            try
            {
                if (!string.IsNullOrEmpty(pathFile) && File.Exists(pathFile))
                    return true;

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string BEFileReadAllText(this string pathFile)
        {
            string res = string.Empty;
            try
            {
                if (!pathFile.BEIsExistFile())
                    throw new Exception("File not exist!");

                res = File.ReadAllText(pathFile);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool BEFileWriteAllText(this string pathFile, string content)
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

        public static bool BEFileWriteAllByte(this string pathFile, byte[] content)
        {
            try
            {
                if (content is not null)
                {
                    File.WriteAllBytes(pathFile, content);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool BEWriteByteToFile(this string? fileName, byte[] pdata)
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

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static bool BEFileDelete(this string pathFile)
        {
            try
            {
                if (pathFile.BEIsExistFile())
                {
                    File.SetAttributes(pathFile, FileAttributes.Normal);
                    File.Delete(pathFile);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void BEFileRename(this FileInfo? fileInfo, string newName)
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


        public static string[]? BEGetFiles(this string pathFolder, string searchPattern = "*", int numberFiles = 0)
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


        public static bool BEIsFileLocked(this string? path)
        {
            if (!string.IsNullOrEmpty(path) && path.BEIsExistFile())
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

        public static string BERenameExtension(this string? path, string extensionOld, string extensionNew, string functions, ref bool res)
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
                        if (!path.BEIsFileLocked())
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

        public static bool BEIsExistFolder(this string pathFolder)
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

        public static string BEReadAllTextFile(this string pathFile)
        {
            try
            {
                if (pathFile.BEIsExistFile())
                    return File.ReadAllText(pathFile);

                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static bool BECreateFolder(this string pathFolder)
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

        public static string[]? BEGetDirectories(this string pathFolder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
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
        public static IEnumerable<T> BEDistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
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
        public static byte[]? BEConvertbase64ToByteArray(this string base64)
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
        public static IEnumerable<T> BEDequeue<T>(this Queue<T> queues, int count)
        {
            for (int i = 0; i < count && queues.Count > 0; i++)
            {
                yield return queues.Dequeue();
            }
        }

        public static string BERemoveSpaces(this string obj)
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

