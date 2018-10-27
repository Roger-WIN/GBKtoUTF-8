using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp
{
    public class Choose
    {
        private List<string> Files { get; set; } // 转换文件路径
        private string Directory { get; set; } // 转换文件夹路径
        private string Output { get; set; } // 输出文件夹路径

        public List<string> ChooseFiles()
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = true, // 允许选择多个文件
                Title = "选择待转换文件",
                Filter = "所有文本文件(*.*)|*.*"
            }; // 打开文件对话框

            if (fileDialog.ShowDialog() == DialogResult.OK) Files = new List<string>(fileDialog.FileNames);
            return Files;
        }

        public (string directory, List<string> files) ChooseDirectory()
        {
            var folderDialog = new FolderBrowserDialog
            {
                Description = "选择待转换文件夹",
                ShowNewFolderButton = false // 不允许在该对话框中新建文件夹
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                Directory = folderDialog.SelectedPath;

                var theFolder = new DirectoryInfo(Directory); // 定位到转换文件夹
                var theFiles = theFolder.GetFiles(); // 获取该目录下的所有文件

                if (theFiles.Length > 0) // 该目录非空
                {
                    Files = new List<string>(); // 创建文件列表

                    foreach (var file in theFiles) Files.Add(file.FullName); // 将该目录下的所有文件添加到文件列表
                }
            }

            return (Directory, Files);
        }

        public string ChooseOutput()
        {
            var folderDialog = new FolderBrowserDialog
            {
                Description = "选择输出文件夹"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK) Output = folderDialog.SelectedPath;

            return Output;
        }
    }
}