using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp
{
    public class Transcode
    {
        public void ToAim(List<string> files, string output, bool bomFlag, bool overrideFlag)
        {
            if (files == null || output == null)
                return;

            try
            {
                foreach (var currentFile in files)
                {
                    var content = File.ReadAllText(currentFile, Encoding.GetEncoding(936)); // 以 GBK 编码读取文件
                    var utf8Encoding = new UTF8Encoding(bomFlag); // 是否包含 BOM（byte order mark，字节顺序标记）

                    var fileName = Path.GetFileName(currentFile); // 获取原文件的文件名（包含扩展名）
                    if (!overrideFlag) // 不覆盖原文件
                    {
                        var suffix = " - [UTF-8" + (bomFlag ? " with BOM" : string.Empty) + "]"; // 指定目标文件名中的后缀，与是否包含 BOM 有关
                        var extension = Path.GetExtension(currentFile); // 获取原文件的扩展名

                        fileName = Path.GetFileNameWithoutExtension(currentFile); // 获取原文件的文件名（不包含扩展名）
                        fileName += suffix; // 向目标文件名中添加后缀
                        fileName += extension; // 向目标文件名补齐扩展名
                    }

                    var path = Path.Combine(output, fileName); // 将输出文件夹的路径与目标文件的文件名组合，获得目标文件的完整路径
                    var newFileStream = File.Create(path); // 创建目标文件
                    newFileStream.Close(); // 创建完成后关闭
                    File.WriteAllText(path, content, utf8Encoding); // 将原文件的内容以 UTF-8 编码写入到目标文件
                }

                MessageBox.Show("转换成功。");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("未找到输出文件夹。");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("未找到文件");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}