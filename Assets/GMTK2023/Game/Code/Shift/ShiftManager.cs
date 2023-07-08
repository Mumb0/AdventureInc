using System;
using UnityEngine;

namespace GMTK2023.Game
{
    public class ShiftManager : MonoBehaviour, IShiftLoader, IShiftProgressTracker
    {
        public event Action<IShiftLoader.ShiftLoadedEvent>? OnShiftLoaded;

        public event Action<IShiftProgressTracker.ShiftStartedEvent>? OnShiftStarted;


        private void StartShift(IShiftInfo shiftInfo)
        {
            OnShiftStarted?.Invoke(new IShiftProgressTracker.ShiftStartedEvent());
        }

        private void OnGameLoaded(IGameLoader.GameLoadEvent e)
        {
            // NOTE: We force the nullable here because a shift should always be found
            var shift = ShiftDb.TryLoadShiftByIndex(e.Game.ShiftIndex)!;

            OnShiftLoaded?.Invoke(new IShiftLoader.ShiftLoadedEvent(shift));

            StartShift(shift);
        }

        private void Awake()
        {
            Singleton.TryFind<IGameLoader>()!.OnGameLoaded += OnGameLoaded;
        }
    }
}