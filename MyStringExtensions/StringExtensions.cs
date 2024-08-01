using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

public static class StringExtensions {
    /// <summary>
    /// Substring for unknown length. No errors for mismatches due to empty space or length
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="length">Cut length from begin</param>
    /// <remarks>
    /// Example: <strong>"asd".Cut(5);</strong>
    /// </remarks>
    public static string Cut(this string value, int length) {
        return value?.Substring(0, Math.Min(value.Length, length)) ?? "";
    }

    /// <summary>
    /// Trim all whitespace in text
    /// </summary>
    /// <param name="value">Source text</param>
    /// <remarks>
    /// Example: <strong>"asd cvb".TrimInside();</strong>
    /// </remarks>
    public static string TrimInside(this string value) {
        return value?.Replace(" ", "") ?? "";
    }

    /// <summary>
    /// Check Null or Empty value in string collection
    /// </summary>
    /// <param name="value">Source text</param>
    public static bool NullExists(this IEnumerable<string> value) {
        return value?.Any(x=> string.IsNullOrEmpty(x)) ?? false;
    }

    /// <summary>
    /// Convert to base64 string yout byte array
    /// </summary>
    /// <param name="value">Source bytes</param>
    public static string ToBase64(this byte[] value) {
        return Convert.ToBase64String(value) ?? "";
    }

    /// <summary>
    /// Find a word in string without sensivity
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="seek">Seeking for</param>
    /// <remarks>
    /// Example: <strong>sourceText.ContainsNosense("rabbit");</strong>
    /// </remarks>
    public static bool ContainsNosense(this string value, string seek) {
        return value?.Contains(seek, StringComparison.InvariantCultureIgnoreCase) ?? false;
    }

    /// <summary>
    /// Find how many times the word you are looking for is repeated in the text
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="seek">Seeking for</param>
    /// <remarks>
    /// Example: <strong>sourceText.CountWords("rabbit");</strong>
    /// </remarks>
    public static int CountWords(this string value, string seek) {
        string[] source = value.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
        var matchQuery = from word in source
                         where word.Equals(seek, StringComparison.InvariantCultureIgnoreCase)
                         select word;
        return matchQuery.Count();
    }

    /// <summary>
    /// Find how many times the word you are looking for is repeated in the words in the text. Detailed
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="seek">Seeking for</param>
    /// <remarks>
    /// Example: <strong>sourceText.CountWords("ra");</strong>
    /// </remarks>
    public static int CountWordsIn(this string value, string seek) {
        string[] source = value.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
        var matchQuery = from word in source
                         where word.Contains(seek, StringComparison.InvariantCultureIgnoreCase)
                         select word;
        return matchQuery.Count();
    }

