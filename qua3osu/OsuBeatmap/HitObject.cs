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
            Time = hitObject.StartTime + (dontUseOffset ? 0 : OsuBeatmap.QUAVER_TO_OSU_OFFSET);
            XPosition = 512 * hitObject.Lane / keyCount - 64;
            Type = hitObject.IsLongNote ? 1<<7 : 1<<0;
            EndTime = hitObject.EndTime;
            // TODO: Hitsounds
        }

        public override string ToString()
        {
            if (Type == 1<<7)
                return $"{XPosition},{YPosition},{Time},{Type},{HitSounds},{EndTime}:0:0:0:0:";
            else
                return $"{XPosition},{YPosition},{Time},{Type},{HitSounds},0:0:0:0:";
        }
    }
}