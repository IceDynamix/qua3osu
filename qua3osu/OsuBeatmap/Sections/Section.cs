namespace qua3osu.OsuBeatmap.Sections
{
    public abstract class Section
    {
        public abstract string SectionTitle { get; }

        public string FormatTitle() => $"[{SectionTitle}]";
        public abstract override string ToString();
    }
}