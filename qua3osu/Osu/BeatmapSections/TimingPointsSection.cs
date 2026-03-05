using System.Text;
using Quaver.API.Maps;

namespace qua3osu.Osu.BeatmapSections
{
    public class TimingPointsSection : BeatmapSection
    {
        public List<TimingPoint> TimingPoints;

        public TimingPointsSection(Qua qua)
        {
            if (qua.BPMDoesNotAffectScrollVelocity)
            {
                qua.SortTimingPoints();
                qua.SortSliderVelocities();
                qua.DenormalizeSVs();
            }

            TimingPoints = qua.TimingPoints
                .Select(timingPoint => new TimingPoint(timingPoint))
                .Concat(qua.SliderVelocities.Select(sv => new TimingPoint(sv)))
                .OrderBy(x => x.Time)
                .ToList();
        }

        protected override string SectionTitle => "TimingPoints";

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            TimingPoints.ForEach(tp => lines.AppendLine(tp.ToString()));
            return lines.ToString();
        }
    }
}