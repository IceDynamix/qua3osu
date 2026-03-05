# qua3osu

Convert Quaver .qp/.qua files to osu! .osz/.osu files

[Download here](https://github.com/IceDynamix/qua3osu/releases/tag/latest)

## Run with Drag&Drop

https://user-images.githubusercontent.com/22303902/140838272-28793396-ce55-42d4-a69f-f2f90f6187a5.mp4

## Run through the command line

Run the project with the `--help` flag to get everything you need. If you
downloaded from releases, open up a command line and run `qua3osu.exe --help`.

```
Description:
  Convert Quaver .qp/.qua files to osu! .osz/.osu files

Usage:
  qua3osu <input>... [options]

Arguments:
  <input>  Path(s) to directories containing .qp/.qua files, or direct file path(s)

Options:
  --output <output>               Specifies the output directory, uses original directory of .qp/.qua by default
  --od <od>                       Overall difficulty as a number between 0 and 10 [default: 8]
  --hp <hp>                       HP drain as a number between 0 and 10 [default: 8]
  --volume <volume>               Hitsound volume for the entire map between 0 and 100 [default: 20]
  -c, --creator <creator>         Changes the creator username for all maps
  --sampleset <drum|normal|soft>  Hitsound sample set [default: soft]
  --audio-offset <audio-offset>   Quaver times by waveform while osu! times by ear, so there's a difference of about
                                  23ms. You can specify your own offset if needed. [default: -23]
  -v, --verbosity <verbosity>     Output verbosity level in range [0, 3] (0 is quiet) [default: 1]
  -?, -h, --help                  Show help and usage information
  --version                       Show version information
```

Example commands:

Regular conversion of a mapset file called my-mapset.qp: `<path>/qua3osu.exe my-mapset.qp`
With OD 8 and the creator name changed to IceDynamix: `<path>/qua3osu.exe my-mapset.qp --od 8 --creator IceDynamix`

## Build yourself

```bash
git clone https://github.com/IceDynamix/qua3osu --recurse-submodules
cd qua3osu
dotnet run --project qua3osu -- <args>
```

Makes releases. Output will be in `./bin/Release/<runtime>/<platform>/publish`

```
dotnet publish -r win-x64
dotnet publish -r linux-x64
```