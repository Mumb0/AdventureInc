using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AdventureInc.Game.IAdventurerTracker;

namespace AdventureInc.Game
{
    public class AdventurerManager : MonoBehaviour, IAdventurerTracker
    {
        private record InactiveAdventurer(IAdventurerInfo Info, TimeSpan EnterTime);


        public event Action<IAdventurerTracker.AdventurerEnteredEvent>? AdventurerEntered;


        private readonly ISet<Adventurer> activeAdventures =
            new HashSet<Adventurer>();

        private readonly ISet<InactiveAdventurer> inactiveAdventurers =
            new HashSet<InactiveAdventurer>();


        public IEnumerable<Adventurer> ActiveAdventurers => activeAdventures;


        private void ActivateAdventurer(InactiveAdventurer inactiveAdventurer)
        {
            var adventurer = new Adventurer(inactiveAdventurer.Info);
            inactiveAdventurers.Remove(inactiveAdventurer);
            activeAdventures.Add(adventurer);

            AdventurerEntered?.Invoke(new IAdventurerTracker.AdventurerEnteredEvent(adventurer));
        }

        private void OnShiftLoaded(IShiftLoader.ShiftLoadedEvent e)
        {
            e.ShiftInfo.Adventurers
                .Select(it => new InactiveAdventurer(it.Info, it.EnterTime))
                .Iter(it => inactiveAdventurers.Add(it));
        }

        private void OnShiftProgressed(IShiftProgressTracker.ShiftProgressEvent e)
        {
            bool IsReadyToActivate(InactiveAdventurer adventurer) =>
                adventurer.EnterTime < e.TimeSinceStart;

            inactiveAdventurers
                .Where(IsReadyToActivate)
                .ToArray()
                .Iter(ActivateAdventurer);
        }

        private void Awake()
        {
            Singleton.TryFind<IShiftLoader>()!.ShiftLoaded += OnShiftLoaded;
            Singleton.TryFind<IShiftProgressTracker>()!.ShiftProgressed += OnShiftProgressed;
        }
    }
}