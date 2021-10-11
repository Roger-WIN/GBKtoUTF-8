using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WinFormsApp
{
    public class FileManager
    {
        /* 将文件读取为字节流 */
        public byte[] FileToByteStream(string filePath)
        {
            // 以读权限打开文件
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // 利用文件流的长度创建新的字节数组
                var fileBytes = new byte[fileStream.Length];
                // 从文件流中读取字节块并写入字节流
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                return fileBytes;
            }
        }

        /* 利用字节流创建或写入新文件 */
        public void ByteStreamToFile(string filePath, byte[] fileBytes, bool hasBom)
        {
            // 以写权限新建文件
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream, new UTF8Encoding(hasBom)))
            {
                // 将字符串写入文件
                streamWriter.Write(Transcode.UTF8.GetString(fileBytes));
            }
        }

        /* 判断该文件是否是文本文件 */
        public bool IsTextFile(string filePath) => IsTextFile(FileToByteStream(filePath));

        /* 判断产生该字节流的文件是否是文本文件 */
        public bool IsTextFile(IEnumerable<byte> fileBytes) => !fileBytes.ToList().Contains(0); // 若存在字节 0，则不是文本文件
    }
}