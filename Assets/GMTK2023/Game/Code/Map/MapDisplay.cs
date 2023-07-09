using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GMTK2023.Game.MiniGames;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK2023.Game {

	public class MapDisplay : MonoBehaviour {

#region Constants

		public const int MaxAdventurers = 4;

#endregion

#region Fields

		[SerializeField] private LocationDisplayLink[] locationDisplays = Array.Empty<LocationDisplayLink>();
		[SerializeField] private SpriteRenderer? backgroundSpriteRenderer;
		[SerializeField] private Image? backArrowImage;
		[SerializeField] private CanvasGroup? canvasGroup;

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
				ldl.LocationDisplay.LocationClicked += OnLocationClicked;
			}

			Singleton.TryFind<AdventurerManager>()!.AdventurerEntered += OnAdventurerEnteredWorld;
			Singleton.TryFind<TravelManager>()!.AdventurerLocationStart += OnAdventurerStarted;
			Singleton.TryFind<TravelManager>()!.AdventurerChangedLocation += OnAdventurerMoved;
		}

		public void SwapMapDisplayState(bool isShown) {
			backgroundSpriteRenderer!.enabled = isShown;
			backArrowImage!.enabled = !isShown;
			canvasGroup.alpha = isShown ? 1 : 0;
			canvasGroup.blocksRaycasts = isShown;
		}

		private void OnLocationClicked(LocationDisplay locationDisplay) {

			ILocation? clickedLocation = Locations?.FirstOrDefault(x => x.Value == locationDisplay).Key;

			if (clickedLocation == null) return;

			IMiniGame? locationMiniGame = Singleton.TryFind<MapKeeper>()!.TryGetMiniGameFor(clickedLocation);

			if (locationMiniGame == null) return;

			Singleton.TryFind<MiniGameManager>()!.OnMiniGameClicked(locationMiniGame);

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
					locationLog.Add(e.Adventurer, e.Location);
				}

			}

		}

		private void OnAdventurerMoved(IAdventurerLocationTracker.AdventurerChangedLocationEvent e) {

			int? currentLocationAdventurers = Locations?[e.Location].CurrentAdventurers;

			if (currentLocationAdventurers < MaxAdventurers) {

				if (Locations != null) {
					Locations[locationLog[e.Adventurer]].CurrentAdventurers--;

					IMiniGame? visitedMiniGame = Singleton.TryFind<MapKeeper>()!.TryGetMiniGameFor(locationLog[e.Adventurer]);

					if (visitedMiniGame != null) {
						Singleton.TryFind<MiniGameManager>()!.OnAdventurerCompletedQuest(visitedMiniGame);
					}

					Locations[e.Location].CurrentAdventurers++;
					Locations[e.Location].AdventurerSlots[currentLocationAdventurers.Value].color = e.Adventurer.Info.DisplayColor;
					locationLog[e.Adventurer] = e.Location;

				}

			}
		}

#endregion

	}

}