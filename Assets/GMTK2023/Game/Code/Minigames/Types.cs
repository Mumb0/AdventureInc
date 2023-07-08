using System.Collections.Generic;
using System.Collections.Immutable;

namespace GMTK2023.Game.MiniGames
{
    public interface IMiniGame
    {
    }

    public interface IMiniGameTracker
    {
        public IReadOnlyList<MiniGame> AllMiniGames { get; }
    }
}