using System;
using UnityEngine;

namespace AdventureInc.Game
{
    public static class TimeUtil
    {
        public static TimeSpan TimeSinceUnityStart => TimeSpan.FromSeconds(Time.time);
    }
}