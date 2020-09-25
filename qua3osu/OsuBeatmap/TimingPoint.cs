using System;
using Quaver.API.Maps.Structures;

namespace qua3osu.OsuBeatmap
{
    public class TimingPoint
    {
        public int Time = 0;
        public double BeatLength = 0;
        public int Meter = 4;
        public int SampleSet = 0;
        public int SampleIndex = 0;
        public int Volume;
        public int Uninherited = 0;
        public int Kiai = 0;

        public TimingPoint(TimingPointInfo timingPoint, int volume)
        {
            Time = (int)Math.Round(timingPoint.StartTime);
            Uninherited = 0;
            // osu! can't handle 0 BPM, so it's replaced with a very low BPM value instead (0.000006 BPM).
            BeatLength = timingPoint.Bpm <= 0 ? -10e10 : timingPoint.MillisecondsPerBeat;
            Volume = volume;
        }

        public TimingPoint(SliderVelocityInfo scrollVelocity, int volume)
        {
            Time = (int)Math.Round(scrollVelocity.StartTime);
            Uninherited = 1;
            Meter = 0;
            // osu! can't handle 0x SV, so it's replaced with a very low value instead (0.00000001x)
            // It clamps all SV values between 0.01x and 10x, so putting -10e10 instead of -10e4 doesn't really
            // make a difference. It could change with Lazer though, so it's kept in.
            BeatLength = scrollVelocity.Multiplier <= 0 ? -10e10 : -100 / scrollVelocity.Multiplier;
            Volume = volume;
        }

        public override string ToString() => $"{Time},{BeatLength},{Meter},{SampleSet},{SampleIndex},{Volume},{Uninherited},{Kiai}";
    }
}