using System.Text;
using CommandLine;

namespace qua3osu
{
    public class Arguments
    {
        [Value(0, MetaName = "input-paths", Required = true,
            HelpText = "Path(s) to directories containing .qp files or direct .qp file path(s)")]
        public IEnumerable<string> Paths { get; } = [];

        [Option('o', "output", HelpText = "Specifies the output directory, uses original directory of .qp by default")]
        public string? Output { get; }

        [Option("od", Default = 8, HelpText = "Overall difficulty as a number between 0 and 10")]
        public double OverallDifficulty { get; private set; }

        [Option("hp", Default = 8, HelpText = "HP drain as a number between 0 and 10")]
        public double HpDrainRate { get; private set; }

        [Option('v', "volume", Default = 20, HelpText = "Hitsound volume for the entire map")]
        public int Volume { get; set; }

        [Option('c', "creator", HelpText = "Changes the creator username for all maps")]
        public string? Creator { get; }

        [Option('r', "recursive", Default = false,
            HelpText = "Looks for .qp in all subdirectories of given directories")]
        public bool RecursiveSearch { get; }

        [Option("sampleset-normal", SetName = "SampleSet", Default = false,
            HelpText = "Use normal sampleset instead of soft")]
        public bool NormalSampleset { get; }

        [Option("sampleset-drum", SetName = "SampleSet", Default = false,
            HelpText = "Use drum sampleset instead of soft")]
        public bool DrumSampleSet { get; }

        [Option("audio-offset", Default = -23,
            HelpText =
                "Quaver times by waveform while osu! times by ear, so there's a difference of about 23 milliseconds. You can specify your own offset if needed.")]
        public int AudioOffset { get; }

        [Option("verbosity", SetName = "verbosity", Hidden = true, Default = 1,
            HelpText = "Show more of what's happening")]
        public int Verbosity { get; }

        public string? SampleSet { get; private set; }

        public Arguments()
        {
        }

        public void Validate()
        {
            SampleSet = DrumSampleSet ? "Drum" : (NormalSampleset ? "Normal" : "Soft");
            OverallDifficulty = ValidateValue(OverallDifficulty, "OD", 0f, 10f);
            HpDrainRate = ValidateValue(HpDrainRate, "HP", 0f, 10f);
            Volume = (int)ValidateValue(Volume, "Volume", 0, 100);
        }

        private double ValidateValue(double value, string name, double min, double max)
        {
            if ((value > max || value < min) && Verbosity >= 1)
                Console.WriteLine($"{name} was clamped between {min} and {max}");
            return Math.Clamp(value, min, max);
        }

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine("Paths: " + Paths);
            lines.AppendLine("Output: " + Output);
            lines.AppendLine("OverallDifficulty: " + OverallDifficulty);
            lines.AppendLine("HpDrainRate: " + HpDrainRate);
            lines.AppendLine("Volume: " + Volume);
            lines.AppendLine("Creator: " + Creator);
            lines.AppendLine("RecursiveSearch: " + RecursiveSearch);
            lines.AppendLine("AudioOffset: " + AudioOffset);
            lines.AppendLine("Verbosity: " + Verbosity);
            lines.AppendLine("SampleSet: " + SampleSet);
            return lines.ToString();
        }

        public void Print(string message, int minimumVerbosity = 1)
        {
            if (Verbosity >= minimumVerbosity)
                Console.WriteLine(message);
        }
    }
}