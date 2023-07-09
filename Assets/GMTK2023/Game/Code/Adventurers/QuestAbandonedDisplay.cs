using TMPro;
using UnityEngine;

namespace GMTK2023.Game
{
    public class QuestAbandonedDisplay : MonoBehaviour
    {
        private TMP_Text label = null!;


        private void OnQuestAbandoned(IQuestTracker.QuestAbandonedEvent e)
        {
            var miniGame = e.Quest.MiniGame;
            var text = $"Attention!\n{e.Adventurer.Info.Title} could not {miniGame.ActivityDescription} because {miniGame.AbandonmentReason}";
            label.text = text;
        }

        private void Awake()
        {
            label = GetComponent<TMP_Text>();
            label.text = "";
            Singleton.TryFind<IQuestTracker>()!.QuestAbandoned += OnQuestAbandoned;
        }
    }
}