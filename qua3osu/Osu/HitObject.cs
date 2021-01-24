using System;
using Quaver.API.Maps.Structures;

namespace qua3osu.OsuBeatmap
{
    public class HitObject
    {
        public int XPosition;
        public int YPosition = 192;
        public int Time = 0;
        public int Type;
        public int HitSounds = 0;
        public int EndTime = 0;

        public HitObject(HitObjectInfo hitObject, int keyCount, bool dontUseOffset)
        {
            var offset = dontUseOffset ? 0 : Osu.OsuBeatmap.QUAVER_TO_OSU_OFFSET;
            Time = hitObject.StartTime + offset;
            XPosition = 512 * hitObject.Lane / keyCount - 64;
            if (hitObject.IsLongNote)
            {
                Type = 1 << 7;
                EndTime = hitObject.EndTime + offset;
            }
            else
                Type = 1 << 0;
            // TODO: Hitsounds
        }

        public override string ToString()
        {
            if (Type == 1 << 7)
                return $"{XPosition},{YPosition},{Time},{Type},{HitSounds},{EndTime}:0:0:0:0:";
            else
                return $"{XPosition},{YPosition},{Time},{Type},{HitSounds},0:0:0:0:";
        }
    }
}