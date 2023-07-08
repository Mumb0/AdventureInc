using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2023.Game
{
    /// <summary>
    /// Describes an adventurer in a shift
    /// </summary>
    [Serializable]
    public class AdventurerInShift
    {
        [SerializeField] private AdventurerInfoAsset? adventurerInfo;
        [SerializeField] private float enterTimeSeconds;


        /// <summary>
        /// The adventurer
        /// </summary>
        public IAdventurerInfo AdventurerInfo => adventurerInfo!;

        /// <summary>
        /// After what time does this adventurer enter the shift
        /// </summary>
        public TimeSpan EnterTime => TimeSpan.FromSeconds(enterTimeSeconds);
    }


    /// <summary>
    /// Describes a shift. Equivalent to a level or night in FNAF
    /// </summary>
    public interface IShiftInfo
    {
        /// <summary>
        /// The adventurers that will be active during this shift
        /// </summary>
        public IReadOnlyList<AdventurerInShift> Adventurers { get; }
    }
}