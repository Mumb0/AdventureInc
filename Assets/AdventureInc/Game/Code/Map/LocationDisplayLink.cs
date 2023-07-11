using System;
using UnityEngine;

namespace GMTK2023.Game {

	[Serializable]
	public class LocationDisplayLink {

		[SerializeField] private LocationDisplay? locationDisplay;
		[SerializeField] private LocationAsset? location;

		public ILocation Location => location!;
		public LocationDisplay LocationDisplay => locationDisplay!;

	}

}