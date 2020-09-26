namespace qua3osu.Osu.BeatmapSections
{
    public abstract class BeatmapSection
    {
        public abstract string SectionTitle { get; }

        public string FormatTitle() => $"[{SectionTitle}]";
        public abstract override string ToString();
    }
}