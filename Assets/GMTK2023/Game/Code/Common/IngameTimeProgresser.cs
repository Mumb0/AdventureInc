using UnityEngine;

namespace GMTK2023.Game
{
    public class IngameTimeProgresser : MonoBehaviour, IIngameTimeKeeper
    {
        [SerializeField] private float secondsPerDay;
        private bool shouldProgressTime;

        public float IngameTimeProgress { get; private set; }


        private void Update()
        {
            if (!shouldProgressTime) return;
            IngameTimeProgress = (IngameTimeProgress + Time.deltaTime / secondsPerDay) % 1f;
        }

        private void OnShiftStarted(IShiftProgressTracker.ShiftStartedEvent _)
        {
            IngameTimeProgress = 0;
        }

        private void Awake()
        {
            Singleton.TryFind<IShiftProgressTracker>()!.ShiftStarted += OnShiftStarted;
            shouldProgressTime = true;
        }
    }
}