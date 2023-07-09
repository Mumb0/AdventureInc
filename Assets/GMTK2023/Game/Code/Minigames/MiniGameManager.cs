using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public class MiniGameManager : MonoBehaviour, IMiniGameTracker {

#region Events

		public Action<IMiniGame>? ActiveMiniGameChanged;

#endregion

#region Fields

		[SerializeField] private GameObject? map;
		[SerializeField] private MiniGame[] availableMiniGames = Array.Empty<MiniGame>();
		[SerializeField] private TextMeshProUGUI[] stepMeshes = Array.Empty<TextMeshProUGUI>();
		private IMiniGame? activeMiniGame;

#endregion

#region Properties

		public IReadOnlyList<IMiniGame> AllMiniGames => availableMiniGames;

		public IMiniGame? ActiveMiniGame {
			get => activeMiniGame;
			set {
				activeMiniGame = value;
				ActiveMiniGameChanged?.Invoke(ActiveMiniGame!);
			}
		}

#endregion

#region Methods

		private void OnEnable() {
			ActiveMiniGameChanged += OnActiveMiniGameChanged;
		}

		public void OnMiniGameClicked(IMiniGame clickedMiniGame) {

			foreach (IMiniGame mg in AllMiniGames) {

				if (mg == clickedMiniGame) {
					ActiveMiniGame = mg;
					ActiveMiniGame.SetActive(true);
				}
				else {
					mg.SetActive(false);
				}

			}

			map!.SetActive(false);

		}

		public void OnActiveMiniGameChanged(IMiniGame miniGame) {

			ActiveMiniGame!.MiniGameTaskCompleted -= OnMiniGameTaskCompleted;

			foreach (var mesh in stepMeshes) {
				mesh.text = "";
			}

			for (int i = 0; i < miniGame.MiniGameTasks.Length; i++) {
				stepMeshes[i].text = miniGame.MiniGameTasks[i].TaskText;
			}

			miniGame.MiniGameTaskCompleted += OnMiniGameTaskCompleted;

		}

		private void OnMiniGameTaskCompleted(int taskIndex) {
			stepMeshes[taskIndex].fontStyle = FontStyles.Strikethrough;
		}

#endregion

	}

}