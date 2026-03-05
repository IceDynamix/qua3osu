using System.Text;
using Quaver.API.Maps;

namespace qua3osu.Osu.BeatmapSections
{
    public class MetadataSection(Qua qua) : BeatmapSection
    {
        public string Title = qua.Title;
        public string TitleUnicode = qua.Title;
        public string Artist = qua.Artist;
        public string ArtistUnicode = qua.Artist;
        public string Creator = qua.Creator;
        public string Version = qua.DifficultyName;
        public string Source = qua.Source;
        public string Tags = qua.Tags;

        public int BeatmapID = 0;
        public int BeatmapSetID = -1;

        protected override string SectionTitle => "Metadata";

        public override string ToString()
        {
            // Metadata doesn't have a space after the colon
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            lines.AppendLine("Title:" + Title);
            lines.AppendLine("TitleUnicode:" + TitleUnicode);
            lines.AppendLine("Artist:" + Artist);
            lines.AppendLine("ArtistUnicode:" + ArtistUnicode);
            lines.AppendLine("Creator:" + Creator);
            lines.AppendLine("Version:" + Version);
            lines.AppendLine("Source:" + Source);
            lines.AppendLine("Tags:" + Tags);
            lines.AppendLine("BeatmapID:" + BeatmapID);
            lines.AppendLine("BeatmapSetID:" + BeatmapSetID);
            return lines.ToString();
        }
    }
}