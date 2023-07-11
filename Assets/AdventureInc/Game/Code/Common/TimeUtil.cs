using System;
using UnityEngine;

namespace GMTK2023.Game
{
    public static class TimeUtil
    {
        public static TimeSpan TimeSinceUnityStart => TimeSpan.FromSeconds(Time.time);
    }
}