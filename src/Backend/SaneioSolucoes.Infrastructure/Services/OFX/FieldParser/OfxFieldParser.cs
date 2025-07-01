using SaneioSolucoes.Infrastructure.Utils;
using System.Text.RegularExpressions;

namespace SaneioSolucoes.Infrastructure.Services.OFX.FieldParser
{
    public static class OfxFieldParser
    {
        public static string? ExtractTag(string content, string tag)
        {
            var match = Regex.Match(content, $"<{tag}>(.*?)($|<)", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value.Trim() : null;
        }

        public static DateTime? ParseDate(string? rawDate)
        {
            if (string.IsNullOrWhiteSpace(rawDate)) return null;

            if (DateTime.TryParseExact(rawDate.Substring(0, 8), "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.None, out var dt))
                return dt;

            return null;
        }

        public static long ParseAmount(string? value)
        {
            if (decimal.TryParse(value?.Replace(",", "."), System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out var result))
            {
                return (long)(result * Constants.MoneyScaleConverter);

            }

            return 0;
        }
    }
}
