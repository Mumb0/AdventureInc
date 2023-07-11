using System.Collections.Generic;
using UnityEngine;

namespace AdventureInc.Game
{
    public static class LocationDb
    {
        private const string Path = "Locations";


        public static IReadOnlyCollection<ILocation> LoadLocations() =>
            Resources.LoadAll<LocationAsset>(Path);
    }
}