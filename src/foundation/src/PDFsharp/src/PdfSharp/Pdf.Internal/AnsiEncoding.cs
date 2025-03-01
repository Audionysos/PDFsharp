// PDFsharp - A .NET library for processing PDF
// See the LICENSE file in the solution root for more information.

using System.Text;

namespace PdfSharp.Pdf.Internal
{
    /// <summary>
    /// An encoder for PDF AnsiEncoding.
    /// </summary>
#if NET6_0_OR_GREATER
    [Obsolete("Use CodePagesEncodingProvider.Instance.GetEncoding(1252)")]
#endif
    public sealed class AnsiEncoding : Encoding
    {
        /// <summary>
        /// Gets the byte count.
        /// </summary>
        public override int GetByteCount(char[] chars, int index, int count)
        {
            return count;
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            int count = charCount;
            for (; charCount > 0; byteIndex++, charIndex++, charCount--)
                bytes[byteIndex] = (byte)UnicodeToAnsi(chars[charIndex]);
            return count;
        }

        /// <summary>
        /// Gets the character count.
        /// </summary>
        public override int GetCharCount(byte[] bytes, int index, int count) => count;

        /// <summary>
        /// Gets the chars.
        /// </summary>
        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            for (int idx = byteCount; idx > 0; byteIndex++, charIndex++, idx--)
                chars[charIndex] = AnsiToUnicode[bytes[byteIndex]];
            return byteCount;
        }

        /// <summary>
        /// When overridden in a derived class, calculates the maximum number of bytes produced by encoding the specified number of characters.
        /// </summary>
        /// <param name="charCount">The number of characters to encode.</param>
        /// <returns>
        /// The maximum number of bytes produced by encoding the specified number of characters.
        /// </returns>
        public override int GetMaxByteCount(int charCount) => charCount;

        /// <summary>
        /// When overridden in a derived class, calculates the maximum number of characters produced by decoding the specified number of bytes.
        /// </summary>
        /// <param name="byteCount">The number of bytes to decode.</param>
        /// <returns>
        /// The maximum number of characters produced by decoding the specified number of bytes.
        /// </returns>
        public override int GetMaxCharCount(int byteCount) => byteCount;

