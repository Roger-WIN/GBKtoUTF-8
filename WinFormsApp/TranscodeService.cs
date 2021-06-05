using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WinFormsApp
{
    public class TranscodeService
    {
        private readonly string pathToUploadedFiles;
        private readonly string pathToConvertedFiles;

        public TranscodeService()
        {
            pathToUploadedFiles = "Files/Uploaded/"; // 指定上传的文件在服务中保存的目录
            pathToConvertedFiles = "Files/Converted/"; // 指定转换后的文件在服务中保存的目录

            Directory.CreateDirectory(pathToUploadedFiles);
            Directory.CreateDirectory(pathToConvertedFiles);
        }

        /* 上传文件，返回这些文件在服务上保存的路径 */
        public string[] UploadFiles(string[] filePaths)
        {
            var uploadedFilePaths = filePaths.Select(filePath => UploadFile(filePath));
            return uploadedFilePaths.Any() ? uploadedFilePaths.ToArray() : null; // 若至少有一个文件上传成功，返回其在服务上保存的路径；否则返回空
        }

        public string UploadFile(string filePath)
        {
            try
            {
                var fileBytes = FileManager.FileToByteStream(filePath); // 将文件读取为字节流
                var fileName = Path.GetFileName(filePath); // 获取文件名
                var uploadedFilePath = Path.Combine(pathToUploadedFiles, fileName); // 指定上传的文件在服务中保存的路径
                FileManager.ByteStreamToFile(uploadedFilePath, fileBytes); // 在服务的指定路径创建相同的文件
                return uploadedFilePath;
            }
            catch (ArgumentNullException) // 未选取文件
            {
                throw new ArgumentNullException("未选取文件");
            }
            catch (ArgumentException) // 选取无效
            {
                throw new ArgumentException("选取无效");
            }
            catch (FileNotFoundException) // 未找到该文件
            {
                throw new FileNotFoundException("未找到文件");
            }
        }

        /* 上传文件夹，返回其中的文件在服务上保存的路径。若为空目录，返回空 */
        public string[] UploadFolder(string folderPath, bool recurFlag)
        {
            if (!Directory.Exists(folderPath)) // 目录不存在
            {
                throw new DirectoryNotFoundException("未找到该目录");
            }

            var theFolder = new DirectoryInfo(folderPath); // 定位到选取的文件夹

            List<FileInfo> theFiles;
            // 递归查找文件夹
            if (recurFlag)
            {
                theFiles = new List<FileInfo>();
                FetchFolderFiles(theFolder, theFiles);
            }
            else
            {
                theFiles = FetchFolderFiles(theFolder);
            }

            if (theFiles.Count <= 0) // 该目录为空
            {
                throw new IOException("目录为空");
            }

            return UploadFiles(theFiles.Select(file => file.FullName).ToArray()); // 将该目录下的所有文件上传
        }

        // 获取文件夹下的文件
        private List<FileInfo> FetchFolderFiles(DirectoryInfo rootDir) => new List<FileInfo>(rootDir.GetFiles());

        // 获取文件夹（及其子文件夹）下的文件
        private void FetchFolderFiles(DirectoryInfo rootDir, List<FileInfo> fileList)
        {
            // 获取子文件，并添加到集合中
            var files = rootDir.GetFiles();
            if (files != null && files.Length > 0)
            {
                fileList.AddRange(files);
            }

            // 获取子文件夹
            var subDirs = rootDir.GetDirectories();
            if (subDirs != null && subDirs.Length > 0)
            {
                // 对每个子文件夹递归执行当前方法
                Array.ForEach(subDirs, subDir => FetchFolderFiles(subDir, fileList));
            }
        }

        /* 转换指定路径的文件，并返回转换后的文件保存在服务上的路径 */
        public string[] TranscodeFiles(string[] filePaths, bool bomFlag, bool overrideFlag)
        {
            var convertedFilePaths = filePaths.Select(filePath => TranscodeFile(filePath, bomFlag, overrideFlag));
            return convertedFilePaths.Any() ? convertedFilePaths.ToArray() : null; // 若至少有一个文件转换成功，返回转换后的文件保存在服务上的路径；否则返回空
        }

        public string TranscodeFile(string filePath, bool bomFlag, bool overrideFlag)
        {
            try
            {
                var fileName = Path.GetFileName(filePath); // 获取原文件的文件名（包含扩展名）
                var originalFileBytes = FileManager.FileToByteStream(filePath); // 将文件读取为字节流
                var targetFileBytes = Transcode.TranscodeByteStream(originalFileBytes, bomFlag); // 转换字节流
                if (!overrideFlag) // 不覆盖原文件
                {
                    var suffix = " - [UTF-8" + (bomFlag ? " with BOM" : string.Empty) + "]"; // 指定目标文件名中的后缀，与是否包含 BOM 有关
                    var extension = Path.GetExtension(filePath); // 获取原文件的扩展名
                    fileName = Path.GetFileNameWithoutExtension(filePath); // 获取原文件的文件名（不包含扩展名）
                    fileName += suffix; // 向目标文件名中添加后缀
                    fileName += extension; // 向目标文件名补齐扩展名
                }
                var convertedFilePath = Path.Combine(pathToConvertedFiles, fileName); // 指定转换后的文件在服务中保存的路径
                FileManager.ByteStreamToFile(convertedFilePath, targetFileBytes); // 在服务的指定路径创建目标文件
                return convertedFilePath;
            }
            catch (ArgumentNullException) // 文件路径为空
            {
                throw new ArgumentNullException("文件路径为空");
            }
            catch (ArgumentException) // 文件路径无效
            {
                throw new ArgumentException("文件路径无效");
            }
            catch (FileNotFoundException) // 未找到文件
            {
                throw new FileNotFoundException("未找到文件");
            }
            catch (FormatException) // 该文件不是文本文件
            {
                throw new FormatException("文件「" + Path.GetFileName(filePath) + "」不是文本文件，不可转换");
            }
        }

        /* 从服务上的指定路径下载文件 */
        public void DownLoadFiles(string[] filePaths, string downloadPath)
        {
            foreach (var originalFilePath in filePaths)
            {
                DownLoadFile(originalFilePath, downloadPath);
            }
        }

        public void DownLoadFile(string filePath, string downloadPath)
        {
            if (!Directory.Exists(downloadPath))
            {
                throw new DirectoryNotFoundException("未找到该目录");
            }

            try
            {
                var fileName = Path.GetFileName(filePath);
                var targetFilePath = Path.Combine(downloadPath, fileName);
                FileManager.ByteStreamToFile(targetFilePath, FileManager.FileToByteStream(filePath));
            }
            catch (ArgumentNullException) // 文件路径为空
            {
                throw new ArgumentNullException("文件路径为空");
            }
            catch (ArgumentException) // 文件路径无效
            {
                throw new ArgumentException("文件路径无效");
            }
            catch (FileNotFoundException) // 未找到文件
            {
                throw new FileNotFoundException("未找到文件");
            }
        }

        /* 清除缓存文件 */
        public void ClearFiles()
        {
            var filesDir = new DirectoryInfo("Files/"); // 指定缓存文件夹的路径
            if (Directory.Exists(filesDir.FullName)) // 若该缓存文件夹存在（未被意外删除）
            {
                filesDir.Delete(true); // 删除缓存文件夹（包含子目录和文件）
            }
        }
    }
}