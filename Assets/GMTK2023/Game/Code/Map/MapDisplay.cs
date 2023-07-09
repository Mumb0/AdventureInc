using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GMTK2023.Game.MiniGames;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK2023.Game
{
    public class MapDisplay : MonoBehaviour
    {
        #region Constants

        public const int MaxAdventurers = 4;

        #endregion

        #region Fields

        [SerializeField] private LocationDisplayLink[] locationDisplays = Array.Empty<LocationDisplayLink>();
        [SerializeField] private SpriteRenderer? backgroundSpriteRenderer;
        [SerializeField] private Image? backArrowImage;
        [SerializeField] private CanvasGroup? canvasGroup;

        private IAdventurerLocationTracker adventurerLocationTracker = null!;

        #endregion

        #region Methods

        private void Awake()
        {
            foreach (var link in locationDisplays)
            {
                link.LocationDisplay.LocationClicked += _ =>
                    OnLocationClicked(link.LocationDisplay, link.Location);
            }

            adventurerLocationTracker = Singleton.TryFind<IAdventurerLocationTracker>()!;

            Singleton.TryFind<TravelManager>()!.AdventurerLocationStart += OnAdventurerStarted;
            Singleton.TryFind<TravelManager>()!.AdventurerChangedLocation += OnAdventurerMoved;
        }

        public void SwapMapDisplayState(bool isShown)
        {
            backgroundSpriteRenderer!.enabled = isShown;
            backArrowImage!.enabled = !isShown;
            canvasGroup!.alpha = isShown ? 1 : 0;
            canvasGroup!.blocksRaycasts = isShown;
        }

        private void RefreshAdventurerImages()
        {
            foreach (var link in locationDisplays)
            {
                var (location, display) = (link.Location, link.LocationDisplay);
                var adventurers = adventurerLocationTracker.AdventurersAt(location).ToArray();
                for (var i = 0; i < display.AdventurerSlots.Length; i++)
                {
                    var slot = display.AdventurerSlots[i];
                    var hasAdventurer = i < adventurers.Length;
                    slot.enabled = hasAdventurer;
                    if (!hasAdventurer) continue;
                    slot.color = adventurers[i].Info.DisplayColor;
                }
            }
        }

        private void OnLocationClicked(LocationDisplay locationDisplay, ILocation location)
        {
            IMiniGame? locationMiniGame = Singleton.TryFind<MapKeeper>()!.TryGetMiniGameFor(location);

            if (locationMiniGame == null) return;

            Singleton.TryFind<MiniGameManager>()!.OnMiniGameClicked(locationMiniGame);
        }

        private void OnAdventurerStarted(IAdventurerLocationTracker.AdventurerLocationStartEvent e)
        {
            RefreshAdventurerImages();
        }

        private void OnAdventurerMoved(IAdventurerLocationTracker.AdventurerChangedLocationEvent e)
        {
            RefreshAdventurerImages();
        }

        #endregion
    }
}