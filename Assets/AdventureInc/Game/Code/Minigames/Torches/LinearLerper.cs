using UnityEngine;
using UnityEngine.Events;

namespace AdventureInc.Game.MiniGames
{
    public class LinearLerper : MonoBehaviour
    {
        [SerializeField] private float start;
        [SerializeField] private float target;
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;
        [SerializeField] private UnityEvent<float> onTChanged = new UnityEvent<float>();

        private float t;
        private float time;


        public float T
        {
            get => t;
            private set
            {
                t = start < target
                    ? Mathf.Clamp(value, start, target)
                    : Mathf.Clamp(value, target, start);
                onTChanged?.Invoke(t);
            }
        }


        private void Update()
        {
            T = Mathf.MoveTowards(T, target, UnityEngine.Time.deltaTime / time);
        }

        public void ResetToStart()
        {
            T = start;
            time = Random.Range(minTime, maxTime);
        }

        private void Start()
        {
            ResetToStart();
        }
    }
}