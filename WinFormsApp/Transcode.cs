using System.Text;

namespace WinFormsApp
{
    public class Transcode
    {
        // UTF-8 编码
        public static readonly Encoding UTF8 = new UTF8Encoding();

        // TODO: 检查源字节流的编码，解决其不是 GBK 编码的异常情况
        public byte[] TranscodeByteStream(byte[] bytes)
        {
            // GBK 编码
            var gbk = Encoding.GetEncoding(936);
            // 将字节流从 GBK 转码为 UTF-8
            return Encoding.Convert(gbk, UTF8, bytes);
        }
    }
}