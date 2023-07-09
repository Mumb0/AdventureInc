using System;
using UnityEngine;

namespace GMTK2023
{
    [CreateAssetMenu(fileName = "new Activity", menuName = "GMTK2023/Activity")]
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