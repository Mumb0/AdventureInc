using System;
using UnityEngine;
using static GMTK2023.Game.IShiftLoader;
using static GMTK2023.Game.IShiftProgressTracker;

namespace GMTK2023.Game
{
    public class ShiftManager : MonoBehaviour, IShiftLoader, IShiftProgressTracker
    {
        private record Shift(IShiftInfo Info, float StartTimeSeconds);


        public event Action<ShiftLoadedEvent>? OnShiftLoaded;
        public event Action<ShiftStartedEvent>? OnShiftStarted;
        public event Action<ShiftProgressEvent>? OnShiftProgress;


        private Shift? currentShift;


        private void ProgressShift(Shift shift)
        {
            var secondsSinceStart = Time.time - shift.StartTimeSeconds;
            var timeSinceStart = TimeSpan.FromSeconds(secondsSinceStart);

            OnShiftProgress?.Invoke(new ShiftProgressEvent(timeSinceStart));
        }

        private void Update()
        {
            if (currentShift == null) return;
            ProgressShift(currentShift);
        }

        private void StartShift(IShiftInfo shiftInfo)
        {
            currentShift = new Shift(shiftInfo, Time.time);

            OnShiftStarted?.Invoke(new ShiftStartedEvent());

            ProgressShift(currentShift);
        }

        private void OnGameLoaded(IGameLoader.GameLoadEvent e)
        {
            // NOTE: We force the nullable here because a shift should always be found
            var shift = ShiftDb.TryLoadShiftByIndex(e.Game.ShiftIndex)!;

            OnShiftLoaded?.Invoke(new ShiftLoadedEvent(shift));

            StartShift(shift);
        }

        private void Awake()
        {
            Singleton.TryFind<IGameLoader>()!.OnGameLoaded += OnGameLoaded;
        }
    }
}