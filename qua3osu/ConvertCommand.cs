using System.CommandLine;

namespace qua3osu;

public class ConvertCommand : RootCommand
{
    public readonly Argument<string[]> Input = new("input")
    {
        Arity = ArgumentArity.OneOrMore,
        Description = "Path(s) to directories containing .qp/.qua files, or direct file path(s)"
    };

    public readonly Option<string?> Output = new("--output")
    {
        Description = "Specifies the output directory, uses original directory of .qp/.qua by default",
    };

    public readonly Option<double> Od = new("--od")
    {
        DefaultValueFactory = _ => 8,
        Description = "Overall difficulty as a number between 0 and 10"
    };

    public readonly Option<double> Hp = new("--hp")
    {
        DefaultValueFactory = _ => 8,
        Description = "HP drain as a number between 0 and 10"
    };

    public readonly Option<int> Volume = new("--volume")
    {
        DefaultValueFactory = _ => 20,
        Description = "Hitsound volume for the entire map between 0 and 100"
    };

    public readonly Option<string?> Creator = new("--creator", "-c")
    {
        Description = "Changes the creator username for all maps"
    };

    public readonly Option<string> SampleSet = new("--sampleset")
    {
        DefaultValueFactory = _ => "soft",
        Description = "Hitsound sample set"
    };

    public readonly Option<int> Offset = new("--audio-offset")
    {
        DefaultValueFactory = _ => -23,
        Description =
            "Quaver times by waveform while osu! times by ear, so there's a difference of about 23ms. You can specify your own offset if needed."
    };

    public readonly Option<int> Verbosity = new("--verbosity", "-v")
    {
        Description = "Output verbosity level in range [0, 3] (0 is quiet)",
        Recursive = true,
        Arity = ArgumentArity.ZeroOrOne,
        DefaultValueFactory = _ => 1
    };

    public ConvertCommand()
    {
        Arguments.Add(Input);

        Options.Add(Output);

        Options.Add(Od);
        Validators.Add(r =>
        {
            if (r.GetValue(Od) is < 0 or > 10)
                r.AddError("OD must be in [0, 10]");
        });

        Options.Add(Hp);
        Validators.Add(r =>
        {
            if (r.GetValue(Hp) is < 0 or > 10)
                r.AddError("HP must be in [0, 10]");
        });

        Options.Add(Volume);
        Validators.Add(r =>
        {
            if (r.GetValue(Volume) is < 0 or > 100)
                r.AddError("Volume must be in [0, 100]");
        });

        Options.Add(Creator);

        SampleSet.AcceptOnlyFromAmong("soft", "normal", "drum");
        Options.Add(SampleSet);

        Options.Add(Offset);
        
        Options.Add(Verbosity);
    }
}