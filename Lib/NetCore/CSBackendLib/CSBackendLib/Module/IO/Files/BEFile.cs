namespace BEBackendLib.Module.IO.Files
{
    public static class BEFile
    {
        public static bool BECheckExisted(this string pathFile)
        {
            bool res = false;
            try
            {
                if (pathFile.Length == 0)
                    throw new Exception("Path File is nullOrEmpty, please check again!");

                res = File.Exists(pathFile);
            }
            catch (Exception) { throw; }
            finally { }
            return res;
        }


        public static bool BECheckLocked(this string pathFile)
        {
            bool res = false;
            FileInfo? file = null;
            try
            {
                if (pathFile.BECheckExisted())
                {
                    file = new FileInfo(pathFile);
                    FileStream? stream = null;

                    try
                    {
                        stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    }
                    catch (IOException)
                    {
                        res = true;
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
            }
            catch (Exception) { throw; }
            finally { }
            return res;
        }

        public static string BERenameExtension(this string? path, string extOld, string extNew)
        {
            object? obj = new object();
            string? newFile = string.Empty;

            try
            {
                lock (obj)
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (!path.BECheckLocked())
                        {
                            FileInfo? fileInfo = new FileInfo(path);
                            if (fileInfo is not null)
                            {
                                if (Path.GetExtension(path).ToLower() == extOld)
                                {
                                    newFile = Path.ChangeExtension(path, extNew);
                                    Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(path, Path.GetFileName(newFile));
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


        public static string[]? BEGetFiles(this string pathFolder, string searchPattern = "*", int numberFiles = 0, SearchOption search = SearchOption.AllDirectories)
        {
            DirectoryInfo? directory = null;
            string[]? res = null;
            try
            {
                if (numberFiles > 0)
                    res = Directory.EnumerateFiles(pathFolder, searchPattern).Take(numberFiles).OrderBy(filename => filename).ToArray();

                else
                {
                    directory = new DirectoryInfo(pathFolder);
                    FileInfo[]? fileInfos = directory.GetFiles(searchPattern, search).OrderBy(t => t.Name).ToArray();
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

        public static string BEReadAllText(this string pathFile)
        {
            string res = string.Empty;
            try
            {
                if (pathFile.Length == 0)
                    throw new Exception("Path File is nullOrEmpty, please check again!");

                if (pathFile.BECheckLocked())
                    throw new Exception("File is Locked, please check again!");

                res = File.ReadAllText(pathFile);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return res;
        }

        public static byte[]? BEReadAllByte(this string pathFile)
        {
            byte[]? res = null;
            try
            {
                if (pathFile.Length == 0)
                    throw new Exception("Path File is nullOrEmpty, please check again!");

                if (pathFile.BECheckLocked())
                    throw new Exception("File is Locked, please check again!");

                res = File.ReadAllBytes(pathFile);
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return res;
        }

        public static bool BEWriteAllText(this string pathFile, string contents)
        {
            bool res = false;
            try
            {
                if (pathFile.Length == 0)
                    throw new Exception("Path File is nullOrEmpty, please check again!");

                if (pathFile.BECheckLocked())
                    throw new Exception("File is Locked, please check again!");

                File.WriteAllText(pathFile, contents);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return res;
        }

        public static bool BEWriteAllBytes(this string pathFile, byte[] contents)
        {
            bool res = false;
            try
            {
                if (pathFile.Length == 0)
                    throw new Exception("Path File is nullOrEmpty, please check again!");

                if (pathFile.BECheckLocked())
                    throw new Exception("File is Locked, please check again!");

                File.WriteAllBytes(pathFile, contents);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return res;
        }

        public static bool BEDeleteFile(this string pathFile)
        {
            bool res = false;
            try
            {
                if (pathFile.Length == 0)
                    throw new Exception("Path File is nullOrEmpty, please check again!");

                if (pathFile.BECheckLocked())
                    throw new Exception("File is Locked, please check again!");

                File.Delete(pathFile);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally { }
            return res;
        }
    }
}
