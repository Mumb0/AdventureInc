using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
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

        [Serializable]
        public class Connection
        {
            [SerializeField] private LocationAsset? a;
            [SerializeField] private LocationAsset? b;
            [SerializeField] private float distance;

            public ILocation A => a!;

            public ILocation B => b!;

            public float Distance => distance;
        }

        private record Route(
            IImmutableList<ILocation> Locations,
            float Length);


        [SerializeField] private LocationMiniGameLink[] locationMiniGameLinks =
            Array.Empty<LocationMiniGameLink>();

        [SerializeField] private Connection[] connections =
            Array.Empty<Connection>();

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

        private IEnumerable<ILocation> ConnectedLocations(ILocation location)
        {
            return connections.TrySelect(route =>
            {
                if (route.A == location) return route.B;
                if (route.B == location) return route.A;
                return null;
            });
        }

        private float? TryDistanceBetween(ILocation a, ILocation b)
        {
            var connection = connections.FirstOrDefault(it =>
                (it.A == a && it.B == b) || (it.A == b && it.B == a));
            return connection?.Distance;
        }

        public ILocation FindNextLocationOnRoute(ILocation start, ILocation target)
        {
            if (start == target) return target;

            IEnumerable<Route> FindRoutesStartingFrom(Route routeSoFar)
            {
                var current = routeSoFar.Locations.Last();

                if (current == target) return routeSoFar.Yield();

                var connected = ConnectedLocations(current);
                var possible = connected.Except(routeSoFar.Locations).ToArray();

                return possible.SelectMany(it =>
                {
                    var distance =
                        TryDistanceBetween(current, it) ?? float.PositiveInfinity;
                    var nextRoute = new Route(
                        routeSoFar.Locations.Add(it),
                        routeSoFar.Length + distance);
                    return FindRoutesStartingFrom(nextRoute);
                });
            }

            var routes = FindRoutesStartingFrom(
                new Route(ImmutableArray<ILocation>.Empty.Add(start), 0));

            // NOTE: We assume there always is a route
            var bestRoute = routes
                .OrderBy(route => route.Length)
                .First();

            return bestRoute.Locations.ElementAt(1);
        }
    }
}