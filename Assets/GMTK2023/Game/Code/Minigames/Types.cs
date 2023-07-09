using System;
using System.Collections.Generic;

namespace GMTK2023.Game.MiniGames
{
    public interface IMiniGame
    {
        public string Name { get; }

        public bool IsCredible { get; }

        public TimeSpan Duration { get; }
    }

    public interface IMiniGameTracker
    {
        public IReadOnlyList<IMiniGame> AllMiniGames { get; }
    }
}