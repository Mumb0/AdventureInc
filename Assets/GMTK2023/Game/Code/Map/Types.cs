using System.Collections.Generic;
using GMTK2023.Game.MiniGames;

namespace GMTK2023.Game
{
    public interface ILocation
    {
        public string Name { get; }
    }


    public interface IMap
    {
        public IEnumerable<ILocation> Locations { get; }


        /// <remarks>This will be null if the location has no mini-game</remarks>
        public IMiniGame? TryGetMiniGameFor(ILocation location);

        public ILocation LocationOf(IMiniGame miniGame);

        public bool HasMiniGameAt(ILocation location) =>
            TryGetMiniGameFor(location) != null;
    }

    public interface IRoutePlanner
    {
        ILocation FindNextLocationOnRoute(ILocation start, ILocation target);
    }
}