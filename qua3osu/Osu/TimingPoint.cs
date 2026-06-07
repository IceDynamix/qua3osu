using Quaver.API.Maps.Structures;

namespace qua3osu.Osu
{
    public class TimingPoint
    {
        public float Time = 0;
        public double BeatLength = 0;
        public int Meter = 4;
        public int SampleSet = 0;
        public int SampleIndex = 0;
        public int Volume = 20;
        public int Uninherited = 0;
        public int Kiai = 0;

        private TimingPoint()
        {
            
        }

        public TimingPoint(TimingPointInfo timingPoint)
        {
            Time = timingPoint.StartTime;
            Uninherited = 1;
            // osu! can't handle 0 BPM, so it's replaced with a very low BPM value instead (0.000006 BPM).
            BeatLength = timingPoint.Bpm <= 0 ? 10e10 : timingPoint.MillisecondsPerBeat;
        }

        public TimingPoint(SliderVelocityInfo scrollVelocity)
        {
            SetSliderVelocity(scrollVelocity.StartTime, scrollVelocity.Multiplier);
        }

        public static TimingPoint TimingChange(float time, double bpm)
        {
            var timingPoint = new TimingPoint { Time = time, Uninherited = 1 };
            // osu! can't handle 0 BPM, so it's replaced with a very low BPM value instead (0.000006 BPM).
            timingPoint.BeatLength = bpm <= 0 ? 10e10 : 60000 / bpm;
            return timingPoint;
        }

        public static TimingPoint SliderVelocity(float time, double multiplier)
        {
            var timingPoint = new TimingPoint();
            timingPoint.SetSliderVelocity(time, multiplier);
            return timingPoint;
        }

        private void SetSliderVelocity(float time, double multiplier)
        {
            Time = time;
            Uninherited = 0;
            Meter = 0;
            // osu! clamps inherited timing point multipliers between 0.01x and 10x.
            BeatLength = multiplier <= 0 ? -10000 : -100 / multiplier;
        }

        public override string ToString() =>
            $"{Time},{BeatLength},{Meter},{SampleSet},{SampleIndex},{Volume},{Uninherited},{Kiai}";
    }
}
