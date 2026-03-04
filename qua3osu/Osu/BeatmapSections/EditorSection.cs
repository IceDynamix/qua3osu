using System.Text;
using Quaver.API.Maps;

namespace qua3osu.Osu.BeatmapSections
{
    public class EditorSection : BeatmapSection
    {
        public List<int> Bookmarks;
        public double DistanceSpacing = 1.5;
        public int BeatDivisor = 4;
        public int GridSize = 4;
        public double TimelineZoom = 2.5;

        public EditorSection(Qua qua, Arguments args)
        {
            var offset = args.DontApplyOffset ? 0 : OsuBeatmap.QUAVER_TO_OSU_OFFSET;
            Bookmarks = qua.Bookmarks.Select(b => b.StartTime + offset).ToList();
        }

        protected override string SectionTitle { get; } = "Editor";

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