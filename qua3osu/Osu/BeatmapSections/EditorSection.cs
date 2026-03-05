using System.Text;
using Quaver.API.Maps;

namespace qua3osu.Osu.BeatmapSections
{
    public class EditorSection(Qua qua) : BeatmapSection
    {
        public List<int> Bookmarks = qua.Bookmarks.Select(b => b.StartTime).ToList();
        public double DistanceSpacing = 1.5;
        public int BeatDivisor = 4;
        public int GridSize = 4;
        public double TimelineZoom = 2.5;

        protected override string SectionTitle => "Editor";

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            lines.AppendLine("Bookmarks: " + string.Join(',', Bookmarks));
            lines.AppendLine("DistanceSpacing: " + DistanceSpacing);
            lines.AppendLine("BeatDivisor: " + BeatDivisor);
            lines.AppendLine("GridSize: " + GridSize);
            lines.AppendLine("TimelineZoom: " + TimelineZoom);
            return lines.ToString();
        }
    }
}