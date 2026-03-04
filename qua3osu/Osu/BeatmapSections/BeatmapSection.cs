namespace qua3osu.Osu.BeatmapSections
{
    public abstract class BeatmapSection
    {
        protected abstract string SectionTitle { get; }

        protected string FormatTitle() => $"[{SectionTitle}]";
        public abstract override string ToString();
    }
}