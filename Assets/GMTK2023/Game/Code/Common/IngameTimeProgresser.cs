using UnityEngine;

namespace GMTK2023.Game
{
    public class IngameTimeProgresser : MonoBehaviour, IIngameTimeKeeper
    {
        [SerializeField] private float secondsPerDay;
        private bool shouldProgressTime;
        private float ingameTimeProgress;


        public int Hour => Mathf.FloorToInt(ingameTimeProgress * 24);

        private void Update()
        {
            if (!shouldProgressTime) return;
            ingameTimeProgress = (ingameTimeProgress + Time.deltaTime / secondsPerDay) % 1f;
        }

        private void OnShiftStarted(IShiftProgressTracker.ShiftStartedEvent _)
        {
            ingameTimeProgress = 0;
        }

        private void Awake()
        {
            Singleton.TryFind<IShiftProgressTracker>()!.ShiftStarted += OnShiftStarted;
            shouldProgressTime = true;
        }
    }
}