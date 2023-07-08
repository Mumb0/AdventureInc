using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GMTK2023.Game.IAdventurerTracker;

namespace GMTK2023.Game
{
    public class AdventurerManager : MonoBehaviour, IAdventurerTracker
    {
        private record InactiveAdventurer(IAdventurerInfo Info, TimeSpan EnterTime);


        public event Action<AdventurerEnteredEvent>? AdventurerEntered;


        private readonly ISet<InactiveAdventurer> inactiveAdventurers =
            new HashSet<InactiveAdventurer>();


        private void ActivateAdventurer(InactiveAdventurer inactiveAdventurer)
        {
            var adventurer = new Adventurer(inactiveAdventurer.Info);
            inactiveAdventurers.Remove(inactiveAdventurer);

            AdventurerEntered?.Invoke(new AdventurerEnteredEvent(adventurer));
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