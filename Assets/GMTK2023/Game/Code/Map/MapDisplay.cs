using UnityEngine;

namespace GMTK2023.Game
{
    public class MapDisplay : MonoBehaviour
    {
        [SerializeField] private LocationDisplay[] locationDisplays = new LocationDisplay[7];
        [SerializeField] private LocationDisplayLink[] locationIconAssets = new LocationDisplayLink[7];

        private void Start()
        {
            for (int i = 0; i < locationDisplays.Length; i++)
            {
                var display = locationDisplays[i];
                var locationIcon = locationIconAssets[i];

                display.LocationIconSlot!.sprite = locationIcon.LocationIcon;
        //        display.AssignedLocation = locationIcon.Location;
            }
        }
    }
}