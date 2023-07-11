using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureInc.Game {

	public class LocationDisplay : MonoBehaviour, IPointerDownHandler {

#region Events

		public Action<LocationDisplay>? LocationClicked;

#endregion

#region Fields

		[SerializeField] private Image[] adventurerSlots = Array.Empty<Image>();

		private int currentAdventurers = 0;

#endregion

#region Properties

		public Image[] AdventurerSlots => adventurerSlots;

		public int CurrentAdventurers {
			get => currentAdventurers;
			set {
				currentAdventurers = value;
				UpdateAdventurerSlots();
			}
		}

#endregion

#region Methods

		private void UpdateAdventurerSlots() {

			foreach (Image t in adventurerSlots) {
				t.enabled = false;
			}

			for (int i = 0; i < CurrentAdventurers; i++) {
				adventurerSlots[i].enabled = true;
			}

		}

		public void OnPointerDown(PointerEventData eventData) {
			LocationClicked?.Invoke(this);
		}

#endregion

	}

}