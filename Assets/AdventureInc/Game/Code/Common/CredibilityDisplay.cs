using TMPro;
using UnityEngine;

namespace AdventureInc.Game
{
    public class CredibilityDisplay : MonoBehaviour
    {
        private TMP_Text label = null!;


        private void OnCredibilityChanged(ICredibilityTracker.CredibilityChangedEvent e)
        {
            var text = $"Credibility: {e.Credibility}%";
            label.text = text;
        }

        private void Awake()
        {
            label = GetComponent<TMP_Text>();
            Singleton.TryFind<ICredibilityTracker>()!.CredibilityChanged +=
                OnCredibilityChanged;
        }
    }
}