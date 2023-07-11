using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AdventureInc.GameSaving;

namespace AdventureInc.Success
{
    public class SuccessSceneManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text? headerLabel;
        [SerializeField] private TMP_Text? buttonLabel;
        [SerializeField] private Button? progressButton;


        private async void Start()
        {
            var savedGame = await TryLoadSavedGameAsync()
                            ?? throw new Exception("No saved game");

            var thereAreMoreShifts = savedGame.ShiftIndex < ShiftDb.ShiftCount;

            if (thereAreMoreShifts)
            {
                headerLabel!.text = $"You completed shift {savedGame.ShiftIndex}";
                buttonLabel!.text = "Start next shift";
                progressButton!.onClick.AddListener(() => SceneManager.LoadScene(1));
            }
            else
            {
                headerLabel!.text = "You completed all shifts! Congratulations!";
                buttonLabel!.text = "Go to menu";
                progressButton!.onClick.AddListener(() => SceneManager.LoadScene(0));
            }
        }
    }
}