using System;
using UnityEngine;
using static GMTK2023.Game.IShiftLoader;
using static GMTK2023.Game.IShiftProgressTracker;

namespace GMTK2023.Game
{
    public class ShiftManager : MonoBehaviour, IShiftLoader, IShiftProgressTracker
    {
        private record Shift(IShiftInfo Info, float StartTimeSeconds);


        public event Action<ShiftLoadedEvent>? ShiftLoaded;
        public event Action<ShiftStartedEvent>? ShiftStarted;
        public event Action<ShiftProgressEvent>? ShiftProgressed;


        private Shift? currentShift;


        private void ProgressShift(Shift shift)
        {
            var secondsSinceStart = Time.time - shift.StartTimeSeconds;
            var timeSinceStart = TimeSpan.FromSeconds(secondsSinceStart);

            ShiftProgressed?.Invoke(new ShiftProgressEvent(timeSinceStart));
        }

        private void Update()
        {
            if (currentShift == null) return;
            ProgressShift(currentShift);
        }

        private void StartShift(IShiftInfo shiftInfo)
        {
            currentShift = new Shift(shiftInfo, Time.time);

            ShiftStarted?.Invoke(new ShiftStartedEvent());

            ProgressShift(currentShift);
        }

        private void OnGameLoaded(IGameLoader.GameLoadEvent e)
        {
            // NOTE: We force the nullable here because a shift should always be found
            var shift = ShiftDb.TryLoadShiftByIndex(e.Game.ShiftIndex)!;

            ShiftLoaded?.Invoke(new ShiftLoadedEvent(shift));

            StartShift(shift);
        }

        private void Awake()
        {
            Singleton.TryFind<IGameLoader>()!.GameLoaded += OnGameLoaded;
        }
    }
}