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
                throw new ArgumentException("Invalid file");
            }

            var outputDir = args.Output ?? Path.GetDirectoryName(mapsetPath);
            var folderName = Path.GetFileNameWithoutExtension(mapsetPath);
            var extractDir = Path.Join(outputDir, folderName);

            try
            {
                ZipFile.ExtractToDirectory(mapsetPath, extractDir, true);
                foreach (var file in Directory.EnumerateFiles(extractDir))
                {
                    switch (Path.GetExtension(file))
                    {
                        case ".qua":
                            var qua = Qua.Parse(file);
                            args.Print("Parsed qua", 3);

                            var map = new Osu.OsuBeatmap(qua, args);
                            args.Print("Converted qua to osu! map object", 3);

                            File.WriteAllText(file, map.ToString());
                            args.Print($"Written to file {file}", 3);

                            var osuPath = Path.Join(extractDir, Path.GetFileNameWithoutExtension(file) + ".osu");
                            File.Move(file, osuPath, true);
                            args.Print($"Renamed file to {osuPath}", 3);

                            break;
                        case ".png":
                        case ".jpg":
                        case ".mp3":
                            args.Print($"Kept file {file} in directory", 3);
                            break;
                        default:
                            args.Print($"Removed file {file}", 3);
                            File.Delete(file);
                            break;
                    }
                }

                var oszPath = extractDir + ".osz";
                if (File.Exists(oszPath))
                {
                    args.Print($"Removed existing .osz", 3);
                    File.Delete(oszPath);
                }

                ZipFile.CreateFromDirectory(extractDir, extractDir + ".osz");
                args.Print($"Created new .osz", 3);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                foreach (var file in Directory.EnumerateFiles(extractDir))
                    File.Delete(file);
                Directory.Delete(extractDir);
                args.Print($"Removed temporary conversion folder", 3);
            }
        }
    }
}