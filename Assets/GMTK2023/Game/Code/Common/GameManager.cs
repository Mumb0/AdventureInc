using System;
using UnityEngine;

namespace GMTK2023.Game
{
    /// <summary>
    /// Top-level game-manager.
    /// Responsible for starting/stopping the game
    /// </summary>
    public class GameManager : MonoBehaviour, IGameLoader
    {
        public event Action<IGameLoader.GameLoadEvent>? GameLoaded;


        private async void Start()
        {
            // Load game or start new if there is none
            var savedGame = await GameSaving.TryLoadSavedGameAsync()
                            ?? await GameSaving.StartNewGameAsync();

            GameLoaded?.Invoke(new IGameLoader.GameLoadEvent(savedGame));
        }
    }
}