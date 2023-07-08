using System;
using static GMTK2023.GameSaving;

namespace GMTK2023.Game
{
    public interface IGameLoader
    {
        public record GameLoadEvent(SavedGame Game);


        public event Action<GameLoadEvent> OnGameLoaded;
    }
}