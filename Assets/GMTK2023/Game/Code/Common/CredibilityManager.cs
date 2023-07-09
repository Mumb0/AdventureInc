using System;
using UnityEngine;

namespace GMTK2023.Game.GMTK2023.Game.Code.Common
{
    public class CredibilityManager : MonoBehaviour, ICredibilityTracker
    {
        public event Action<ICredibilityTracker.CredibilityChangedEvent>? CredibilityChanged;

        [SerializeField] private int startCredibility;
        [SerializeField] private int questAbandonPenalty;

        private int credibility;


        private int Credibility
        {
            get => credibility;
            set
            {
                credibility = Mathf.Clamp(value, 0, startCredibility);
                CredibilityChanged?.Invoke(new ICredibilityTracker.CredibilityChangedEvent(Credibility));
            }
        }


        private void OnShiftStarted(IShiftProgressTracker.ShiftStartedEvent _)
        {
            Credibility = startCredibility;
        }

        private void OnQuestAbandoned(IQuestTracker.QuestAbandonedEvent _)
        {
            Credibility -= questAbandonPenalty;
        }

        private void Awake()
        {
            Singleton.TryFind<IShiftProgressTracker>()!.ShiftStarted += OnShiftStarted;
            Singleton.TryFind<IQuestTracker>()!.QuestAbandoned += OnQuestAbandoned;
        }
    }
}