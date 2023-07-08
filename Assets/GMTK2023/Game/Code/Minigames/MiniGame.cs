using System;
using UnityEngine;

namespace GMTK2023.Game.MiniGames {

	public abstract class MiniGame : MonoBehaviour, IMiniGame{

#region Events

		public Action? AllMiniGameTasksCompleted;

#endregion

#region Fields

		[SerializeField] private Canvas? miniGameCanvas;

#endregion

#region Methods

		private void Awake() {
			miniGameCanvas!.worldCamera = Camera.main;
		}

		public abstract void OnAdventurerEntered();

		public abstract void OnAdventurerLeft();

#endregion

	}

}