using UnityEngine;

namespace AdventureInc.Game
{
    [CreateAssetMenu(fileName = "new Location", menuName = "AdventureInc/Location")]
    public class LocationAsset : ScriptableObject, ILocation
    {
        // NOTE: Use asset name
        public string Name => name;
    }
}