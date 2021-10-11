﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WinFormsApp
{
    public class TranscodeService
    {
        /**
         * 上传的文件在服务中保存的目录
         */
        private const string DIR_FOR_UPLOADED_FILES = "Files/Uploaded/";

        /**
         * 转换后的文件在服务中保存的目录
         */
        private const string DIR_FOR_CONVERTED_FILES = "Files/Converted/";

        private readonly FileManager fileManager = new FileManager();

        private readonly Transcode transcode = new Transcode();

        public TranscodeService()
        {
            Directory.CreateDirectory(DIR_FOR_UPLOADED_FILES);
            Directory.CreateDirectory(DIR_FOR_CONVERTED_FILES);
        }

        /* 上传文件，返回这些文件在服务上保存的路径 */
        public string[] UploadFiles(IEnumerable<string> filePaths)
        {
            var uploadedFilePaths = TrimNull(filePaths.Select(filePath => UploadFile(filePath)));
            // 若至少有一个文件上传成功，返回其在服务上保存的路径；否则返回空
            return uploadedFilePaths.Any() ? uploadedFilePaths.ToArray() : null;
        }

        private string UploadFile(string filePath)
        {
            try
            {
                // 获取文件名
                var fileName = Path.GetFileName(filePath);
                // 指定上传的文件在服务中保存的路径
                var uploadedFilePath = Path.Combine(DIR_FOR_UPLOADED_FILES, fileName);
                // 在服务的指定路径创建相同的文件
                File.Copy(filePath, uploadedFilePath, true);
                return uploadedFilePath;
            }
            // 未选取文件
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("未选取文件");
            }
            // 选取无效
            catch (ArgumentException)
            {
                throw new ArgumentException("选取无效");
            }
            // 未找到该文件
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("未找到文件");
            }
        }

        /* 上传文件夹，返回其中的文件在服务上保存的路径。若为空目录，返回空 */
        public string[] UploadFolder(string folderPath, bool isRecursive)
        {
            // 目录不存在
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException("未找到该目录");
            }

            // 定位到选取的文件夹
            var folder = new DirectoryInfo(folderPath);

            List<FileInfo> files;
            // 递归查找文件夹
            if (isRecursive)
            {
                files = new List<FileInfo>();
                FetchFolderFiles(folder, files);
            }
            else
            {
                files = FetchFolderFiles(folder);
            }

            // 该目录为空
            if (!files.Any())
            {
                throw new IOException("目录为空");
            }

            // 将该目录下的所有文件上传
            return UploadFiles(files.Select(file => file.FullName));
        }

        // 获取文件夹下的文件
        private List<FileInfo> FetchFolderFiles(DirectoryInfo dir) => new List<FileInfo>(dir.GetFiles());

        // 获取文件夹（及其子文件夹）下的文件
        private void FetchFolderFiles(DirectoryInfo dir, List<FileInfo> fileList)
        {
            // 获取子文件，并添加到集合中
            var files = dir.GetFiles();
            if (files.Any())
            {
                fileList.AddRange(files);
            }

            // 获取子文件夹
            var subDirs = dir.GetDirectories();
            if (subDirs.Any())
            {
                // 对每个子文件夹递归执行当前方法
                Array.ForEach(subDirs, subDir => FetchFolderFiles(subDir, fileList));
            }
        }

        /* 转换指定路径的文件，并返回转换后的文件保存在服务上的路径 */
        public string[] TranscodeFiles(IEnumerable<string> filePaths, bool hasBom, bool isOverriden)
        {
            var convertedFilePaths = TrimNull(filePaths.Select(filePath => TranscodeFile(filePath, hasBom, isOverriden)));
            // 若至少有一个文件转换成功，返回转换后的文件保存在服务上的路径；否则返回空
            return convertedFilePaths.Any() ? convertedFilePaths.ToArray() : null;
        }

        private string TranscodeFile(string filePath, bool hasBom, bool isOverriden)
        {
            try
            {
                // 该文件不是文本文件
                if (!fileManager.IsTextFile(filePath))
                {
                    return null;
                }

                // 获取原文件的文件名（包含扩展名）
                var fileName = Path.GetFileName(filePath);
                // 将文件读取为字节流
                var originalFileBytes = fileManager.FileToByteStream(filePath);
                // 转换字节流
                var targetFileBytes = transcode.TranscodeByteStream(originalFileBytes);
                // 不覆盖原文件
                if (!isOverriden)
                {
                    // 指定目标文件名中的后缀，与是否包含 BOM 有关
                    var suffix = " - [UTF-8" + (hasBom ? " with BOM" : string.Empty) + "]";
                    // 获取原文件的扩展名
                    var extension = Path.GetExtension(filePath);
                    // 获取原文件的文件名（不包含扩展名）
                    fileName = Path.GetFileNameWithoutExtension(filePath);
                    // 向目标文件名中添加后缀
                    fileName += suffix;
                    // 向目标文件名补齐扩展名
                    fileName += extension;
                }
                // 指定转换后的文件在服务中保存的路径
                var convertedFilePath = Path.Combine(DIR_FOR_CONVERTED_FILES, fileName);
                // 在服务的指定路径创建目标文件
                fileManager.ByteStreamToFile(convertedFilePath, targetFileBytes, hasBom);
                return convertedFilePath;
            }
            // 文件路径为空
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("文件路径为空");
            }
            // 文件路径无效
            catch (ArgumentException)
            {
                throw new ArgumentException("文件路径无效");
            }
            // 未找到文件
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("未找到文件");
            }
        }

        /* 从服务上的指定路径下载文件 */
        public void DownLoadFiles(IEnumerable<string> filePaths, string downloadPath) => Array.ForEach(filePaths.ToArray(), originalFilePath => DownLoadFile(originalFilePath, downloadPath));

        private void DownLoadFile(string filePath, string downloadPath)
        {
            if (!Directory.Exists(downloadPath))
            {
                throw new DirectoryNotFoundException("未找到该目录");
            }

            try
            {
                var fileName = Path.GetFileName(filePath);
                var targetFilePath = Path.Combine(downloadPath, fileName);
                File.Copy(filePath, targetFilePath, true);
            }
            // 文件路径为空
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("文件路径为空");
            }
            // 文件路径无效
            catch (ArgumentException)
            {
                throw new ArgumentException("文件路径无效");
            }
            // 未找到文件
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("未找到文件");
            }
        }

        /* 清除缓存文件 */
        public void ClearTempFiles()
        {
            // 指定缓存文件夹的路径
            var tempDir = new DirectoryInfo("Files/");
            // 若该缓存文件夹存在（未被意外删除）
            if (tempDir.Exists)
            {
                // 删除缓存文件夹（包含子目录和文件）
                tempDir.Delete(true);
            }
        }

        /* 从集合中移除空元素 */
        private IEnumerable<T> TrimNull<T>(IEnumerable<T> collection) => collection.Where(c => c != null);
    }
}