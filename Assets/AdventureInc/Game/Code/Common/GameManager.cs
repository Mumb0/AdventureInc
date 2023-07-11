using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GMTK2023.GameSaving;

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


        private SavedGame savedGame = null!;


        private async void Start()
        {
            // Load game or start new if there is none
            savedGame = await TryLoadSavedGameAsync()
                        ?? await StartNewGameAsync();

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

        private async void OnShiftCompleted(IShiftProgressTracker.ShiftCompletedEvent obj)
        {
            if (savedGame.ShiftIndex < ShiftDb.ShiftCount - 1)
                await SaveAsync(new SavedGame(ShiftIndex: savedGame.ShiftIndex + 1));
            else
                _ = await StartNewGameAsync();

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