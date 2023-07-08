﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK2023.Game
{
    [CreateAssetMenu(fileName = "New Shift-description", menuName = "GMTK2023/Shift")]
    public class ShiftInfoAsset : ScriptableObject, IShiftInfo
    {
        [SerializeField] private AdventurerInShift[] adventurers =
            Array.Empty<AdventurerInShift>();


        public IReadOnlyList<AdventurerInShift> Adventurers => adventurers;
    }
}