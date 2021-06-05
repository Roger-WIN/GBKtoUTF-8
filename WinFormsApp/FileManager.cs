using System.IO;
using System.Linq;

namespace WinFormsApp
{
    public static class FileManager
    {
        /* 将文件读取为字节流 */
        public static byte[] FileToByteStream(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); // 以读权限打开文件
            var fileBytes = new byte[fileStream.Length]; // 利用文件流的长度创建新的字节数组
            fileStream.Read(fileBytes, 0, fileBytes.Length); // 从文件流中读取字节块并写入字节流
            fileStream.Close(); // 关闭文件流
            return fileBytes;
        }

        /* 利用字节流创建或写入新文件 */
        public static void ByteStreamToFile(string filePath, byte[] fileBytes)
        {
            var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write); // 以写权限打开或新建文件
            fileStream.Write(fileBytes, 0, fileBytes.Length); // 将字节流写入文件
            fileStream.Close(); // 关闭文件流
        }

        /* 判断产生该字节流的文件是否是文本文件 */
        public static bool IsTextFile(byte[] fileBytes)
        {
            var fileByteList = fileBytes.ToList(); // 利用字节数组创建字节列表
            return !fileByteList.Contains(0); // 若存在字节 0，则不是文本文件
        }
    }
}