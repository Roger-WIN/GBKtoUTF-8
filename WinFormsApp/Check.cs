using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp
{
    public class Check
    {
        public Check()
        {
            TextFiles = new List<string>();
            BinaryFileNames = new List<string>();
        }

        private List<string> TextFiles { get; } // 文本文件
        private List<string> BinaryFileNames { get; set; } // 二进制文件

        private bool IsTextFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var flag = true;
            try
            {
                for (var i = 0; i < Convert.ToInt32(fileStream.Length); i++)
                {
                    var data = Convert.ToByte(fileStream.ReadByte()); // 读取每一个字节
                    if (data == 0) // 只要有一个字节为 0
                    {
                        flag = false; // 不是文本文件
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fileStream.Close(); // 关闭文件
            }

            return flag;
        }

        public List<string> FilterTextFile(ref List<string> files)
        {
            if (files == null)
                return null;

            foreach (var file in files)
                if (IsTextFile(file))
                    TextFiles.Add(file);
                else
                    BinaryFileNames.Add(Path.GetFileName(file));

            files = TextFiles;

            if (BinaryFileNames.Count < 1) BinaryFileNames = null;

            return BinaryFileNames;
        }
    }
}