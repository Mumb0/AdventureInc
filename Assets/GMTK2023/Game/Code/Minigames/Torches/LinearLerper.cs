using UnityEngine;
using UnityEngine.Events;

namespace GMTK2023.Game.MiniGames
{
    public class LinearLerper : MonoBehaviour
    {
        [SerializeField] private float start;
        [SerializeField] private float target;
        [SerializeField] private float time;
        [SerializeField] private UnityEvent<float> onTChanged = new UnityEvent<float>();

        private float t;


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
            T = Mathf.MoveTowards(T, target, Time.deltaTime / time);
        }

        public void ResetToStart()
        {
            T = start;
        }

        private void Start()
        {
            ResetToStart();
        }
    }
}