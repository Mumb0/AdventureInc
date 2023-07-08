using System.Collections.Generic;
using UnityEngine;

namespace GMTK2023.Game
{
    public static class LocationDb
    {
        private const string Path = "Locations";


        public static IReadOnlyCollection<ILocation> LoadLocations() =>
            Resources.LoadAll<LocationAsset>(Path);
    }
}