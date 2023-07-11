using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AdventureInc
{
    [CreateAssetMenu(fileName = "New Adventurer-info", menuName = "GMTK2023/Adventurer-info")]
    public class AdventurerInfoAsset : ScriptableObject, IAdventurerInfo
    {
        [SerializeField] [Range(0, 1)] private float moveChance;
        [SerializeField] [Range(0, 1)] private float randomWalkChance;
        [SerializeField] private string colorName = "";
        [SerializeField] private Color32 displayColor;

        [SerializeField] private CustomActivityWeight[] weights =
            Array.Empty<CustomActivityWeight>();

        public string ColorName => colorName;

        public float MoveChance => moveChance;

        public float RandomWalkChance => randomWalkChance;

        public Color DisplayColor => displayColor;

        public float WeightForActivity(IActivity activity)
        {
            return weights.FirstOrDefault(it => it.Activity == activity)?.Weight ?? 1;
        }
    }
}