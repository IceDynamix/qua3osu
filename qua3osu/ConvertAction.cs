using System.CommandLine;
using System.IO.Compression;
using qua3osu.Osu;
using Quaver.API.Maps;

namespace qua3osu;

public class ConvertAction
{
    private readonly ConvertCommand _command;
    private ParseResult? _result;

    private string[] Input => _result.GetValue(_command.Input);
    private string? Output => _result.GetValue(_command.Output);
    private double Od => _result.GetValue(_command.Od);
    private double Hp => _result.GetValue(_command.Hp);
    private int Volume => _result.GetValue(_command.Volume);
    private string? Creator => _result.GetValue(_command.Creator);
    private string SampleSet => _result.GetValue(_command.SampleSet);
    private int Offset => _result.GetValue(_command.Offset);
    private int Verbosity => _result.GetValue(_command.Verbosity);

    public ConvertAction(ConvertCommand command)
    {
        _command = command;
    }

    public void Convert(ParseResult res)
    {
        _result = res;

        var listOfFiles = new List<string>();
        foreach (var inputPath in Input)
        {
            if (Directory.Exists(inputPath))
            {
                listOfFiles.AddRange(Directory.GetFiles(inputPath).Where(CanConvertPath));
            }
            else if (File.Exists(inputPath) && CanConvertPath(inputPath))
            {
                listOfFiles.Add(inputPath);
            }
        }

        if (listOfFiles.Count == 0)
            Print("No files found");
        else
            Print($"Found {listOfFiles.Count} maps or mapsets to convert", 2);

        foreach (var path in listOfFiles)
        {
            Print($"Converting {path}", 2);
            try
            {
                if (Path.GetExtension(path) == ".qp")
                {
                    ConvertMapset(path);
                    Console.WriteLine($"Finished converting mapset {path}", 2);
                }
                else
                {
                    ConvertMapFile(path);
                    Console.WriteLine($"Finished converting map {path}", 2);
                }
            }
            catch (Exception e)
            {
                Print($"Could not convert map or mapset {path}, {e.Message}");
            }
        }
    }

    void ConvertMapset(string mapsetPath)
    {
        if (!File.Exists(mapsetPath) || Path.GetExtension(mapsetPath) != ".qp")
        {
            throw new ArgumentException("Invalid file");
        }

        var outputDir = Output ?? Path.GetDirectoryName(mapsetPath);
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
                        var osuPath = Path.Join(extractDir, Path.GetFileNameWithoutExtension(file) + ".osu");

                        var qua = Qua.Parse(file);
                        Print("Parsed qua", 3);

                        var map = Qua2Osu(qua);
                        Print("Converted qua to osu! map object", 3);

                        File.WriteAllText(osuPath, map.ToString());
                        Print($"Written to osuPath {osuPath}", 3);
                        break;
                    case ".png":
                    case ".jpg":
                    case ".mp3":
                        Print($"Kept file {file} in directory", 3);
                        break;
                    default:
                        Print($"Removed file {file}", 3);
                        File.Delete(file);
                        break;
                }
            }

            var oszPath = extractDir + ".osz";
            if (File.Exists(oszPath))
            {
                Print($"Removed existing .osz", 3);
                File.Delete(oszPath);
            }

            ZipFile.CreateFromDirectory(extractDir, extractDir + ".osz");
            Print($"Created new .osz", 3);
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
            Print($"Removed temporary conversion folder", 3);
        }
    }

    void ConvertMapFile(string filePath)
    {
        if (!File.Exists(filePath) || Path.GetExtension(filePath) != ".qua")
        {
            throw new ArgumentException("Invalid file");
        }

        var outputDir = Output ?? Path.GetDirectoryName(filePath);
        var osuPath = Path.Join(outputDir, Path.GetFileNameWithoutExtension(filePath) + ".osu");

        var qua = Qua.Parse(filePath);
        Print("Parsed qua", 3);

        var map = Qua2Osu(qua);
        Print("Converted qua to osu! map object", 3);

        File.WriteAllText(osuPath, map.ToString());
        Print($"Written to path {osuPath}", 3);
    }

    OsuBeatmap Qua2Osu(Qua qua)
    {
        var offset = Offset;

        // apply modifications based on command arguments
        var map = new OsuBeatmap(qua)
        {
            GeneralSection =
            {
                SampleSet = SampleSet switch
                {
                    "normal" => "Normal",
                    "drum" => "Drum",
                    _ => "Soft"
                }
            },
            DifficultySection =
            {
                OverallDifficulty = Od,
                HpDrainRate = Hp
            },
        };
        map.MetadataSection.Creator = Creator ?? map.MetadataSection.Creator;

        map.GeneralSection.PreviewTime += offset;
        map.EditorSection.Bookmarks = map.EditorSection.Bookmarks.Select(t => t + offset).ToList();
        map.HitObjectsSection.HitObjects.ForEach(h =>
        {
            h.Time += offset;
            if (h.IsLongNote)
                h.EndTime += offset;
        });
        var vol = Volume;
        map.TimingPointsSection.TimingPoints.ForEach(tp =>
        {
            tp.Time += offset;
            tp.Volume = vol;
        });

        return map;
    }

    private bool CanConvertPath(string path) => Path.GetExtension(path) == ".qp" || Path.GetExtension(path) == ".qua";

    private void Print(string message, int minVerbosity = 1)
    {
        if (Verbosity >= minVerbosity)
            Console.WriteLine(message);
    }
}