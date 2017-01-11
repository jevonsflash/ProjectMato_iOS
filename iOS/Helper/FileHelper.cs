using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMato.iOS.Helper
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileHelper
    {

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async static Task<bool> IsExistFileAsync(string fileName)
        {
            bool isExistFile = false;
            try
            {
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var library = Path.Combine(documents, "..", "Library");
                var fileFullName = Path.Combine(library, fileName);
                isExistFile = File.Exists(fileFullName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                isExistFile = false;
            }

            return isExistFile;
        }

        /// <summary>
        /// 创建文件目录
        /// </summary>
        /// <param name="dirName">目录名称</param>
        public async static Task<bool> CreateDirectoryAsync(string dirName)
        {
            bool isCreateDir = false;
            try
            {
                //当前应用程序包位置
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var library = Path.Combine(documents, "..", "Library");
                var dirFullName = Path.Combine(library, dirName);
                var info = Directory.CreateDirectory(dirFullName);
                if (info.Exists)
                {
                    isCreateDir = true;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                isCreateDir = false;
            }

            return isCreateDir;
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteFileAsync(string fileName)
        {
            bool isDeleteFile = false;
            try
            {
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var library = Path.Combine(documents, "..", "Library");
                var fileFullName = Path.Combine(library, fileName);
                File.Delete(fileFullName);
                isDeleteFile = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                isDeleteFile = false;
            }
            return isDeleteFile;
        }


        /// <summary>
        /// 创建并写入文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async static Task<bool> CreateAndWriteFileAsync(string fileName, string content)
        {
            bool isCreateFile = false;
            try
            {
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var library = Path.Combine(documents, "..", "Library");
                var fileFullName = Path.Combine(library, fileName);
                File.WriteAllText(fileFullName, content);
                isCreateFile = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                isCreateFile = false;
            }
            return isCreateFile;
        }



        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<string> ReadTxtFileAsync(string fileName)
        {
            string text = string.Empty;
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var library = Path.Combine(documents, "..", "Library");
            var fileFullName = Path.Combine(library, fileName);
            text = File.ReadAllText(fileFullName);
            return text;
        }




        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <returns></returns>
        public async static Task<List<FileInfo>> GetFiles(string folderName)
        {
            try
            {
                var result = new List<string>();
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var library = string.IsNullOrEmpty(folderName) ? Path.Combine(documents, "..", "Library") : Path.Combine(documents, "..", "Library", folderName);
                DirectoryInfo directory = new DirectoryInfo(library);
                var fileinfos = directory.GetFiles();

                return fileinfos.ToList();


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }

        }
    }

}
