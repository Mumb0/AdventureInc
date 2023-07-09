using System;
using GMTK2023.Game.MiniGames;

namespace GMTK2023.Game
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
    }

    /// <summary>
    /// An adventurer that is active in the game
    /// </summary>
    public record Adventurer(IAdventurerInfo Info);

    public record Quest(IMiniGame MiniGame);


    public interface IAdventurerTracker
    {
        public record AdventurerEnteredEvent(Adventurer Adventurer);


        /// <summary>
        /// Invoked when an adventurer enters the shift (spawns)
        /// </summary>
        public event Action<AdventurerEnteredEvent> AdventurerEntered;
    }

    public interface IAdventurerLocationTracker
    {
        public record AdventurerLocationStartEvent(Adventurer Adventurer, ILocation Location);

        public record AdventurerChangedLocationEvent(Adventurer Adventurer, ILocation Location);


        /// <summary>
        /// Invoked when an adventurer first spawns and is put on a location
        /// </summary>
        public event Action<AdventurerLocationStartEvent> AdventurerLocationStart;

        /// <summary>
        /// Invoked when an adventurer changes their location
        /// </summary>
        public event Action<AdventurerChangedLocationEvent> AdventurerChangedLocation;


        public ILocation LocationOf(Adventurer adventurer);
    }

    public interface IQuestTracker
    {
        public record QuestStartEvent(Adventurer Adventurer, Quest Quest);

        public record QuestCompletedEvent(Adventurer Adventurer);


        /// <summary>
        /// Invoked when an adventurer reaches the location of their quest and starts it
        /// </summary>
        public event Action<QuestStartEvent> QuestStart;

        /// <summary>
        /// Invoked when an adventurer completes quest
        /// </summary>
        public event Action<QuestCompletedEvent> QuestComplete;


        public Quest CurrentQuestOf(Adventurer adventurer);
    }
}