        /// <summary>
        /// Indicates whether the specified Unicode character is available in the ANSI code page 1252.
        /// </summary>
        public static bool IsAnsi1252Char(char ch)
        {
            if (ch is < '\u0080' or >= '\u00A0' and <= '\u00FF')
                return true;

            switch (ch)
            {
                case '\u20AC':
                case '\u0081':
                case '\u201A':
                case '\u0192':
                case '\u201E':
                case '\u2026':
                case '\u2020':
                case '\u2021':
                case '\u02C6':
                case '\u2030':
                case '\u0160':
                case '\u2039':
                case '\u0152':
                case '\u008D':
                case '\u017D':
                case '\u008F':
                case '\u0090':
                case '\u2018':
                case '\u2019':
                case '\u201C':
                case '\u201D':
                case '\u2022':
                case '\u2013':
                case '\u2014':
                case '\u02DC':
                case '\u2122':
                case '\u0161':
                case '\u203A':
                case '\u0153':
                case '\u009D':
                case '\u017E':
                case '\u0178':
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Maps Unicode to ANSI code page 1252.
        /// </summary>
        public static char UnicodeToAnsi(char ch)
        {
            if (ch is < '\u0080' or >= '\u00A0' and <= '\u00FF')
                return ch;

            return ch switch
            {
                '\u20AC' => '\u0080',
                '\u0081' => '\u0081',
                '\u201A' => '\u0082',
                '\u0192' => '\u0083',
                '\u201E' => '\u0084',
                '\u2026' => '\u0085',
                '\u2020' => '\u0086',
                '\u2021' => '\u0087',
                '\u02C6' => '\u0088',
                '\u2030' => '\u0089',
                '\u0160' => '\u008A',
                '\u2039' => '\u008B',
                '\u0152' => '\u008C',
                '\u008D' => '\u008D',
                '\u017D' => '\u008E',
                '\u008F' => '\u008F',
                '\u0090' => '\u0090',
                '\u2018' => '\u0091',
                '\u2019' => '\u0092',
                '\u201C' => '\u0093',
                '\u201D' => '\u0094',
                '\u2022' => '\u0095',
                '\u2013' => '\u0096',
                '\u2014' => '\u0097',
                '\u02DC' => '\u0098',
                '\u2122' => '\u0099',
                '\u0161' => '\u009A',
                '\u203A' => '\u009B',
                '\u0153' => '\u009C',
                '\u009D' => '\u009D',
                '\u017E' => '\u009E',
                '\u0178' => '\u009F',
                _ => '\u00A4' // Char 164 is ANSI value of '�'.
            };
        }

        /// <summary>
        /// Maps WinAnsi to Unicode characters.
        /// </summary>
        static readonly char[] AnsiToUnicode =
            {
              //          00        01        02        03        04        05        06        07        08        09        0A        0B        0C        0D        0E        0F
              /* 00 */ '\u0000', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\u0007', '\u0008', '\u0009', '\u000A', '\u000B', '\u000C', '\u000D', '\u000E', '\u000F',
              /* 10 */ '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001A', '\u001B', '\u001C', '\u001D', '\u001E', '\u001F',
              /* 20 */ '\u0020', '\u0021', '\u0022', '\u0023', '\u0024', '\u0025', '\u0026', '\u0027', '\u0028', '\u0029', '\u002A', '\u002B', '\u002C', '\u002D', '\u002E', '\u002F',
              /* 30 */ '\u0030', '\u0031', '\u0032', '\u0033', '\u0034', '\u0035', '\u0036', '\u0037', '\u0038', '\u0039', '\u003A', '\u003B', '\u003C', '\u003D', '\u003E', '\u003F',
              /* 40 */ '\u0040', '\u0041', '\u0042', '\u0043', '\u0044', '\u0045', '\u0046', '\u0047', '\u0048', '\u0049', '\u004A', '\u004B', '\u004C', '\u004D', '\u004E', '\u004F',
              /* 50 */ '\u0050', '\u0051', '\u0052', '\u0053', '\u0054', '\u0055', '\u0056', '\u0057', '\u0058', '\u0059', '\u005A', '\u005B', '\u005C', '\u005D', '\u005E', '\u005F',
              /* 60 */ '\u0060', '\u0061', '\u0062', '\u0063', '\u0064', '\u0065', '\u0066', '\u0067', '\u0068', '\u0069', '\u006A', '\u006B', '\u006C', '\u006D', '\u006E', '\u006F',
              /* 70 */ '\u0070', '\u0071', '\u0072', '\u0073', '\u0074', '\u0075', '\u0076', '\u0077', '\u0078', '\u0079', '\u007A', '\u007B', '\u007C', '\u007D', '\u007E', '\u007F',
              /* 80 */ '\u20AC', '\u0081', '\u201A', '\u0192', '\u201E', '\u2026', '\u2020', '\u2021', '\u02C6', '\u2030', '\u0160', '\u2039', '\u0152', '\u008D', '\u017D', '\u008F',
              /* 90 */ '\u0090', '\u2018', '\u2019', '\u201C', '\u201D', '\u2022', '\u2013', '\u2014', '\u02DC', '\u2122', '\u0161', '\u203A', '\u0153', '\u009D', '\u017E', '\u0178',
              /* A0 */ '\u00A0', '\u00A1', '\u00A2', '\u00A3', '\u00A4', '\u00A5', '\u00A6', '\u00A7', '\u00A8', '\u00A9', '\u00AA', '\u00AB', '\u00AC', '\u00AD', '\u00AE', '\u00AF',
              /* B0 */ '\u00B0', '\u00B1', '\u00B2', '\u00B3', '\u00B4', '\u00B5', '\u00B6', '\u00B7', '\u00B8', '\u00B9', '\u00BA', '\u00BB', '\u00BC', '\u00BD', '\u00BE', '\u00BF',
              /* C0 */ '\u00C0', '\u00C1', '\u00C2', '\u00C3', '\u00C4', '\u00C5', '\u00C6', '\u00C7', '\u00C8', '\u00C9', '\u00CA', '\u00CB', '\u00CC', '\u00CD', '\u00CE', '\u00CF',
              /* D0 */ '\u00D0', '\u00D1', '\u00D2', '\u00D3', '\u00D4', '\u00D5', '\u00D6', '\u00D7', '\u00D8', '\u00D9', '\u00DA', '\u00DB', '\u00DC', '\u00DD', '\u00DE', '\u00DF',
              /* E0 */ '\u00E0', '\u00E1', '\u00E2', '\u00E3', '\u00E4', '\u00E5', '\u00E6', '\u00E7', '\u00E8', '\u00E9', '\u00EA', '\u00EB', '\u00EC', '\u00ED', '\u00EE', '\u00EF',
              /* F0 */ '\u00F0', '\u00F1', '\u00F2', '\u00F3', '\u00F4', '\u00F5', '\u00F6', '\u00F7', '\u00F8', '\u00F9', '\u00FA', '\u00FB', '\u00FC', '\u00FD', '\u00FE', '\u00FF'
            };

#if true_  // Keep for reference to proof that this implementation is correct.
        public static void ProofImplementation()
        {
            // Implementation was verified with .NET Ansi encoding.
            Encoding dotnetImplementation = CodePagesEncodingProvider.Instance.GetEncoding(1252);
            Encoding thisImplementation = new AnsiEncoding();

            // Check ANSI chars.
            for (int i = 0; i <= 255; i++)
            {
                byte[] b = { (byte) i };
                char[] ch1 = dotnetImplementation.GetChars(b, 0, 1);
                char[] ch2 = thisImplementation.GetChars(b, 0, 1);
                if (ch1[0] != ch2[0])
                    Debug.Print("Error");
                byte[] b1 = dotnetImplementation.GetBytes(ch1, 0, 1);
                byte[] b2 = thisImplementation.GetBytes(ch1, 0, 1);
                if (b1.Length != b2.Length || b1.Length > 1 || b1[0] != b2[0])
                    Debug.Print("Error");
            }

            // Check Unicode chars.
            for (int i = 0; i <= 65535; i++)
            {
                if (i >= 256)
                    break;
                if (i == 0x80)
                    Debug.Print("");
                char[] ch = new char[] { (char)i };
                byte[] b1 = dotnetImplementation.GetBytes(ch, 0, 1);
                byte[] b2 = thisImplementation.GetBytes(ch, 0, 1);
                if (b1.Length != b2.Length || b1.Length > 1 || b1[0] != b2[0])
                    Debug.Print("Error");
                //byte[] b = new byte[] { (byte)i };
                //char ch = (char)i;
                char[] ch1 = dotnetImplementation.GetChars(b1, 0, 1);
                char[] ch2 = thisImplementation.GetChars(b2, 0, 1);
                if (ch1[0] != ch2[0])
                    Debug.Print("Error");
            }
        }
#endif
    }
}
