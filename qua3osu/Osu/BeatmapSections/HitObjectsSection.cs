using System.Text;
using Quaver.API.Maps;

namespace qua3osu.Osu.BeatmapSections
{
    public class HitObjectsSection(Qua qua) : BeatmapSection
    {
        public List<HitObject> HitObjects = qua.HitObjects
            .Select(hitObject => new HitObject(hitObject, qua.GetKeyCount()))
            .ToList();

        protected override string SectionTitle => "HitObjects";

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            HitObjects.ForEach(ho => lines.AppendLine(ho.ToString()));
            return lines.ToString();
        }
    }
}