using System;
using static GMTK2023.GameSaving;

namespace GMTK2023.Game
{
    public interface IGameLoader
    {
        public record GameLoadEvent(SavedGame Game);


        /// <summary>
        /// Invoked when the game is loaded
        /// </summary>
        public event Action<GameLoadEvent> GameLoaded;
    }

    public interface ICredibilityTracker
    {
        public record CredibilityChangedEvent(int Credibility);


        public event Action<CredibilityChangedEvent> CredibilityChanged;
    }
}