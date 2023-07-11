using TMPro;
using UnityEngine;

namespace AdventureInc.Game
{
    public class IngameTimeDisplay : MonoBehaviour
    {
        private TMP_Text label = null!;
        private IIngameTimeKeeper ingameTimeKeeper = null!;


        private void Update()
        {
            label.text = $"Ingame-time: {ingameTimeKeeper.Hour:00}:00";
        }

        private void Awake()
        {
            label = GetComponent<TMP_Text>();
            ingameTimeKeeper = Singleton.TryFind<IIngameTimeKeeper>()!;
        }
    }
}