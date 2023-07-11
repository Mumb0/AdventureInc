using System;
using UnityEngine;

namespace AdventureInc.Game {

	[Serializable]
	public class LocationDisplayLink {

		[SerializeField] private LocationDisplay? locationDisplay;
		[SerializeField] private LocationAsset? location;

		public ILocation Location => location!;
		public LocationDisplay LocationDisplay => locationDisplay!;

	}

}