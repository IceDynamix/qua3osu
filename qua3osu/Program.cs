using System;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using CommandLine;
using qua3osu.OsuBeatmap.Sections;
using Quaver.API.Maps;

namespace qua3osu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path of .qua to read:");
            var path = Console.ReadLine();
            if (!File.Exists(path))
            {
                Console.WriteLine("File not found");
                return;
            }
            
            var qua = Qua.Parse(path);
            var arguments = new Arguments()
            {
                SampleSet = "Drum",
                Volume = 50
            };

            var osu = new OsuBeatmap.OsuBeatmap(qua, arguments);
            Console.WriteLine(osu.ToString());
        }
    }
}