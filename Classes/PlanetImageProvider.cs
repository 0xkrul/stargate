using System.Globalization;
using System.Text;

namespace appliPandora.Classes
{
    internal static class PlanetImageProvider
    {
        private static readonly Dictionary<string, string> PlanetFiles = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Aina"] = "Aina.jpg",
            ["Aurae"] = "Aurae.jpg",
            ["Jupiter"] = "Jupiter.jpg",
            ["Kobaia"] = "Kobaia.jpg",
            ["La 9eme planete"] = "La_9eme_planete.jpg",
            ["La 9ème planète"] = "La_9eme_planete.jpg",
            ["Malaria"] = "Malaria.jpg",
            ["Mars"] = "Mars.jpg",
            ["Mercure"] = "Mercure.jpg",
            ["Muh"] = "Muh.jpg",
            ["Neptune"] = "Neptune.jpg",
            ["Saturne"] = "Saturne.jpg",
            ["Sckxyss"] = "Sckxyss.jpg",
            ["Setna"] = "Setna.jpg",
            ["Sohia"] = "Sohia.jpg",
            ["Terre"] = "Terre.jpg",
            ["Uranus"] = "Uranus.jpg",
            ["Venus"] = "Venus.jpg",
            ["Vénus"] = "Venus.jpg"
        };

        public static Image? Load(string planetName)
        {
            string? fileName = ResolveFileName(planetName);
            if (fileName == null)
                return null;

            string path = Path.Combine(AppContext.BaseDirectory, "Resources", "Planets", fileName);
            if (!File.Exists(path))
                path = Path.Combine(AppContext.BaseDirectory, fileName);

            return File.Exists(path) ? Image.FromFile(path) : null;
        }

        private static string? ResolveFileName(string planetName)
        {
            if (PlanetFiles.TryGetValue(planetName, out string? direct))
                return direct;

            string normalized = RemoveDiacritics(planetName).Replace("'", "").Trim();
            return PlanetFiles.TryGetValue(normalized, out string? fallback)
                ? fallback
                : null;
        }

        private static string RemoveDiacritics(string value)
        {
            string normalized = value.Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder(normalized.Length);

            foreach (char c in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                    builder.Append(c);
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
