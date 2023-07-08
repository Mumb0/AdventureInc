using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static GMTK2023.Game.IAdventurerLocationTracker;

namespace GMTK2023.Game
{
    public class TravelManager : MonoBehaviour, IAdventurerLocationTracker
    {
        public event Action<AdventurerChangedLocationEvent>? AdventurerChangedLocation;


        private IMap map = null!;


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

        private void OnAdventurerEntered(IAdventurerTracker.AdventurerEnteredEvent e)
        {
            StartAdventurerAtRandomLocation(e.Adventurer);
        }

        private void Awake()
        {
            Singleton.TryFind<IAdventurerTracker>()!.AdventurerEntered
                += OnAdventurerEntered;
            map = Singleton.TryFind<IMap>()!;
        }
    }
}