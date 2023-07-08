using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GMTK2023.Game
{
    public class AdventureManager : MonoBehaviour
    {
        private record InactiveAdventurer(TimeSpan EnterTime);


        private readonly IList<InactiveAdventurer> inactiveAdventurers =
            new List<InactiveAdventurer>();


        private void ActivateAdventurer(InactiveAdventurer adventurer)
        {
            throw new NotImplementedException();
        }

        private void OnShiftLoaded(IShiftLoader.ShiftLoadedEvent e)
        {
            e.ShiftInfo.Adventurers
                .Select(it => new InactiveAdventurer(it.EnterTime))
                .Iter(it => inactiveAdventurers.Add(it));
        }


        private void OnShiftProgress(IShiftProgressTracker.ShiftProgressEvent e)
        {
            bool IsReadyToActivate(InactiveAdventurer adventurer) =>
                adventurer.EnterTime < e.TimeSinceStart;

            inactiveAdventurers
                .Where(IsReadyToActivate)
                .Iter(ActivateAdventurer);
        }

        private void Awake()
        {
            Singleton.TryFind<IShiftLoader>()!.OnShiftLoaded += OnShiftLoaded;
            Singleton.TryFind<IShiftProgressTracker>()!.OnShiftProgress += OnShiftProgress;
        }
    }
}