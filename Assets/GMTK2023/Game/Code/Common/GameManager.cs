using System;
using UnityEngine;

namespace GMTK2023.Game
{
    /// <summary>
    /// Top-level game-manager.
    /// Responsible for starting/stopping the game
    /// </summary>
    public class GameManager : MonoBehaviour, IGameLoader, IGameOverTracker
    {
        public event Action<IGameLoader.GameLoadEvent>? GameLoaded;

        public event Action<IGameOverTracker.GameOverEvent>? GameOver;


        private async void Start()
        {
            // Load game or start new if there is none
            var savedGame = await GameSaving.TryLoadSavedGameAsync()
                            ?? await GameSaving.StartNewGameAsync();

            GameLoaded?.Invoke(new IGameLoader.GameLoadEvent(savedGame));
        }

        private void OnCredibilityChanged(ICredibilityTracker.CredibilityChangedEvent e)
        {
            if (e.Credibility > 0) return;
            GameOver?.Invoke(new IGameOverTracker.GameOverEvent());
        }

        private void Awake()
        {
            Singleton.TryFind<ICredibilityTracker>()!.CredibilityChanged +=
                OnCredibilityChanged;
        }
    }
}