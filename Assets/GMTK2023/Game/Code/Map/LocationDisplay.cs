using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK2023.Game {

	public class LocationDisplay : MonoBehaviour {

		[SerializeField] private Image? locationIconSlot;
		[SerializeField] private Image[] adventurerSlots = Array.Empty<Image>();

		public ILocation? AssignedLocation { get; set; }

	}

}