    /// <summary>
    /// Generate Random text
    /// </summary>
    /// <param name="length">Generated text length</param>
    /// <remarks>
    /// Example: <strong>StringExtensions.RandomString(12);</strong>
    /// </remarks>
    public static string RandomString(int length) {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789&?%$@";
        Random Random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Generate non-recyclable writing from your text
    /// </summary>
    /// <param name="rawData">Source text</param>
    /// <remarks>
    /// Example: <strong>sourceText.EncryptHash();</strong>
    /// </remarks>
    public static string EncryptHash(this string rawData) {
        using (SHA256 sha256Hash = SHA256.Create()) {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    /// <summary>
    /// Generate masked text from your text
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="start">Mask start</param>
    /// <param name="length">Mask length</param>
    /// <remarks>
    /// Example: <strong>sourceText.Mask(8,12);</strong>
    /// </remarks>
    public static string Mask(this string value, int start, int length) {
        var arr = value.ToArray();
        var sb = new StringBuilder();
        for(int i = 0; i< arr.Length; i++) {
            if (i < start) {
                sb.Append(arr[i]);
            } else if(i > start && i <= (start + length)){
                sb.Append("*");
            } else {
                sb.Append(arr[i]);
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Text to image bytes. Exports png file bytes. Only works on Windows OS
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="font">Windows font name is optional</param>
    /// <returns>image byte array</returns>
    /// <exception cref="Exception">Only works on Windows OS</exception>
    /// <remarks>
    /// Example: <strong>sourceText.ToImage();</strong>
    /// </remarks>
    public static byte[] ToImage(this string value, string font ="Arial") {
        string text = value.Trim();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
            Bitmap bitmap = new Bitmap(1, 1);
            Font _font = new Font(font, 25, FontStyle.Regular, GraphicsUnit.Pixel);
            Graphics graphics = Graphics.FromImage(bitmap);
            int width = (int)graphics.MeasureString(text, _font).Width;
            int height = (int)graphics.MeasureString(text, _font).Height;
            bitmap = new Bitmap(bitmap, new Size(width, height));
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            graphics.DrawString(text, _font, new SolidBrush(Color.Black), 0, 0);
            graphics.Flush();
            graphics.Dispose();
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        } else {
            throw new Exception("Text to image only works on Windows os");
        }
    }

    /// <summary>
    /// Run a delegated action for each found word 
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="seek">Seeking for</param>
    /// <param name="act">What to do for each word found. This is an action waiting for a delegate</param>
    /// <remarks>
    /// Example: <strong>sourceText.ForWords("huge", x => Console.WriteLine(x));</strong>
    /// </remarks>
    public static void ForWords(this string value, string seek, Action<string> act) {
        string[] source = value.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
        var matchQuery = from word in source
                         where word.Equals(seek, StringComparison.InvariantCultureIgnoreCase)
                         select word;
        foreach(string w in matchQuery) {
            act.Invoke(w);
        }
    }

    /// <summary>
    /// Run a delegated action for each found word. Detailed
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="seek">Seeking for</param>
    /// <param name="act">What to do for each word found. This is an action waiting for a delegate</param>
    /// <remarks>
    /// Example: <strong>sourceText.ForWordsIn("i", x => Console.WriteLine(x));</strong>
    /// </remarks>
    public static void ForWordsIn(this string value, string seek, Action<string> act) {
        string[] source = value.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
        var matchQuery = from word in source
                         where word.Contains(seek, StringComparison.InvariantCultureIgnoreCase)
                         select word;
        foreach (string w in matchQuery) {
            act.Invoke(w);
        }
    }

    /// <summary>
    /// Make uppercase the first letters after a space
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="culture">CultureInfo ex: "en-US" optional</param>
    /// <remarks>
    /// Example: <strong>sourceText.ToTitleCase();</strong>
    /// </remarks>
    public static string ToTitleCase(this string value, string culture = "en-US") {
        TextInfo ProperCase = new CultureInfo(culture, false).TextInfo;
        return ProperCase.ToTitleCase(value.ToLower());
    }

    /// <summary>
    /// Make uppercase the first character of the text
    /// </summary>
    /// <param name="value">Source text</param>
    /// <remarks>
    /// Example: <strong>sourceText.ToBodyCase();</strong>
    /// </remarks>
    public static string ToBodyCase(this string value) {
        var sb = new StringBuilder();
        foreach (var x in value) {
            if (
                !char.IsDigit(x) &&
                !char.IsWhiteSpace(x) &&
                !char.IsSymbol(x) &&
                !char.IsPunctuation(x) &&
                !sb.ToString().LoopIn(x => char.IsUpper(x))
            ) {
                sb.Append(char.ToUpper(x));
            } else {
                sb.Append(x);
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Make snakecase for source text
    /// </summary>
    /// <param name="value">Source text</param>
    /// <remarks>
    /// Example: <strong>sourceText.ToLowerUnderscored();</strong>
    /// </remarks>
    public static string ToLowerUnderscored(this string value) {
        char Seperator = '_';
        IEnumerable<char> ToSeperated() {
            var e = value.GetEnumerator();
            if (!e.MoveNext()) 
                yield break;

            var c = char.ToLower(e.Current);
            yield return c;
            while (e.MoveNext()) {                
                if (char.IsUpper(e.Current)) {
                    yield return Seperator;
                    yield return char.ToLower(e.Current);
                } else if(char.IsWhiteSpace(e.Current)) {
                    yield return Seperator;
                } else {
                    yield return e.Current;
                }
            }
        }

        return new string(ToSeperated().ToArray()).Singularize('_');
    }

    /// <summary>
    /// Deduplicate characters that repeat side by side
    /// </summary>
    /// <param name="value">Source text</param>
    /// <remarks>
    /// Example: <strong>sourceText.Singularize();</strong>
    /// </remarks>
    public static string Singularize(this string value) {
        string last = "";
        StringBuilder result = new StringBuilder();
        value.LoopIn(x=> { 
            if(last != x.ToString()) {
                result.Append(x);
                last = x.ToString();
            } else {
                last = x.ToString();
            }
        });
        return result.ToString();
    }

    /// <summary>
    /// Deduplicate selected characters that repeat side by side
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="seek">Seeking for</param>
    /// <remarks>
    /// Example: <strong>sourceText.Singularize('c');</strong>
    /// </remarks>
    public static string Singularize(this string value, char seek) {
        string last = "";
        StringBuilder result = new StringBuilder();
        value.LoopIn(x => {
            if (last != x.ToString() || x != seek) {
                result.Append(x);
                last = x.ToString();
            } else {
                last = x.ToString();
            }
        });
        return result.ToString();
    }

    /// <summary>
    /// Duplicate a selected text as many times as you like
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="length">How much time</param>
    /// <remarks>
    /// Example: <strong>"#".Multiplex(21);</strong>
    /// </remarks>
    public static string Multiplex(this string value, uint length) {
        var sb = new StringBuilder(value);
        for(int i =0; i<length;i++) {
            sb.Append(value);
        }
        return sb.ToString();
    }

    /// <summary>
    /// String mask 
    /// ex: value = Turkiye, 
    /// startLength = 2,
    /// maskLength = 3,
    /// endLength = 2
    /// returns Tu***ye
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="startLength">From 0 index -> ex:startLength = 4 (0,1,2,3,4)</param>
    /// <param name="maskLength">maskChar count -> ex:maskLength = 3 (***) </param>
    /// <param name="endLength">From value_length-endLength -> ex:value_length= 6, endLength = 4 (4,5,6)</param>
    /// <returns></returns>
    public static string Mask(this string value, uint startLength, uint maskLength, uint endLength, char maskChar='*') {
        var sb = new StringBuilder();
        for (int i = 0; i < value.Length; i++) {
            if(i<=startLength)
                sb.Append(value[i]);
            else if(i<(value.Length - endLength) && !sb.ToString().Contains(maskChar))
                for(int c=0;c<maskLength;c++)
                    sb.Append(maskChar);
            else if(i >= (value.Length - endLength)-1)
                sb.Append(value[i]);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Run an action for each character in the text
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="act">Your void action</param>
    /// <remarks>
    /// Example: <strong>sourceText.LoopIn(x =>Console.WriteLine(x));</strong>
    /// </remarks>
    public static void LoopIn(this string value, Action<char> act) {
        foreach(char c in value) {
            act.Invoke(c);
        }
    }

    /// <summary>
    /// Run a function with a return value for each character in the text
    /// </summary>
    /// <param name="value">Source text</param>
    /// <param name="func">A deelgated function with boolean return value</param>
    /// <remarks>
    /// Example: <strong>sourceText.LoopIn(x =>Console.WriteLine(x));</strong>
    /// </remarks>
    public static bool LoopIn(this string value, Func<char, bool> func) {
        foreach (char c in value) {
            if (func.Invoke(c)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Remove numeric chars and return new string object
    /// </summary>
    /// <param name="value">Source text</param>
    /// <remarks>
    /// Example: <strong>"123 ankara ++".ClearDigits();</strong>
    /// </remarks>
    public static string ClearDigits(this string value) {
        var sb = new StringBuilder();
        foreach (var x in value) {
            if (!char.IsDigit(x)) {
                sb.Append(x);
            } 
        }
        return sb.ToString();
    }

    /// <summary>
    /// Remove symbolic chars and return new string object
    /// </summary>
    /// <param name="value">Source text</param>
    /// <remarks>
    /// Example: <strong>"123 ankara ++".ClearSymbols();</strong>
    /// </remarks>
    public static string ClearSymbols(this string value) {
        var sb = new StringBuilder();
        foreach (var x in value) {
            if (!char.IsSymbol(x)) {
                sb.Append(x);
            } 
        }
        return sb.ToString();
    }

    /// <summary>
    /// Compresses a string and returns a deflate compressed, Base64 encoded string.
    /// </summary>
    /// <param name="uncompressedString">String to compress</param>
    public static string Compress(this string uncompressedString) {
        byte[] compressedBytes;

        using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(uncompressedString))) {
            using (var compressedStream = new MemoryStream()) {
                using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.SmallestSize, true)) {
                    uncompressedStream.CopyTo(compressorStream);
                }
                compressedBytes = compressedStream.ToArray();
            }
        }
        return Convert.ToBase64String(compressedBytes);
    }

    /// <summary>
    /// Decompresses a deflate compressed, Base64 encoded string and returns an uncompressed string.
    /// </summary>
    /// <param name="compressedString">String to decompress.</param>
    public static string Decompress(this string compressedString) {
        byte[] decompressedBytes;

        var compressedStream = new MemoryStream(Convert.FromBase64String(compressedString));

        using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress)) {
            using (var decompressedStream = new MemoryStream()) {
                decompressorStream.CopyTo(decompressedStream);

                decompressedBytes = decompressedStream.ToArray();
            }
        }

        return Encoding.UTF8.GetString(decompressedBytes);
    }
}