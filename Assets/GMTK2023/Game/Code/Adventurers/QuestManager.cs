using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMTK2023.Game.MiniGames;
using UnityEngine;
using static GMTK2023.Game.IAdventurerLocationTracker;
using static GMTK2023.Game.IQuestTracker;
using static GMTK2023.Game.TimeUtil;

namespace GMTK2023.Game
{
    public class QuestManager : MonoBehaviour, IQuestTracker
    {
        public event Action<QuestStartEvent>? QuestStart;
        public event Action<QuestCompletedEvent>? QuestComplete;
        public event Action<QuestAbandonedEvent>? QuestAbandoned;


        [SerializeField] private float waitSecondsAfterQuestAbandon;

        private readonly IDictionary<Adventurer, Quest> questByAdventurer =
            new Dictionary<Adventurer, Quest>();

        private IMap map = null!;
        private IAdventurerLocationTracker adventurerLocationTracker = null!;
        private IMiniGameTracker miniGameTracker = null!;


        private TimeSpan WaitTimeAfterQuestAbandon =>
            TimeSpan.FromSeconds(waitSecondsAfterQuestAbandon);


        public Quest CurrentQuestOf(Adventurer adventurer) =>
            // NOTE: We dont check for existence, because an adventurer should always have a quest
            questByAdventurer[adventurer];

        private Quest ChooseQuestFor(Adventurer adventurer)
        {
            var currentLocation = adventurerLocationTracker.LocationOf(adventurer);
            var currentMiniGame = map.TryGetMiniGameFor(currentLocation);

            var miniGames = miniGameTracker.AllMiniGames;
            var possibleMiniGames = currentMiniGame != null
                ? miniGames.Except(currentMiniGame).ToArray()
                : miniGames;

            // NOTE: We force the nullable because there should always be at least 1 mini-game
            var chosenMiniGame = possibleMiniGames.TryRandom()!;

            return new Quest(chosenMiniGame);
        }

        private void AssignQuest(Adventurer adventurer)
        {
            var quest = ChooseQuestFor(adventurer);

            questByAdventurer[adventurer] = quest;
        }

        private void OnAdventurerEntered(IAdventurerTracker.AdventurerEnteredEvent e)
        {
            AssignQuest(e.Adventurer);
        }

        private void CompleteQuest(Adventurer adventurer)
        {
            QuestComplete?.Invoke(new QuestCompletedEvent(adventurer));
            AssignQuest(adventurer);
        }

        private async void AbandonQuest(Adventurer adventurer)
        {
            QuestAbandoned?.Invoke(new QuestAbandonedEvent(adventurer));
            await Task.Delay(WaitTimeAfterQuestAbandon);
            AssignQuest(adventurer);
        }

        private async void DoQuest(Adventurer adventurer, Quest quest)
        {
            QuestStart?.Invoke(new QuestStartEvent(adventurer, quest));

            var startTime = TimeSinceUnityStart;
            var elapsed = TimeSpan.Zero;

            while (elapsed <= quest.MiniGame.Duration)
            {
                if (!quest.MiniGame.IsCredible)
                {
                    AbandonQuest(adventurer);
                    return;
                }

                await Task.Yield();
                elapsed = TimeSinceUnityStart - startTime;
            }

            CompleteQuest(adventurer);
        }

        private void OnAdventurerReachedQuestLocation(Adventurer adventurer, Quest quest)
        {
            DoQuest(adventurer, quest);
        }

        private void OnAdventurerChangedLocation(AdventurerChangedLocationEvent e)
        {
            var quest = questByAdventurer[e.Adventurer];
            var questLocation = map.LocationOf(quest.MiniGame);

            if (e.Location != questLocation) return;

            OnAdventurerReachedQuestLocation(e.Adventurer, quest);
        }

        private void Awake()
        {
            Singleton.TryFind<IAdventurerTracker>()!.AdventurerEntered +=
                OnAdventurerEntered;
            Singleton.TryFind<IAdventurerLocationTracker>()!.AdventurerChangedLocation +=
                OnAdventurerChangedLocation;

            miniGameTracker = Singleton.TryFind<IMiniGameTracker>()!;
            adventurerLocationTracker = Singleton.TryFind<IAdventurerLocationTracker>()!;
            map = Singleton.TryFind<IMap>()!;
        }
    }
}