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
        public Dictionary<string, string>? UploadFiles(Dictionary<string, string> files)
        {
            var uploadedFiles = new Dictionary<string, string>();
            foreach (var file in files)
            {
                var uploadedFile = UploadFile(file);
                if (uploadedFile != null)
                {
                    uploadedFiles.TryAdd(uploadedFile, file.Value);
                }
            }
            // 若至少有一个文件上传成功，返回其在服务上保存的路径；否则返回空
            return uploadedFiles.Any() ? uploadedFiles : null;
        }

        private string UploadFile(KeyValuePair<string, string> file)
        {
            try
            {
                // 获取文件名
                var fileName = Path.GetFileName(file.Key);
                // 获取目录层次
                var dirLevel = file.Value;
                // 指定上传的文件在服务中保存的路径
                var uploadedDir = Path.Combine(DIR_FOR_UPLOADED_FILES, dirLevel);
                var uploadedFile = Path.Combine(uploadedDir, fileName);
                // 在服务中创建指定的上传文件夹
                Directory.CreateDirectory(uploadedDir);
                // 在服务的指定路径创建相同的文件
                File.Copy(file.Key, uploadedFile, true);
                return uploadedFile;
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

        /* 上传文件，返回这些文件在服务上保存的路径 */
        public string[]? UploadFiles(IEnumerable<string> files)
        {
            var uploadedFiles = TrimNull(files.Select(file => UploadFile(file)));
            // 若至少有一个文件上传成功，返回其在服务上保存的路径；否则返回空
            return uploadedFiles.Any() ? uploadedFiles.ToArray() : null;
        }

        private string UploadFile(string file)
        {
            try
            {
                // 获取文件名
                var fileName = Path.GetFileName(file);
                // 指定上传的文件在服务中保存的路径
                var uploadedFile = Path.Combine(DIR_FOR_UPLOADED_FILES, fileName);
                // 在服务的指定路径创建相同的文件
                File.Copy(file, uploadedFile, true);
                return uploadedFile;
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
        public Dictionary<string, string>? UploadFolder(string folderPath, bool isRecursive)
        {
            // 目录不存在
            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException("未找到该目录");
            }

            // 定位到选取的文件夹
            var folder = new DirectoryInfo(folderPath);

            List<FileInfo> files;
            Dictionary<string, string> fileDirs;
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

            // 该目录不为空
            if (files.Any())
            {
                fileDirs = FetchFolderRelaDirs(folder, files);
            }
            else
            {
                throw new IOException("目录为空");
            }

            // 将该目录下的所有文件上传
            return UploadFiles(fileDirs);
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

        // 获取文件夹下的子文件夹
        private Dictionary<string, string>? FetchFolderRelaDirs(DirectoryInfo root, IEnumerable<FileInfo> fileList) => fileList.ToDictionary(file => file.FullName, file => Path.GetRelativePath(root.FullName, file.Directory.FullName));

        /* 转换指定路径的文件，并返回转换后的文件保存在服务上的路径 */
        public Dictionary<string, string>? TranscodeFiles(Dictionary<string, string>? files, bool hasBom, bool hasSuffix)
        {
            if (files == null)
            {
                return null;
            }

            var convertedFiles = new Dictionary<string, string>();
            foreach (var file in files)
            {
                var convertedFile = TranscodeFile(file, hasBom, hasSuffix);
                if (convertedFile != null)
                {
                    convertedFiles.TryAdd(convertedFile, file.Value);
                }
            }
            // 若至少有一个文件上传成功，返回其在服务上保存的路径；否则返回空
            return convertedFiles.Any() ? convertedFiles : null;
        }

        private string? TranscodeFile(KeyValuePair<string, string> file, bool hasBom, bool hasSuffix)
        {
            try
            {
                // 该文件不是文本文件
                if (!fileManager.IsTextFile(file.Key))
                {
                    return null;
                }

                // 获取原文件的文件名（包含扩展名）
                var fileName = Path.GetFileName(file.Key);
                // 将文件读取为字节流
                var originalFileBytes = fileManager.FileToByteStream(file.Key);
                // 转换字节流
                var targetFileBytes = transcode.TranscodeByteStream(originalFileBytes);
                // 文件名添加后缀
                if (hasSuffix)
                {
                    // 指定目标文件名中的后缀，与是否包含 BOM 有关
                    var suffix = " - [UTF-8" + (hasBom ? " with BOM" : string.Empty) + "]";
                    // 获取原文件的扩展名
                    var extension = Path.GetExtension(file.Key);
                    // 获取原文件的文件名（不包含扩展名）
                    fileName = Path.GetFileNameWithoutExtension(file.Key);
                    // 向目标文件名中添加后缀
                    fileName += suffix;
                    // 向目标文件名补齐扩展名
                    fileName += extension;
                }
                // 获取目录层次
                var dirLevel = file.Value;
                // 指定转换后的文件在服务中保存的路径
                var convertedDir = Path.Combine(DIR_FOR_CONVERTED_FILES, dirLevel);
                var convertedFile = Path.Combine(convertedDir, fileName);
                // 在服务中创建指定的目标文件夹
                Directory.CreateDirectory(convertedDir);
                // 在服务的指定路径创建目标文件
                fileManager.ByteStreamToFile(convertedFile, targetFileBytes, hasBom);
                return convertedFile;
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

        /* 转换指定路径的文件，并返回转换后的文件保存在服务上的路径 */
        public string[]? TranscodeFiles(IEnumerable<string>? files, bool hasBom, bool hasSuffix)
        {
            if (files == null)
            {
                return null;
            }

            var convertedFiles = TrimNull(files.Select(file => TranscodeFile(file, hasBom, hasSuffix)));
            // 若至少有一个文件转换成功，返回转换后的文件保存在服务上的路径；否则返回空
            return convertedFiles.Any() ? convertedFiles.ToArray() : null;
        }

        private string? TranscodeFile(string file, bool hasBom, bool hasSuffix)
        {
            try
            {
                // 该文件不是文本文件
                if (!fileManager.IsTextFile(file))
                {
                    return null;
                }

                // 获取原文件的文件名（包含扩展名）
                var fileName = Path.GetFileName(file);
                // 将文件读取为字节流
                var originalFileBytes = fileManager.FileToByteStream(file);
                // 转换字节流
                var targetFileBytes = transcode.TranscodeByteStream(originalFileBytes);
                // 文件名添加后缀
                if (hasSuffix)
                {
                    // 指定目标文件名中的后缀，与是否包含 BOM 有关
                    var suffix = " - [UTF-8" + (hasBom ? " with BOM" : string.Empty) + "]";
                    // 获取原文件的扩展名
                    var extension = Path.GetExtension(file);
                    // 获取原文件的文件名（不包含扩展名）
                    fileName = Path.GetFileNameWithoutExtension(file);
                    // 向目标文件名中添加后缀
                    fileName += suffix;
                    // 向目标文件名补齐扩展名
                    fileName += extension;
                }
                // 指定转换后的文件在服务中保存的路径
                var convertedFile = Path.Combine(DIR_FOR_CONVERTED_FILES, fileName);
                // 在服务的指定路径创建目标文件
                fileManager.ByteStreamToFile(convertedFile, targetFileBytes, hasBom);
                return convertedFile;
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
        public void DownLoadFiles(Dictionary<string, string>? files, string? downloadPath)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    DownLoadFile(file, downloadPath);
                }
            }
        }

        private void DownLoadFile(KeyValuePair<string, string> file, string? downloadPath)
        {
            if (!Directory.Exists(downloadPath))
            {
                throw new DirectoryNotFoundException("未找到该目录");
            }

            try
            {
                var fileName = Path.GetFileName(file.Key);
                var dirLevel = file.Value;
                var targetDir = Path.Combine(downloadPath, dirLevel);
                var targetFile = Path.Combine(targetDir, fileName);
                Directory.CreateDirectory(targetDir);
                File.Copy(file.Key, targetFile, true);
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
        public void DownLoadFiles(IEnumerable<string>? files, string? downloadPath)
        {
            if (files != null)
            {
                Array.ForEach(files.ToArray(), file => DownLoadFile(file, downloadPath));
            }
        }

        private void DownLoadFile(string file, string? downloadPath)
        {
            if (!Directory.Exists(downloadPath))
            {
                throw new DirectoryNotFoundException("未找到该目录");
            }

            try
            {
                var fileName = Path.GetFileName(file);
                var targetFilePath = Path.Combine(downloadPath, fileName);
                File.Copy(file, targetFilePath, true);
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