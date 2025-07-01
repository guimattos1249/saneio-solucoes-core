using System.Text.RegularExpressions;

namespace SaneioSolucoes.Infrastructure.Services.OFX.Sanitizer
{
    public static class OfxSanitizer
    {
        public static string Sanitize(string rawOfx)
        {
            var lines = rawOfx
                .Replace("\r", "") // remove carriage returns
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(FixLine)
                .ToList();

            return string.Join('\n', lines);
        }

        private static string FixLine(string line)
        {
            line = line.Trim();

            if (string.IsNullOrWhiteSpace(line)) return line;

            // Detect lines like <TAG>value and add </TAG> if missing
            var tagMatch = Regex.Match(line, @"^<(\w+?)>(.+)$");
            if (tagMatch.Success)
            {
                var tag = tagMatch.Groups[1].Value;
                var value = tagMatch.Groups[2].Value;

                // if value doesn't contain any "<" or ">", we assume it's raw text
                if (!value.Contains('<') && !value.Contains('>'))
                {
                    return $"<{tag}>{value}</{tag}>";
                }
            }

            return line;
        }
    }
}