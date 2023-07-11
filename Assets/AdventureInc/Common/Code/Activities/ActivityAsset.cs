using System;
using UnityEngine;

namespace AdventureInc
{
    [CreateAssetMenu(fileName = "new Activity", menuName = "AdventureInc/Activity")]
    public class ActivityAsset : ScriptableObject, IActivity
    {
        [SerializeField] private string description = "";
        [SerializeField] private string abandonmentReason = "";
        [SerializeField] private float durationSeconds;
        [SerializeField] private int supportedAdventurerCount;


        public string Description => description;

        public string AbandonmentReason => abandonmentReason;

        public TimeSpan Duration => TimeSpan.FromSeconds(durationSeconds);

        public int SupportedAdventurerCount => supportedAdventurerCount;
    }
}