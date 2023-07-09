using System;

namespace GMTK2023
{
    public interface IActivity
    {
        public string Description { get; }

        public string AbandonmentReason { get; }

        public TimeSpan Duration { get; }

        public int SupportedAdventurerCount { get; }
    }
}