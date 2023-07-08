using System;
using System.Collections.Generic;
using GMTK2023.Game.MiniGames;
using UnityEngine;
using static GMTK2023.Game.IAdventurerLocationTracker;
using static GMTK2023.Game.IQuestTracker;

namespace GMTK2023.Game
{
    public class QuestManager : MonoBehaviour, IQuestTracker
    {
        public event Action<QuestStartEvent>? QuestStart;


        private readonly IDictionary<Adventurer, Quest> questByAdventurer =
            new Dictionary<Adventurer, Quest>();

        private IMap map = null!;
        private IMiniGameTracker miniGameTracker = null!;


        public Quest CurrentQuestOf(Adventurer adventurer) =>
            // NOTE: We dont check for existence, because an adventurer should always have a quest
            questByAdventurer[adventurer];

        private Quest ChooseQuestFor(Adventurer _)
        {
            var miniGames = miniGameTracker.AllMiniGames;
            // NOTE: We force the nullable because there should always be at least 1 mini-game
            var chosenMiniGame = miniGames.TryRandom()!;

            return new Quest(chosenMiniGame);
        }

        private void OnAdventurerEntered(IAdventurerTracker.AdventurerEnteredEvent e)
        {
            questByAdventurer.Add(e.Adventurer, ChooseQuestFor(e.Adventurer));
        }

        private void OnAdventurerReachedQuestLocation(Adventurer adventurer, Quest quest)
        {
            QuestStart?.Invoke(new QuestStartEvent(adventurer, quest));
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
            map = Singleton.TryFind<IMap>()!;
        }
    }
}