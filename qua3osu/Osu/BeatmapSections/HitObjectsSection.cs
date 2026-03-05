using System.Text;
using Quaver.API.Maps;

namespace qua3osu.Osu.BeatmapSections
{
    public class HitObjectsSection : BeatmapSection
    {
        public List<HitObject> HitObjects;

        public HitObjectsSection(Qua qua, Arguments args)
        {
            HitObjects = qua.HitObjects
                .Select(hitObject => new HitObject(hitObject, qua.GetKeyCount(), args.AudioOffset))
                .ToList();
        }

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