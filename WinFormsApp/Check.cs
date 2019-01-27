using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            try
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); // 以读权限打开文件并读取
                var fileBytes = new byte[fileStream.Length]; // 利用文件流的长度创建新的字节数组
                fileStream.Read(fileBytes, 0, fileBytes.Length); // 从文件流中读取字节块并写入字节流
                fileStream.Close(); // 关闭文件流
                var fileByteList = fileBytes.ToList(); // 利用字节数组创建字节列表
                return !fileByteList.Contains(0); // 若存在字节 0，则不是文本文件
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("未找到文件");
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
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