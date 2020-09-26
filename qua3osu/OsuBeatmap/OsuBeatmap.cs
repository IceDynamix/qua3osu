using System.Collections.Generic;
using System.Text;
using qua3osu.OsuBeatmap.Sections;
using Quaver.API.Maps;

namespace qua3osu.OsuBeatmap
{
    public class OsuBeatmap
    {
        public const int QUAVER_TO_OSU_OFFSET = -23;
        public GeneralSection GeneralSection;
        public EditorSection EditorSection;
        public MetadataSection MetadataSection;
        public DifficultySection DifficultySection;
        public EventsSection EventsSection;
        public TimingPointsSection TimingPointsSection;
        public HitObjectsSection HitObjectsSection;

        public OsuBeatmap(Qua qua, Arguments args)
        {
            GeneralSection = new GeneralSection(qua, args);
            EditorSection = new EditorSection(qua, args);
            MetadataSection = new MetadataSection(qua, args);
            DifficultySection = new DifficultySection(qua, args);
            EventsSection = new EventsSection(qua, args);
            TimingPointsSection = new TimingPointsSection(qua, args);
            HitObjectsSection = new HitObjectsSection(qua, args);
        }

        public string SongString => $"{MetadataSection.Artist} - {MetadataSection.Title} [{MetadataSection.Version}]";

        public override string ToString()
        {
            var sections = new List<Section>
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