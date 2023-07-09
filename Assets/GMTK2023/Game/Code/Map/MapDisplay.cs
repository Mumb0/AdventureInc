using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMTK2023.Game {

	public class MapDisplay : MonoBehaviour {

#region Constants

		public const int MaxAdventurers = 4;

#endregion

#region Fields

		[SerializeField] private LocationDisplayLink[] locationDisplays = Array.Empty<LocationDisplayLink>();

		private IList<Adventurer> ActiveAdventurers { get; } = new Collection<Adventurer>();
		private Dictionary<Adventurer, ILocation> locationLog = new Dictionary<Adventurer, ILocation>();

#endregion

#region Properties

		public Dictionary<ILocation, LocationDisplay>? Locations { get; } = new Dictionary<ILocation, LocationDisplay>();

#endregion

#region Methods

		private void Awake() {

			foreach (LocationDisplayLink ldl in locationDisplays) {
				Locations?.Add(ldl.Location, ldl.LocationDisplay);
			}

			Singleton.TryFind<AdventurerManager>()!.AdventurerEntered += OnAdventurerEnteredWorld;
			Singleton.TryFind<TravelManager>()!.AdventurerLocationStart += OnAdventurerStarted;
			Singleton.TryFind<TravelManager>()!.AdventurerChangedLocation += OnAdventurerMoved;
		}

		private void OnAdventurerEnteredWorld(IAdventurerTracker.AdventurerEnteredEvent e) {
			ActiveAdventurers.Add(e.Adventurer);
		}

		private void OnAdventurerStarted(IAdventurerLocationTracker.AdventurerLocationStartEvent e) {

			int? currentLocationAdventurers = Locations?[e.Location].CurrentAdventurers;

			if (currentLocationAdventurers < MaxAdventurers) {

				if (Locations != null) {
					Locations[e.Location].CurrentAdventurers++;
					Locations[e.Location].AdventurerSlots[currentLocationAdventurers.Value].color = e.Adventurer.Info.DisplayColor;
					Debug.Log(locationLog);
					locationLog.Add(e.Adventurer, e.Location);
				}

			}

		}

		private void OnAdventurerMoved(IAdventurerLocationTracker.AdventurerChangedLocationEvent e) {

			int? currentLocationAdventurers = Locations?[e.Location].CurrentAdventurers;

			if (currentLocationAdventurers < MaxAdventurers) {

				if (Locations != null) {
					Locations[locationLog[e.Adventurer]].CurrentAdventurers--;
					Locations[e.Location].CurrentAdventurers++;
					Locations[e.Location].AdventurerSlots[currentLocationAdventurers.Value].color = e.Adventurer.Info.DisplayColor;
					locationLog[e.Adventurer] = e.Location;
				}

			}
		}

#endregion

	}

}