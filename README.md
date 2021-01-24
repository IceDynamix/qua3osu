# qua3osu

This is a rewrite of [qua2osu](https://github.com/IceDynamix/qua2osu) in C#
instead of Python. Using [Quaver.API](https://github.com/Quaver/Quaver.API)
makes conversion a lot easier.

Run the project with the `--help` flag to get everything you need. If you
downloaded from releases, open up a command line and run `qua3osu.exe --help`.

```
qua3osu 1.0.0
Copyright (C) 2020 qua3osu

  -o, --output                   Specifies the output directory, uses original directory of .qp by default

  --od                           (Default: 8) Overall difficulty as a number between 0 and 10

  --hp                           (Default: 8) HP drain as a number between 0 and 10

  -v, --volume                   (Default: 20) Hitsound volume for the entire map

  -c, --creator                  Changes the creator username for all maps

  -r, --recursive                (Default: false) Looks for .qp in all subdirectories of given directories

  --sampleset-normal             (Default: false) Use normal sampleset instead of soft

  --sampleset-drum               (Default: false) Use drum sampleset instead of soft

  --dont-apply-default-offset    (Default: false) Quaver times by waveform while osu! times by ear, so there's a
                                 difference of about 23 milliseconds. This removes the automatic adjustment of that
                                 offset.

  --help                         Display this help screen.

  --version                      Display version information.

  input-paths (pos. 0)           Required. Path(s) to directories containing .qp files or direct .qp file path(s)
```

Example commands:

Regular conversion of a mapset file called my-mapset.qp: `<path>/qua3osu.exe my-mapset.qp`
With OD 8 and the creator name changed to IceDynamix: `<path>/qua3osu.exe my-mapset.qp --od 8 --creator IceDynamix`
