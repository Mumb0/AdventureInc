using System;
using UnityEngine;
using static AdventureInc.Game.IShiftLoader;
using static AdventureInc.Game.IShiftProgressTracker;

namespace AdventureInc.Game
{
    public class ShiftManager : MonoBehaviour, IShiftLoader, IShiftProgressTracker
    {
        private record Shift(IShiftInfo Info, float StartTimeSeconds);


        public event Action<IShiftLoader.ShiftLoadedEvent>? ShiftLoaded;
        public event Action<IShiftProgressTracker.ShiftStartedEvent>? ShiftStarted;
        public event Action<IShiftProgressTracker.ShiftProgressEvent>? ShiftProgressed;
        public event Action<IShiftProgressTracker.ShiftCompletedEvent>? ShiftCompleted;


        [SerializeField] private float shiftDurationInMinutes;

        private Shift? currentShift;


        private TimeSpan ShiftDuration => TimeSpan.FromMinutes(shiftDurationInMinutes);


        private void CompleteShift()
        {
            currentShift = null;
            ShiftCompleted?.Invoke(new IShiftProgressTracker.ShiftCompletedEvent());
        }

        private void ProgressShift(Shift shift)
        {
            var secondsSinceStart = Time.time - shift.StartTimeSeconds;
            var timeSinceStart = TimeSpan.FromSeconds(secondsSinceStart);
            var t = (float) (timeSinceStart.TotalSeconds / ShiftDuration.TotalSeconds);

            ShiftProgressed?.Invoke(new IShiftProgressTracker.ShiftProgressEvent(timeSinceStart, t));

            if (timeSinceStart >= ShiftDuration)
                CompleteShift();
        }

        private void Update()
        {
            if (currentShift == null) return;
            ProgressShift(currentShift);
        }

        private void StartShift(IShiftInfo shiftInfo)
        {
            currentShift = new Shift(shiftInfo, Time.time);

            ShiftStarted?.Invoke(new IShiftProgressTracker.ShiftStartedEvent());

            ProgressShift(currentShift);
        }

        private void OnGameLoaded(IGameLoader.GameLoadEvent e)
        {
            // NOTE: We force the nullable here because a shift should always be found
            var shift = ShiftDb.TryLoadShiftByIndex(e.Game.ShiftIndex)!;

            ShiftLoaded?.Invoke(new IShiftLoader.ShiftLoadedEvent(shift));

            StartShift(shift);
        }

        private void Awake()
        {
            Singleton.TryFind<IGameLoader>()!.GameLoaded += OnGameLoaded;
        }
    }
}