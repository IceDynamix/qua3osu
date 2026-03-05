using System.Text;
using qua3osu.Osu.BeatmapSections;
using Quaver.API.Maps;

namespace qua3osu.Osu
{
    public class OsuBeatmap(Qua qua)
    {
        public readonly GeneralSection GeneralSection = new(qua);
        public readonly EditorSection EditorSection = new EditorSection(qua);
        public readonly MetadataSection MetadataSection = new MetadataSection(qua);
        public readonly DifficultySection DifficultySection = new DifficultySection(qua);
        public readonly EventsSection EventsSection = new EventsSection(qua);
        public readonly TimingPointsSection TimingPointsSection = new TimingPointsSection(qua);
        public readonly HitObjectsSection HitObjectsSection = new HitObjectsSection(qua);

        public string SongString => $"{MetadataSection.Artist} - {MetadataSection.Title} [{MetadataSection.Version}]";

        public override string ToString()
        {
            var sections = new List<BeatmapSection>
            {
                GeneralSection,
                EditorSection,
                MetadataSection,
                DifficultySection,
                EventsSection,
                TimingPointsSection,
                HitObjectsSection,
            };

            var lines = new StringBuilder();
            lines.AppendLine("osu file format v14\n");
            sections.ForEach(section => lines.AppendLine(section.ToString()));
            return lines.ToString();
        }
    }
}