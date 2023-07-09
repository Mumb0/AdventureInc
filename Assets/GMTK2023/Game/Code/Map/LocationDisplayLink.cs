using System;
using UnityEngine;

namespace GMTK2023.Game {

	[Serializable]
	public class LocationDisplayLink {

		[SerializeField] private Sprite? locationIcon;
		[SerializeField] private LocationAsset? location;

		public ILocation Location => location!;
		public Sprite LocationIcon => locationIcon!;

	}

}