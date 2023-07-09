using TMPro;
using UnityEngine;

namespace GMTK2023.Game.GMTK2023.Game.Code.Common
{
    public class IngameTimeDisplay : MonoBehaviour
    {
        private TMP_Text label = null!;
        private IIngameTimeKeeper ingameTimeKeeper = null!;


        private void Update()
        {
            var hour = ingameTimeKeeper.IngameTimeProgress * 24;
            label.text = $"Ingame-time: {hour:00}:00";
        }

        private void Awake()
        {
            label = GetComponent<TMP_Text>();
            ingameTimeKeeper = Singleton.TryFind<IIngameTimeKeeper>()!;
        }
    }
}