using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using GMTK2023.Game.MiniGames;
using UnityEngine;

namespace GMTK2023.Game
{
    public class MapKeeper : MonoBehaviour, IMap
    {
        [Serializable]
        public struct LocationMiniGameLink
        {
            [SerializeField] private LocationAsset location;
            [SerializeField] private MiniGame miniGame;


            public ILocation Location => location;

            public IMiniGame MiniGame => miniGame;
        }


        [SerializeField] private LocationMiniGameLink[] locationMiniGameLinks =
            Array.Empty<LocationMiniGameLink>();

        private IReadOnlyCollection<ILocation> locations =
            ImmutableArray<ILocation>.Empty;

        private IReadOnlyDictionary<ILocation, IMiniGame> miniGamesByLocation =
            new Dictionary<ILocation, IMiniGame>();


        public IEnumerable<ILocation> Locations => locations;


        public IMiniGame? TryGetMiniGameFor(ILocation location) =>
            miniGamesByLocation.TryGet(location);


        private void Awake()
        {
            locations = LocationDb.LoadLocations();
            miniGamesByLocation = locationMiniGameLinks.ToImmutableDictionary(
                it => it.Location, it => it.MiniGame);
        }
    }
}