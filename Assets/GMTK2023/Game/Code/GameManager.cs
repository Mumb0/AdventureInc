using UnityEngine;

namespace GMTK2023.Game
{
    /// <summary>
    /// Top-level game-manager.
    /// Responsible for starting/stopping the game
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private async void Start()
        {
            var savedGame = await GameSaving.TryLoadSavedGameAsync();

            // If we dont have a saved game, just load the first shift
            // NOTE: We force the nullable here because a shift should always be found
            var shift = ShiftDb.TryLoadShiftByIndex(savedGame?.ShiftIndex ?? 0)!;

            Singleton.TryFind<IShiftStarter>()!.StartShift(shift);
        }
    }
}