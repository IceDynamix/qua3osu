using System.Text;
using Quaver.API.Maps;

namespace qua3osu.OsuBeatmap.Sections
{
    public class MetadataSection : Section
    {
        public string Title;
        public string TitleUnicode;
        public string Artist;
        public string ArtistUnicode;
        public string Creator;
        public string Version;
        public string Source;
        public string Tags;
        
        public int BeatmapID = 0;
        public int BeatmapSetID = -1;

        public MetadataSection(Qua qua, Arguments args)
        {
            Title = qua.Title;
            TitleUnicode = qua.Title;
            Artist = qua.Artist;
            ArtistUnicode = qua.Artist;
            Creator = qua.Creator;
            Version = qua.DifficultyName;
            Source = qua.Source;
            Tags = qua.Tags;
        }

        public override string SectionTitle { get; } = "Metadata";

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