using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2023.Game {

	public class MapDisplay : MonoBehaviour {

		[SerializeField] private LocationDisplayLink[] locationDisplays = Array.Empty<LocationDisplayLink>();

		public Dictionary<ILocation, LocationDisplay>? Locations { get; } = new Dictionary<ILocation, LocationDisplay>();

		private void Start() {

			foreach (LocationDisplayLink ldl in locationDisplays) {
				Locations?.Add(ldl.Location, ldl.LocationDisplay);
			}

		}

	}

}