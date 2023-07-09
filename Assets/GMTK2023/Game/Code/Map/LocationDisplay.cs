using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK2023.Game {

	public class LocationDisplay : MonoBehaviour {

		[SerializeField] private Image[] adventurerSlots = Array.Empty<Image>();

		private int currentAdventurers = 0;

		public int CurrentAdventurers {
			get => currentAdventurers;
			set {
				currentAdventurers = value;
				UpdateAdventurerSlots();
			}
		}

		private void UpdateAdventurerSlots() {

			foreach (Image t in adventurerSlots) {
				t.enabled = false;
			}

			for (int i = 0; i < CurrentAdventurers; i++) {
				adventurerSlots[i].enabled = true;
			}

		}

	}

}