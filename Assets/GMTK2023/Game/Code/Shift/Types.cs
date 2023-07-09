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
        [SerializeField] private AdventurerInfoAsset? info;
        [SerializeField] private float enterTimeSeconds;


        /// <summary>
        /// The adventurer
        /// </summary>
        // NOTE: We force this nullable because it should never be not set
        public IAdventurerInfo Info => info!;

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

    public interface IShiftLoader
    {
        public record ShiftLoadedEvent(IShiftInfo ShiftInfo);


        /// <summary>
        /// Invoked when a shift was loaded
        /// </summary>
        public event Action<ShiftLoadedEvent> ShiftLoaded;
    }

    public interface IShiftProgressTracker
    {
        public record ShiftStartedEvent;

        public record ShiftProgressEvent(TimeSpan TimeSinceStart);

        public record ShiftCompletedEvent;


        /// <summary>
        /// Invoked when the shift starts
        /// </summary>
        public event Action<ShiftStartedEvent> ShiftStarted;

        /// <summary>
        /// Invoked when the shift progressed
        /// </summary>
        public event Action<ShiftProgressEvent> ShiftProgressed;

        /// <summary>
        /// Invoked when the player reached the end of the shift
        /// </summary>
        public event Action<ShiftCompletedEvent> ShiftCompleted;
    }
}