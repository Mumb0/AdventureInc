using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK2023.Game
{
    public class LocationDisplay : MonoBehaviour
    {
        [SerializeField] private LocationAsset? location;
        [SerializeField] private Image? locationIconSlot;
        [SerializeField] private Image[] adventurerSlots = Array.Empty<Image>();

        public ILocation AssignedLocation => location!;

        public Image LocationIconSlot => locationIconSlot!;
    }
}