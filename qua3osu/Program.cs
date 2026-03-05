using CommandLine;
using qua3osu;

Parser.Default.ParseArguments<Arguments>(args)
    .WithParsed(ConvertListOfMapsets);

void ConvertListOfMapsets(Arguments args)
{
    args.Validate();

    args.Print(args.ToString(), 3);

    var listOfFiles = new List<string>();
    foreach (var inputPath in args.Paths)
    {
        if (Directory.Exists(inputPath))
        {
            listOfFiles.AddRange(Directory.GetFiles(inputPath).Where(CanConvert));
        }
        else if (File.Exists(inputPath) && CanConvert(inputPath))
        {
            listOfFiles.Add(inputPath);
        }
    }

    if (listOfFiles.Count == 0)
        args.Print("No files found");
    else
        args.Print($"Found {listOfFiles.Count} maps or mapsets to convert", 2);

    foreach (var path in listOfFiles)
    {
        args.Print($"Converting {path}", 2);
        try
        {
            if (Path.GetExtension(path) == ".qp")
            {
                Conversion.ConvertMapset(path, args);
                Console.WriteLine($"Finished converting mapset {path}", 2);
            }
            else
            {
                Conversion.ConvertMapFile(path, args);
                Console.WriteLine($"Finished converting map {path}", 2);
            }
        }
        catch (Exception e)
        {
            args.Print($"Could not convert map or mapset {path}, {e.Message}");
        }
    }
}

bool CanConvert(string path) => Path.GetExtension(path) == ".qp" || Path.GetExtension(path) == ".qua";