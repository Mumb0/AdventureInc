using System;
using UnityEngine;

namespace AdventureInc
{
    [Serializable]
    public class CustomActivityWeight
    {
        [SerializeField] private ActivityAsset? activity;
        [SerializeField] private float weight;

        public IActivity Activity => activity!;

        public float Weight => weight;
    }

    /// <summary>
    /// Describes an adventurer
    /// </summary>
    public interface IAdventurerInfo
    {
        /// <summary>
        /// Name/title of the adventurer
        /// </summary>
        public string ColorName { get; }

        public float MoveChance { get; }

        public float RandomWalkChance { get; }

        public Color DisplayColor { get; }


        public float WeightForActivity(IActivity activity);
    }
}