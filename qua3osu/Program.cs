using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;

namespace qua3osu
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = Parser.Default.ParseArguments<Arguments>(args)
                .WithParsed(arguments => ConvertListOfMapsets(arguments));
        }

        static void ConvertListOfMapsets(Arguments args)
        {
            args.Validate();

            args.Print(args.ToString(), 3);

            var listOfQpFiles = new List<string>();
            foreach (var inputPath in args.Paths)
            {
                if (Directory.Exists(inputPath))
                {
                    listOfQpFiles.AddRange(
                        Directory.GetFiles(inputPath)
                            .Where(file => Path.GetExtension(file) == ".qp")
                    );
                }
                else if (File.Exists(inputPath) && Path.GetExtension(inputPath) == ".qp")
                {
                    listOfQpFiles.Add(inputPath);
                }
            }

            if (listOfQpFiles.Count == 0)
                args.Print("No files found");
            else
                args.Print($"Found {listOfQpFiles.Count} mapsets to convert", 2);

            foreach (var mapsetPath in listOfQpFiles)
            {
                args.Print($"Converting {mapsetPath}", 2);
                try
                {
                    Conversion.ConvertMapset(mapsetPath, args);
                    Console.WriteLine($"Finished converting mapset {mapsetPath}", 2);
                }
                catch (Exception e)
                {
                    args.Print($"Could not convert mapset {mapsetPath}, {e.Message}");
                }
            }
        }
    }
}