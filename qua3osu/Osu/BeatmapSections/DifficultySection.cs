using System.Text;
using Quaver.API.Maps;

namespace qua3osu.Osu.BeatmapSections
{
    public class DifficultySection(Qua qua) : BeatmapSection
    {
        public double HpDrainRate = 8;
        public double CircleSize = qua.GetKeyCount();
        public double OverallDifficulty = 8;
        public double ApproachRate = 5;
        public double SliderMultiplier = 1.4;
        public double SliderTickRate = 1;

        protected override string SectionTitle => "Difficulty";

        public override string ToString()
        {
            // Difficulty doesn't have a space after the colon
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            lines.AppendLine("HPDrainRate:" + HpDrainRate);
            lines.AppendLine("CircleSize:" + CircleSize);
            lines.AppendLine("OverallDifficulty:" + OverallDifficulty);
            lines.AppendLine("ApproachRate:" + ApproachRate);
            lines.AppendLine("SliderMultiplier:" + SliderMultiplier);
            lines.AppendLine("SliderTickRate:" + SliderTickRate);
            return lines.ToString();
        }
    }
}