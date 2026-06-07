using System.Text;
using Quaver.API.Maps;

namespace qua3osu.Osu.BeatmapSections
{
    public class TimingPointsSection : BeatmapSection
    {
        private const float MinOsuSliderVelocity = 0.01f;
        private const float MaxOsuSliderVelocity = 10f;
        private const double FloatTolerance = 0.000001;

        public List<TimingPoint> TimingPoints;

        public TimingPointsSection(Qua qua)
        {
            if (qua.BPMDoesNotAffectScrollVelocity)
            {
                qua.SortTimingPoints();
                qua.SortSliderVelocities();
                qua.DenormalizeSVs();
            }

            TimingPoints = BuildTimingPoints(qua);
        }

        protected override string SectionTitle => "TimingPoints";

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            TimingPoints.ForEach(tp => lines.AppendLine(tp.ToString()));
            return lines.ToString();
        }

        private static List<TimingPoint> BuildTimingPoints(Qua qua)
        {
            var timingPoints = qua.TimingPoints.OrderBy(x => x.StartTime).ToList();
            var sliderVelocities = qua.SliderVelocities.OrderBy(x => x.StartTime).ToList();

            if (timingPoints.Count == 0)
                return sliderVelocities.Select(sv => new TimingPoint(sv)).ToList();

            var output = new List<TimingPoint>();
            var timingPointIndex = 0;
            var sliderVelocityIndex = 0;

            var currentBpm = timingPoints[0].Bpm;
            var currentOutputBpm = (double) currentBpm;
            var currentSliderVelocity = 1f;
            var hasSliderVelocity = false;
            var hasOutputTimingPoint = false;

            while (timingPointIndex < timingPoints.Count || sliderVelocityIndex < sliderVelocities.Count)
            {
                var nextTimingPointTime = timingPointIndex < timingPoints.Count
                    ? timingPoints[timingPointIndex].StartTime
                    : float.PositiveInfinity;
                var nextSliderVelocityTime = sliderVelocityIndex < sliderVelocities.Count
                    ? sliderVelocities[sliderVelocityIndex].StartTime
                    : float.PositiveInfinity;
                var time = Math.Min(nextTimingPointTime, nextSliderVelocityTime);

                while (timingPointIndex < timingPoints.Count && timingPoints[timingPointIndex].StartTime == time)
                {
                    var timingPoint = timingPoints[timingPointIndex++];
                    currentBpm = timingPoint.Bpm;
                    currentOutputBpm = currentBpm;
                    output.Add(new TimingPoint(timingPoint));
                    hasOutputTimingPoint = true;
                }

                while (sliderVelocityIndex < sliderVelocities.Count && sliderVelocities[sliderVelocityIndex].StartTime == time)
                {
                    var sliderVelocity = sliderVelocities[sliderVelocityIndex++];
                    currentSliderVelocity = sliderVelocity.Multiplier;
                    hasSliderVelocity = true;
                }

                if (!hasSliderVelocity)
                    continue;

                var (targetBpm, targetSliderVelocity) = GetOutputTiming(currentBpm, currentSliderVelocity);

                if (!hasOutputTimingPoint || !NearlyEqual(currentOutputBpm, targetBpm))
                {
                    output.Add(TimingPoint.TimingChange(time, targetBpm, true));
                    currentOutputBpm = targetBpm;
                    hasOutputTimingPoint = true;
                }

                output.Add(TimingPoint.SliderVelocity(time, targetSliderVelocity));
            }

            return output;
        }

        private static (double Bpm, double SliderVelocity) GetOutputTiming(float bpm, float sliderVelocity)
        {
            if (bpm <= 0 || sliderVelocity <= 0)
                return (bpm, MinOsuSliderVelocity);

            if (sliderVelocity < MinOsuSliderVelocity)
                return (bpm * sliderVelocity / MinOsuSliderVelocity, MinOsuSliderVelocity);

            if (sliderVelocity > MaxOsuSliderVelocity)
                return (bpm * sliderVelocity / MaxOsuSliderVelocity, MaxOsuSliderVelocity);

            return (bpm, sliderVelocity);
        }

        private static bool NearlyEqual(double x, double y) => Math.Abs(x - y) < FloatTolerance;
    }
}
