using System.Text;
using Quaver.API.Maps;

namespace qua3osu.OsuBeatmap.Sections
{
    public class EventsSection : Section
    {
        public string BackgroundFile;

        public EventsSection(Qua qua, Arguments args)
        {
            BackgroundFile = qua.BackgroundFile;
        }

        public override string SectionTitle { get; } = "Events";

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            lines.AppendLine($"0,0,\"{BackgroundFile}\",0,0");
            return lines.ToString();
        }
    }
}