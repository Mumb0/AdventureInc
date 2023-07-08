using System;
using System.Collections.Generic;
using GMTK2023.Game.MiniGames;
using UnityEngine;

namespace GMTK2023.Game
{
    public class QuestManager : MonoBehaviour, IQuestTracker
    {
        private readonly IDictionary<Adventurer, Quest> questByAdventurer =
            new Dictionary<Adventurer, Quest>();

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

        private void Awake()
        {
            Singleton.TryFind<IAdventurerTracker>()!.AdventurerEntered +=
                OnAdventurerEntered;
            miniGameTracker = Singleton.TryFind<IMiniGameTracker>()!;
        }
    }
}