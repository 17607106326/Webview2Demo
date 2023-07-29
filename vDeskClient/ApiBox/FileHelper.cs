using System.Text;

namespace NetCore.Common.Files
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="conents">文件内容</param>
        /// <param name="directoryPath">文件所在的文件夹地址</param>
        public static void WriteFile(string conents, string filePath)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(filePath, true, Encoding.UTF8);
                writer.Write(conents);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// 获取文件夹及其子文件下的素有文件
        /// </summary>
        /// <param name="currentDirectory"></param>
        /// <param name="reportfiles"></param>
        public static void GetAllFiles(string currentDirectory, ref List<string> reportfiles)
        {
            DirectoryInfo directoryInfo = new(currentDirectory);

            if (!directoryInfo.Exists) return;

            DirectoryInfo dir = directoryInfo;
            //不是目录 
            if (dir == null) return;

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                //是文件 
                if (file != null)
                {
                    reportfiles.Add(file.FullName);
                }
                //对于子目录，进行递归调用 
                else
                {
                    GetAllFiles(files[i].FullName, ref reportfiles);
                }
            }
        }
    }
}
