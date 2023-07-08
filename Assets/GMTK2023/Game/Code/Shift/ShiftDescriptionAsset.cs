﻿using System.Collections.Generic;
using UnityEngine;

namespace GMTK2023.Game
{
    [CreateAssetMenu(fileName = "New Shift-description", menuName = "GMTK2023/Shift")]
    public class ShiftDescriptionAsset : ScriptableObject, IShiftDescription
    {
        [SerializeField] private AdventurerInfoAsset[] adventurers;

        public IReadOnlyList<IAdventurerInfo> Adventurers => adventurers;
    }
}