using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2023.Game
{
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

        public record ShiftProgressEvent(TimeSpan TimeSinceStart, float ProgressionT);

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