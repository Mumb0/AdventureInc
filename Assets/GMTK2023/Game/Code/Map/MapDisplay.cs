using UnityEngine;

namespace GMTK2023.Game {

	public class MapDisplay : MonoBehaviour {

		[SerializeField] private LocationDisplay[] locationDisplays = new LocationDisplay[7];
		[SerializeField] private LocationDisplayLink[] locationIconAssets = new LocationDisplayLink[7];

		private void Start() {

			for (int i = 0; i < locationDisplays.Length; i++) {
				locationDisplays[i].LocationIconSlot!.sprite = locationIconAssets[i].LocationIcon;
				locationDisplays[i].AssignedLocation = locationIconAssets[i].Location;
			}

		}

	}

}