using System.Text;
using Quaver.API.Maps;

namespace qua3osu.OsuBeatmap.Sections
{
    public class GeneralSection : Section
    {
        public string AudioFilename;
        public int AudioLeadIn = 0;
        public int PreviewTime = 0;
        public int Countdown = 0;
        public string SampleSet;
        public double StackLeniency = 0.7;
        public int Mode = 3;
        public int LetterBoxInBreaks = 0;
        public int SpecialStyle = 0;
        public int WidescreenStoryboard = 1;

        public GeneralSection(Qua qua, Arguments args)
        {
            AudioFilename = qua.AudioFile;
            PreviewTime = qua.SongPreviewTime;
            SampleSet = args.SampleSet;
        }

        public override string SectionTitle { get; } = "General";

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            lines.AppendLine("AudioFilename: " + AudioFilename);
            lines.AppendLine("AudioLeadIn: " + AudioLeadIn);
            lines.AppendLine("PreviewTime: " + PreviewTime);
            lines.AppendLine("Countdown: " + Countdown);
            lines.AppendLine("SampleSet: " + SampleSet);
            lines.AppendLine("StackLeniency: " + StackLeniency);
            lines.AppendLine("Mode: " + Mode);
            lines.AppendLine("LetterBoxInBreaks: " + LetterBoxInBreaks);
            lines.AppendLine("SpecialStyle: " + SpecialStyle);
            lines.AppendLine("WidescreenStoryboard: " + WidescreenStoryboard);
            return lines.ToString();
        }
    }
}