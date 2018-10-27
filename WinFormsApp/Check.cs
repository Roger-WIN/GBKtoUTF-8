using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp
{
    /* 检查文件类型，是否是文本文件 */
    public class Check
    {
        public Check()
        {
            TextFiles = new List<string>();
            BinaryFileNames = new List<string>();
        }

        private List<string> TextFiles { get; } // 文本文件
        private List<string> BinaryFileNames { get; set; } // 二进制文件的文件名

        // 检查指定路径的文件是否是文本文件
        private bool IsTextFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); // 根据路径，打开文件并读取
            var flag = true; // 是否是文本文件的标识。首先假设是
            try
            {
                for (var i = 0; i < Convert.ToInt32(fileStream.Length); i++) // 从文件的开头开始读取
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

        // 从若干文件中过滤出文本文件
        public List<string> FilterTextFile(ref List<string> files)
        {
            if (files == null) // 若没有文件
                return null; // 返回空

            foreach (var file in files) // 在若干文件中检查
                if (IsTextFile(file)) // 当前文件是文本文件
                    TextFiles.Add(file); // 加入到文本文件列表中
                else // 当前文件是二进制文件
                    BinaryFileNames.Add(Path.GetFileName(file)); // 加入到二进制文件文件名的列表中

            files = TextFiles; // 将过滤后的文件列表（即文本文件列表）重新赋值给传入的文件列表

            if (BinaryFileNames.Count < 1) BinaryFileNames = null;

            return BinaryFileNames;
        }
    }
}