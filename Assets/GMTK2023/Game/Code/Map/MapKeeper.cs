using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

namespace GMTK2023.Game
{
    public class MapKeeper : MonoBehaviour, IMap
    {
        private IReadOnlyCollection<ILocation> locations =
            ImmutableArray<ILocation>.Empty;


        public IEnumerable<ILocation> Locations => locations;


        private void Awake()
        {
            locations = LocationDb.LoadLocations();
        }
    }
}