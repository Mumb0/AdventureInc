using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GMTK2023.Game
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