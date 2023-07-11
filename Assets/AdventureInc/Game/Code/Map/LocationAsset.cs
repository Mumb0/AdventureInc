using UnityEngine;

namespace GMTK2023.Game
{
    [CreateAssetMenu(fileName = "new Location", menuName = "GMTK2023/Location")]
    public class LocationAsset : ScriptableObject, ILocation
    {
        // NOTE: Use asset name
        public string Name => name;
    }
}