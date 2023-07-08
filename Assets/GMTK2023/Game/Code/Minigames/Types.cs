using System.Collections.Generic;

namespace GMTK2023.Game.MiniGames
{
    public interface IMiniGame
    {
    }

    public interface IMiniGameTracker
    {
        public IReadOnlyList<IMiniGame> AllMiniGames { get; }
    }
}