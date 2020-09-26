using System.Collections.Generic;
using CommandLine;

namespace qua3osu
{
    public class Arguments
    {
        [Option('q', Default = false)]
        public bool Quiet { get; set; }

        [Option('o', Default = "./output", HelpText = "Specifies the output directory")]
        public string Output { get; set; }

        [Option('v', Default = 20, HelpText = "Hitsound volume for the entire map")]
        public int Volume { get; set; }

        [Option('s', Default = "Soft")]
        public string SampleSet { get; set; }

        [Option('d', Default = false)]
        public bool DontApplyOffset { get; set; }

        [Value(0, MetaName = "inputPaths", Required = true,
            HelpText = "Paths to directories containing .qp files or direct .qp file paths")]
        public IEnumerable<string> Paths { get; set; }
    }
}