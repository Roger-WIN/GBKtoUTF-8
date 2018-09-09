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
                    var content = File.ReadAllText(currentFile, Encoding.Default);
                    var utf8Encoding = new UTF8Encoding(bomFlag); // 是否包含 BOM（byte order mark，字节顺序标记）

                    var fileName = Path.GetFileName(currentFile);
                    if (!overrideFlag) // 不覆盖原文件
                    {
                        var suffix = " - [UTF-8" + (bomFlag ? " with BOM" : string.Empty) + "]";
                        var extension = Path.GetExtension(currentFile);

                        fileName = Path.GetFileNameWithoutExtension(currentFile);
                        fileName += suffix;
                        fileName += extension;
                    }

                    var path = Path.Combine(output, fileName);
                    var newFileStream = File.Create(path);
                    newFileStream.Close();
                    File.WriteAllText(path, content, utf8Encoding);
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