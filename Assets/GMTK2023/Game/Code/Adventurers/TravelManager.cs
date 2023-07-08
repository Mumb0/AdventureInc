using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GMTK2023.Game.IAdventurerLocationTracker;

namespace GMTK2023.Game
{
    public class TravelManager : MonoBehaviour, IAdventurerLocationTracker
    {
        public event Action<AdventurerChangedLocationEvent>? AdventurerChangedLocation;


        [SerializeField] private float travelOpportunityIntervalSeconds;

        private TimeSpan lastUpdateTime = TimeSpan.Zero;
        private IMap map = null!;


        private TimeSpan TravelOpportunityInterval =>
            TimeSpan.FromSeconds(travelOpportunityIntervalSeconds);


        private void SetAdventurerLocation(Adventurer adventurer, ILocation location)
        {
            AdventurerChangedLocation?.Invoke(
                new AdventurerChangedLocationEvent(adventurer, location));
        }

        private void StartAdventurerAtRandomLocation(Adventurer adventurer)
        {
            var possibleLocations = map.Locations.WhereNot(map.HasMiniGameAt).ToArray();
            // NOTE: We can force the nullable because there should always be a location available
            var location = possibleLocations.TryRandom()!;

            SetAdventurerLocation(adventurer, location);
        }

        private void UpdateAdventurerLocations()
        {
           throw new NotImplementedException();
        }

        private void OnAdventurerEntered(IAdventurerTracker.AdventurerEnteredEvent e)
        {
            StartAdventurerAtRandomLocation(e.Adventurer);
        }

        private void OnShiftProgressed(IShiftProgressTracker.ShiftProgressEvent e)
        {
            var timeSinceLastTravelOpportunity =
                e.TimeSinceStart - lastUpdateTime;
            if (timeSinceLastTravelOpportunity < TravelOpportunityInterval)
                return;

            UpdateAdventurerLocations();
            lastUpdateTime = e.TimeSinceStart;
        }

        private void Awake()
        {
            Singleton.TryFind<IAdventurerTracker>()!.AdventurerEntered
                += OnAdventurerEntered;
            Singleton.TryFind<IShiftProgressTracker>()!.ShiftProgressed
                += OnShiftProgressed;
            map = Singleton.TryFind<IMap>()!;
        }
    }
}