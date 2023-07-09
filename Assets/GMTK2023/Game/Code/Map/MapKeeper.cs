using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GMTK2023.Game.MiniGames;
using UnityEngine;

namespace GMTK2023.Game
{
    public class MapKeeper : MonoBehaviour, IMap, IRoutePlanner
    {
        [Serializable]
        public class LocationMiniGameLink
        {
            [SerializeField] private LocationAsset? location;
            [SerializeField] private MiniGame? miniGame;


            public ILocation Location => location!;

            public IMiniGame MiniGame => miniGame!;
        }


        [SerializeField] private LocationMiniGameLink[] locationMiniGameLinks =
            Array.Empty<LocationMiniGameLink>();

        private IReadOnlyCollection<ILocation> locations =
            ImmutableArray<ILocation>.Empty;


        public IEnumerable<ILocation> Locations => locations;


        public IMiniGame? TryGetMiniGameFor(ILocation location) =>
            locationMiniGameLinks
                .FirstOrDefault(it => it.Location == location)
                ?.MiniGame;

        public ILocation LocationOf(IMiniGame miniGame) =>
            locationMiniGameLinks
                // NOTE: We assume existence because every mini-game should have a location
                .First(it => it.MiniGame == miniGame).Location;

        private void Awake()
        {
            locations = LocationDb.LoadLocations();
        }

        public ILocation FindNextLocationOnRoute(ILocation current, ILocation target)
        {
            // TODO: Implement actual route finding
            return target;
        }
    }
}