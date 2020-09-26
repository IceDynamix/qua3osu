using System.Text;
using Quaver.API.Maps;

namespace qua3osu.OsuBeatmap.Sections
{
    public class EditorSection : Section
    {
        public string Bookmarks = "";
        public double DistanceSpacing = 1.5;
        public int BeatDivisor = 4;
        public int GridSize = 4;
        public double TimelineZoom = 2.5;

        public EditorSection(Qua qua, Arguments args)
        {
        }

        public override string SectionTitle { get; } = "Editor";

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            if (Bookmarks != "")
                lines.AppendLine("Bookmarks: " + Bookmarks);
            lines.AppendLine("DistanceSpacing: " + DistanceSpacing);
            lines.AppendLine("BeatDivisor: " + BeatDivisor);
            lines.AppendLine("GridSize: " + GridSize);
            lines.AppendLine("TimelineZoom: " + TimelineZoom);
            return lines.ToString();
        }
    }
}