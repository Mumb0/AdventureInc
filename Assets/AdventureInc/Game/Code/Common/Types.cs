using System;
using static AdventureInc.GameSaving;

namespace AdventureInc.Game
{
    public interface IGameLoader
    {
        public record GameLoadEvent(GameSaving.SavedGame Game);


        /// <summary>
        /// Invoked when the game is loaded
        /// </summary>
        public event Action<GameLoadEvent> GameLoaded;
    }

    public interface IGameOverTracker
    {
        public record GameOverEvent;


        /// <summary>
        /// Invoked when the player lost the game
        /// </summary>
        public event Action<GameOverEvent> GameOver;
    }

    public interface ICredibilityTracker
    {
        public record CredibilityChangedEvent(int Credibility);


        public event Action<CredibilityChangedEvent> CredibilityChanged;
    }

    public interface IIngameTimeKeeper
    {
        public int Hour { get; }
    }
}