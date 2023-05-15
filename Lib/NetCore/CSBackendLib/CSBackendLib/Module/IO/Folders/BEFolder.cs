namespace BEBackendLib.Module.IO.Folders
{
    public static class BEFolder
    {
        public static bool BECheckExisted(this string pathFolder)
        {
            bool res = false;
            try
            {
                if (pathFolder.Length == 0)
                    throw new Exception("Path pathFolder is nullOrEmpty, please check again!");

                res = Directory.Exists(pathFolder);
            }
            catch (Exception) { throw; }
            finally { }
            return res;
        }

        public static bool BECreateFolder(this string pathFolder)
        {
            bool res = false;
            try
            {
                Directory.CreateDirectory(pathFolder);
                res = true; ;
            }
            catch (Exception) { throw; }
            finally { }
            return res;
        }

        public static string[]? BEGetDirectories(this string pathFolder, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            DirectoryInfo? directory = null;
            string[]? res = null;
            try
            {
                if (!pathFolder.BECheckExisted())
                    throw new Exception("Path Folder not existed, please check again!");

                directory = new DirectoryInfo(pathFolder);

                var folderInfos = directory.GetDirectories(searchPattern, searchOption);
                if (folderInfos is not null)
                    res = folderInfos.Where(x => x.Exists).Select(f => f.FullName).ToArray();
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

        public static bool BEDeleteFolder(this string pathFolder, bool bRecursive = true)
        {
            bool res = false;
            try
            {
                if (!pathFolder.BECheckExisted())
                    throw new Exception("Path Folder not existed, please check again!");

                Directory.Delete(pathFolder, bRecursive);
                res = true;
            }
            catch (Exception) { throw; }
            finally { }
            return res;
        }
    }
}
