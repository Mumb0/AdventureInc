using UnityEngine;

namespace GMTK2023
{
    /// <summary>
    /// Describes an adventurer
    /// </summary>
    public interface IAdventurerInfo
    {
        /// <summary>
        /// Name/title of the adventurer
        /// </summary>
        public string Title { get; }

        public float MoveChance { get; }

        public float RandomWalkChance { get; }

        public Color DisplayColor { get; }
    }
}