using System.Linq;
using UnityEngine;

namespace GMTK2023
{
    public static class Chance
    {
        public static float Or(params float[] chances)
        {
            if (chances.Length == 0) return 0;

            return Mathf.Clamp01(chances.Sum());
        }

        public static float And(params float[] chances)
        {
            if (chances.Length == 0) return 0;

            return Mathf.Clamp01(chances.Aggregate((a, b) => a * b));
        }

        public static bool Roll(float chance) =>
            Random.Range(0f, 1f) < chance;
    }
}