using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private void HandleGameOver()
        {
            GameOver?.Invoke(new IGameOverTracker.GameOverEvent());

            // TODO: Go to game over screen instead
            SceneManager.LoadScene(0); // Menu
        }

        private void OnCredibilityChanged(ICredibilityTracker.CredibilityChangedEvent e)
        {
            if (e.Credibility > 0) return;
            HandleGameOver();
        }

        private void OnShiftCompleted(IShiftProgressTracker.ShiftCompletedEvent obj)
        {
            SceneManager.LoadScene(2); // Success
        }

        private void Awake()
        {
            Singleton.TryFind<ICredibilityTracker>()!.CredibilityChanged +=
                OnCredibilityChanged;
            Singleton.TryFind<IShiftProgressTracker>()!.ShiftCompleted +=
                OnShiftCompleted;
        }


        public void Quit()
        {
            Application.Quit();
        }
    }
}