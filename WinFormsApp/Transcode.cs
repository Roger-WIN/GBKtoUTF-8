using System.Text;

namespace WinFormsApp
{
    public class Transcode
    {
        // UTF-8 编码
        public static readonly Encoding UTF8 = new UTF8Encoding();

        public Transcode()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public byte[] TranscodeByteStream(byte[] bytes)
        {
            // 检测字符编码
            var encoding = DetectEncoding(bytes);
            // 将字节流从其它字符编码转码为 UTF-8
            return Encoding.Convert(encoding, UTF8, RemoveBom(bytes));
        }

        /* TODO: 检测文件的字符编码 */
        private Encoding DetectEncoding(byte[] bytes)
        {
            return Encoding.GetEncoding(936);
        }

        /* 移除 BOM */
        private byte[] RemoveBom(byte[] bytes)
        {
            var bom = MatchBom(bytes);
            return bom != null ? bytes.Skip(bom.Length).ToArray() : bytes;
        }

        /* 寻找匹配的 BOM */
        private byte[]? MatchBom(byte[] bytes)
        {
            // BOM for UTF-8
            var utf8 = new byte[] { 0xEF, 0xBB, 0xBF };
            // BOM for UTF-16 (big-endian)
            var utf16be = new byte[] { 0xFE, 0xFF };
            // BOM for UTF-16 (little-endian)
            var utf16le = new byte[] { 0xFF, 0xFE };
            // BOM for UTF-32 (big-endian)
            var utf32be = new byte[] { 0x00, 0x00, 0xFE, 0xFF };
            // BOM for UTF-32 (little-endian)
            var utf32le = new byte[] { 0xFF, 0xFE, 0x00, 0x00 };

            var boms = new List<byte[]> { utf8, utf16be, utf16le, utf32be, utf32le };

            // bytes 从头部截取与 BOM 等长的序列，查找是否存在与 BOM 完全匹配的
            Predicate<byte[]> predicate = bom => Enumerable.SequenceEqual(bytes.Take(bom.Length), bom);
            // 若存在匹配开头的，返回匹配项；否则返回空
            return boms.Exists(predicate) ? boms.Find(predicate) : null;
        }
    }
}