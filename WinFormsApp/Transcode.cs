using System;
using System.Text;

namespace WinFormsApp
{
    public static class Transcode
    {
        // TODO: 检查源字节流的编码，解决其不是 GBK 编码的异常情况
        // TODO: 修复文件无法转换为 UTF-8 with BOM 编码的问题
        public static byte[] TranscodeByteStream(byte[] bytes, bool bomFlag)
        {
            // 产生该字节流的文件不是文本文件
            if (!FileManager.IsTextFile(bytes))
            {
                throw new FormatException("此文件不是文本文件，不可转换。");
            }

            // GBK 编码
            var gbk = Encoding.GetEncoding(936);
            // UTF-8 编码
            var utf8 = new UTF8Encoding(bomFlag);
            // 将字节流从 GBK 转码为 UTF-8
            var utf8bytes = Encoding.Convert(gbk, utf8, bytes);
            return utf8bytes;
        }
    }
}