using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMTK2023.Game {

	public class MapDisplay : MonoBehaviour {

#region Constants

		public static int MaxAdventurers = 4;

#endregion

#region Fields

		[SerializeField] private LocationDisplayLink[] locationDisplays = Array.Empty<LocationDisplayLink>();
		[SerializeField] private Color32[] adventurerColors = Array.Empty<Color32>();

		private IList<Adventurer> ActiveAdventurers { get; set; } = new Collection<Adventurer>();
		private Dictionary<Adventurer, ILocation> LocationLog = new Dictionary<Adventurer, ILocation>();

#endregion

#region Properties

		public Dictionary<ILocation, LocationDisplay>? Locations { get; } = new Dictionary<ILocation, LocationDisplay>();

#endregion

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
			if (ActiveAdventurers.Contains(e.Adventurer)) {

				if (Locations?[e.Location].CurrentAdventurers < MaxAdventurers) {
					Locations[e.Location].CurrentAdventurers++;
					LocationLog.Add(e.Adventurer, e.Location);
				}

			}
		}

		private void OnAdventurerMoved(IAdventurerLocationTracker.AdventurerChangedLocationEvent e) {

			if (ActiveAdventurers.Contains(e.Adventurer)) {
				if (Locations?[e.Location].CurrentAdventurers < MaxAdventurers) {

					Locations[LocationLog[e.Adventurer]].CurrentAdventurers--;

					Locations[e.Location].CurrentAdventurers++;

					LocationLog[e.Adventurer] = e.Location;
				}
			}
		}

	}

}