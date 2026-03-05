using Quaver.API.Maps.Structures;

namespace qua3osu.Osu
{
    public class HitObject
    {
        public int XPosition;
        public int YPosition = 192;
        public int Time = 0;
        public int Type;
        public int HitSounds = 0;
        public int EndTime = 0;
        public bool IsLongNote => Type == 1 << 7;

        public HitObject(HitObjectInfo hitObject, int keyCount)
        {
            Time = hitObject.StartTime;
            XPosition = 512 * hitObject.Lane / keyCount - 64;
            if (hitObject.IsLongNote)
            {
                Type = 1 << 7;
                EndTime = hitObject.EndTime;
            }
            else
                Type = 1 << 0;
            // TODO: Hitsounds
        }

        public override string ToString()
        {
            if (IsLongNote)
                return $"{XPosition},{YPosition},{Time},{Type},{HitSounds},{EndTime}:0:0:0:0:";
            else
                return $"{XPosition},{YPosition},{Time},{Type},{HitSounds},0:0:0:0:";
        }
    }
}