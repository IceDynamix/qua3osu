using System;
using System.IO;
using System.IO.Compression;
using Quaver.API.Maps;

namespace qua3osu
{
    public static class Conversion
    {
        public static void ConvertMapset(string mapsetPath, Arguments args)
        {
            if (!File.Exists(mapsetPath) || Path.GetExtension(mapsetPath) != ".qp")
            {
                Console.WriteLine("Invalid mapset file");
                return;
            }

            var outputDir = args.Output ?? Path.GetDirectoryName(mapsetPath);
            var folderName = Path.GetFileNameWithoutExtension(mapsetPath);
            var extractDir = Path.Join(outputDir, folderName);

            ZipFile.ExtractToDirectory(mapsetPath, extractDir, true);
            foreach (var file in Directory.EnumerateFiles(extractDir))
            {
                switch (Path.GetExtension(file))
                {
                    case ".qua":
                        var map = new Osu.OsuBeatmap(Qua.Parse(file), args);
                        File.WriteAllText(file, map.ToString());
                        var osuPath = Path.Join(extractDir, Path.GetFileNameWithoutExtension(file) + ".osu");
                        File.Move(file, osuPath, true);
                        break;
                    case ".png":
                    case ".jpg":
                    case ".mp3":
                        break;
                    default:
                        File.Delete(file);
                        break;
                }
            }

            var oszPath = extractDir + ".osz";
            if (File.Exists(oszPath))
                File.Delete(oszPath);
            ZipFile.CreateFromDirectory(extractDir, extractDir + ".osz");

            foreach (var file in Directory.EnumerateFiles(extractDir))
                File.Delete(file);
            Directory.Delete(extractDir);
        }
    }
}