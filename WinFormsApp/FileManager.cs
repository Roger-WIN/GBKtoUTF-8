using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WinFormsApp
{
    public static class FileManager
    {
        /* 将文件读取为字节流 */
        public static byte[] FileToByteStream(string filePath)
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
        public static void ByteStreamToFile(string filePath, byte[] fileBytes)
        {
            // 以写权限打开或新建文件
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                // 将字节流写入文件
                fileStream.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        /* 判断该文件是否是文本文件 */
        public static bool IsTextFile(string filePath) => IsTextFile(FileToByteStream(filePath));

        /* 判断产生该字节流的文件是否是文本文件 */
        public static bool IsTextFile(IEnumerable<byte> fileBytes) => !fileBytes.ToList().Contains(0); // 若存在字节 0，则不是文本文件
    }
}