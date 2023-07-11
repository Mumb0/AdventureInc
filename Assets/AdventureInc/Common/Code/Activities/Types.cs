using System;

namespace AdventureInc
{
    public interface IActivity
    {
        public string Description { get; }

        public string AbandonmentReason { get; }

        public TimeSpan Duration { get; }

        public int SupportedAdventurerCount { get; }
    }
}