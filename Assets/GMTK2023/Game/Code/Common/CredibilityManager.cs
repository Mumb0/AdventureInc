using System;
using UnityEngine;

namespace GMTK2023.Game.GMTK2023.Game.Code.Common
{
    public class CredibilityManager : MonoBehaviour, ICredibilityTracker
    {
        public event Action<ICredibilityTracker.CredibilityChangedEvent>? CredibilityChanged;


        private float credibility;


        private float Credibility
        {
            get => credibility;
            set
            {
                credibility = value;
                CredibilityChanged?.Invoke(new ICredibilityTracker.CredibilityChangedEvent(value));
            }
        }


        private void OnShiftStarted(IShiftProgressTracker.ShiftStartedEvent _)
        {
            Credibility = 1;
        }

        private void Awake()
        {
            Singleton.TryFind<IShiftProgressTracker>()!.ShiftStarted += OnShiftStarted;
        }
    }
}