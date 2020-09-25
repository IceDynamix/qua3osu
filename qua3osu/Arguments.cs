using System.Collections.Generic;
using CommandLine;
using MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors;

namespace qua3osu
{
    public class Arguments
    {
        [Option('q', "quiet", Default = false)]
        public bool Quiet { get; set; }

        [Option('o', "output", Default = "./output", HelpText = "Specifies the output directory")]
        public string Output { get; set; }
        
        [Option('v', "volume", Default = 20, HelpText = "Hitsound volume for the entire map")]
        public int Volume { get; set; }
        
        [Option('s', "sampleset", Default = "Soft")]
        public string SampleSet { get; set; }

        [Value(0, MetaName = "input paths", Required = false, HelpText = "Paths to directories containing .qp files or direct .qp file paths")]
        public IEnumerable<string> Paths { get; set; }
        
    }
}